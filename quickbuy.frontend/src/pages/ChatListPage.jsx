import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

const ChatListPage = () => {
    const [chats, setChats] = useState([]);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const userId = localStorage.getItem("userId");

    useEffect(() => {
        if (!userId) {
            navigate("/login");
            return;
        }

        const fetchChats = async () => {
            try {
                const response = await fetch(`https://localhost:7176/Chat/GetChatsByUserId?userId=${userId}`);
                if (!response.ok) throw new Error("Błąd pobierania czatów.");
                const data = await response.json();
                setChats(data);
            } catch (err) {
                console.error(err);
                setError("Nie udało się załadować listy czatów.");
            }
        };

        fetchChats();
    }, [userId, navigate]);

    const handleCreateChat = async (itemId, sellerId) => {
        const existingChat = chats.find(chat => chat.itemId === itemId && chat.sellerId === sellerId);
        if (existingChat) {
            navigate(`/chat/${existingChat.id}`);
            return;
        }

        const newChat = {
            itemId,
            buyerId: parseInt(userId),
            sellerId,
            createdAt: new Date().toISOString()
        };

        try {
            const response = await fetch("https://localhost:7176/Chat/CreateChat", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(newChat)
            });

            if (!response.ok) throw new Error("Błąd przy tworzeniu czatu.");

            const createdChat = await response.json();
            navigate(`/chat/${createdChat.id}`);
        } catch (err) {
            console.error(err);
            setError("Nie udało się utworzyć czatu.");
        }
    };

    if (error) return <div>{error}</div>;

    return (
        <div className="chat-list">
            <h1>Twoje czaty</h1>
            {chats.length === 0 ? (
                <p>Brak aktywnych czatów.</p>
            ) : (
                <ul>
                    {chats.map((chat) => (
                        <li key={chat.id}>
                            <button onClick={() => navigate(`/chat/${chat.id}`)}>
                                Czat z użytkownikiem ID: {chat.sellerId === parseInt(userId) ? chat.buyerId : chat.sellerId}
                            </button>
                        </li>
                    ))}
                </ul>
            )}
            <h2>Wybierz przedmiot, aby rozpocząć czat</h2>
            <ul>
                <li>
                    <button onClick={() => handleCreateChat(1, 2)}>
                        Rozpocznij czat o przedmiocie 1 z użytkownikiem 2
                    </button>
                </li>
                <li>
                    <button onClick={() => handleCreateChat(2, 3)}>
                        Rozpocznij czat o przedmiocie 2 z użytkownikiem 3
                    </button>
                </li>
            </ul>
        </div>
    );
};

export default ChatListPage;
