import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./../styles/_homePage.scss";

const HomePage = () => {
    const [items, setItems] = useState([]);
    const [error, setError] = useState(null);
    const [user, setUser] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchItems = async () => {
            try {
                const response = await fetch("https://localhost:7176/Item/GetAllItems");
                if (!response.ok) {
                    const errorData = await response.text();
                    throw new Error(errorData || "Blad pobierania przedmiotow.");
                }
                const data = await response.json();
                setItems(data);
            } catch (err) {
                console.error("Blad przy pobieraniu przedmiotow:", err);
                setError("Nie udalo sie pobrac przedmiotow. Sprobuj ponownie pozniej.");
            }
        };

        const authToken = localStorage.getItem("authToken");
        const userId = localStorage.getItem("userId");
        if (authToken && userId) {
            setUser({ id: userId });
        }

        fetchItems();
    }, []);

    const handleLogout = () => {
        localStorage.removeItem("authToken");
        localStorage.removeItem("userId");
        setUser(null); 
        navigate("/");
    };

    return (
        <div className="homepage">
            <header className="homepage-header">
                <nav className="navbar">
                    <a href="/" className="navbar-logo">
                        Eventify
                    </a>
                    <div className="navbar-buttons">
                        <button className="nav-btn" onClick={() => navigate("/")}>
                            Strona glowna
                        </button>
                        {user ? (
                            <>
                               
                                <button className="nav-btn" onClick={() => navigate("/dashboard")}>
                                    Dashboard
                                </button>
                                <button className="nav-btn" onClick={() => navigate("/user-chats")}>
                                    User Chats
                                </button>
                                <button className="nav-btn" onClick={handleLogout}>
                                    Wyloguj sie
                                </button>
                            </>
                        ) : (
                            <>
                                <button className="nav-btn" onClick={() => navigate("/login")}>
                                    Logowanie
                                </button>
                                <button className="nav-btn" onClick={() => navigate("/register")}>
                                    Rejestracja
                                </button>
                            </>
                        )}
                    </div>
                </nav>
            </header>

            <main className="homepage-content">
                <h1>Wszystkie przedmioty</h1>
                {error && <p className="error-message">{error}</p>}
                <div className="items-list">
                    {items && items.length > 0 ? (
                        items.map((item) => (
                            <div
                                key={item.id}
                                className="item-card"
                                onClick={() => navigate(`/item/${item.id}`)}
                                style={{ cursor: "pointer" }}
                            >
                                <h2>{item.title}</h2>
                                {item.description && <p>{item.description}</p>}
                                <p>
                                    <strong>Cena:</strong> {Number(item.price).toFixed(2)} PLN
                                </p>
                                <p><strong>Miasto:</strong> {item.city}</p>
                                <p><strong>Kategoria:</strong> {item.category}</p>

                            </div>
                        ))
                    ) : (
                        <p>Brak przedmiotow do wyswietlenia.</p>
                    )}
                </div>
            </main>
        </div>
    );
};

export default HomePage;
