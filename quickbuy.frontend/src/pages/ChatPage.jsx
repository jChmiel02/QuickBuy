import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import "./../styles/_chatPage.scss";

const ChatPage = () => {
    const { chatId } = useParams();
    const navigate = useNavigate();
    const userId = localStorage.getItem("userId");
    const [messages, setMessages] = useState([]);
    const [newMessage, setNewMessage] = useState("");
    const [error, setError] = useState(null);
    const [user, setUser] = useState(null);
    const [isChecked, setIsChecked] = useState(false);

    useEffect(() => {
        const storedUserId = localStorage.getItem("userId");
        const storedUserName = localStorage.getItem("userName");
        if (storedUserId && storedUserName) {
            setUser({ id: storedUserId, name: storedUserName });
        }
        setIsChecked(true);
    }, []);

    useEffect(() => {
        if (!userId) return;

        const fetchMessages = async () => {
            try {
                const response = await fetch(
                    `https://localhost:7176/Message/GetMessagesByChatId?chatId=${chatId}`
                );

                if (!response.ok) {
                    if (response.status === 404) {
                        setMessages([]);
                        return;
                    }
                    throw new Error("Błąd pobierania wiadomości.");
                }

                const data = await response.json();

                if (Array.isArray(data)) {
                    setMessages(data);
                } else {
                    setError("Błąd: Otrzymano nieoczekiwany format danych.");
                }
            } catch (err) {
                console.error(err);
                setError("Nie udało się załadować wiadomości.");
            }
        };

        fetchMessages();
    }, [chatId, userId]);

    useEffect(() => {
        if (!userId && isChecked) {
            navigate("/login");
        }
    }, [userId, navigate, isChecked]);

    const handleSendMessage = async () => {
        if (!newMessage.trim()) return;

        const messageData = {
            chatId: parseInt(chatId),
            senderId: parseInt(userId),
            content: newMessage,
            sentAt: new Date().toISOString(),
        };

        try {
            const response = await fetch("https://localhost:7176/Message/SendMessage", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(messageData),
            });

            if (!response.ok) throw new Error("Błąd przy wysyłaniu wiadomości.");

            const sentMessage = await response.json();
            setMessages((prev) => [...prev, sentMessage]);
            setNewMessage("");
        } catch (err) {
            console.error(err);
            setError("Nie udało się wysłać wiadomości.");
        }
    };

    const handleDeleteChat = async () => {
        if (!window.confirm("Czy na pewno chcesz usunąć ten czat?")) return;

        try {
            const response = await fetch(`https://localhost:7176/Chat/DeleteChat?chatId=${chatId}`, {
                method: "DELETE",
            });

            if (!response.ok) {
                throw new Error("Nie udało się usunąć czatu.");
            }

            alert("Czat został usunięty.");
            navigate("/user-chats");
        } catch (err) {
            console.error(err);
            setError("Wystąpił błąd podczas usuwania czatu.");
        }
    };

    if (error) return <div>{error}</div>;

    return (
        <div className="chat-page">
            <header className="homepage-header">
                <nav className="navbar">
                    <a href="/" className="navbar-logo">Eventify</a>
                    <div className="navbar-buttons">
                        <button className="nav-btn" onClick={() => navigate("/")}>Strona główna</button>
                        {user ? (
                            <>
                                <button className="nav-btn" onClick={() => navigate("/dashboard")}>Dashboard</button>
                                <button className="nav-btn" onClick={() => navigate("/user-chats")}>User Chats</button>
                            </>
                        ) : (
                            <>
                                <button className="nav-btn" onClick={() => navigate("/login")}>Logowanie</button>
                                <button className="nav-btn" onClick={() => navigate("/register")}>Rejestracja</button>
                            </>
                        )}
                    </div>
                </nav>
            </header>

            <div className="chat-header">
                <h1>Czat #{chatId}</h1>
                <button className="delete-chat-btn" onClick={handleDeleteChat}>
                    Usuń czat
                </button>
            </div>

            <div className="messages">
                {messages.length === 0 ? (
                    <p>Nie ma jeszcze żadnych wiadomości.</p>
                ) : (
                    messages.map((msg) => (
                        <div
                            key={msg.id}
                            className={`message ${parseInt(msg.senderId) === parseInt(userId) ? "own" : "other"}`}
                        >
                            <p>{msg.content}</p>
                            <small>{new Date(msg.sentAt).toLocaleString()}</small>
                        </div>
                    ))
                )}
            </div>

            <div className="send-message">
                <input
                    type="text"
                    value={newMessage}
                    onChange={(e) => setNewMessage(e.target.value)}
                    placeholder="Napisz wiadomość..."
                />
                <button onClick={handleSendMessage}>Wyślij</button>
            </div>
        </div>
    );
};

export default ChatPage;
