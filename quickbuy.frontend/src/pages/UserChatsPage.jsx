import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./../styles/_userChatsPage.scss";

const UserChatsPage = () => {
    const [chats, setChats] = useState({ buyerChats: [], sellerChats: [] });
    const [error, setError] = useState(null);
    const [user, setUser] = useState(null);
    const userId = localStorage.getItem("userId");
    const navigate = useNavigate();

    useEffect(() => {
        const storedUserId = localStorage.getItem("userId");
        const storedUserName = localStorage.getItem("userName");
        if (storedUserId && storedUserName) {
            setUser({ id: storedUserId, name: storedUserName });
        }
    }, []);

    useEffect(() => {
        const fetchChats = async () => {
            try {
                const response = await fetch(`https://localhost:7176/Chat/GetChatsByUserId?userId=${userId}`);
                if (!response.ok) {
                    throw new Error("Błąd pobierania czatów.");
                }
                const data = await response.json();

                const buyerChats = data.filter(chat => chat.buyerId === parseInt(userId));
                const sellerChats = data.filter(chat => chat.sellerId === parseInt(userId));

                setChats({ buyerChats, sellerChats });
            } catch (err) {
                console.error(err);
                setError("Nie udało się załadować czatów.");
            }
        };

        fetchChats();
    }, [userId]);

    const handleChatClick = (chatId) => {
        navigate(`/chat/${chatId}`);
    };

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div className="user-chats-page">
            <header className="homepage-header">
                <nav className="navbar">
                    <a href="/" className="navbar-logo">Eventify</a>
                    <div className="navbar-buttons">
                        <button className="nav-btn" onClick={() => navigate("/")}>Strona główna</button>
                        {user ? (
                            <>
                                <button className="nav-btn" onClick={() => navigate("/dashboard")}>Dashboard</button>
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

            <h1>Twoje Czat</h1>

            <section>
                <h2>Sprzedane</h2>
                {chats.sellerChats.length === 0 ? (
                    <p>Nie masz żadnych czatów jako sprzedawca.</p>
                ) : (
                    <ul>
                        {chats.sellerChats.map(chat => (
                            <li key={chat.id} onClick={() => handleChatClick(chat.id)}>
                                Czat #{chat.id} - Sprzedajesz
                            </li>
                        ))}
                    </ul>
                )}
            </section>

            <section>
                <h2>Kupione</h2>
                {chats.buyerChats.length === 0 ? (
                    <p>Nie masz żadnych czatów jako kupujący.</p>
                ) : (
                    <ul>
                        {chats.buyerChats.map(chat => (
                            <li key={chat.id} onClick={() => handleChatClick(chat.id)}>
                                Czat #{chat.id} - Kupujesz
                            </li>
                        ))}
                    </ul>
                )}
            </section>
        </div>
    );
};

export default UserChatsPage;
