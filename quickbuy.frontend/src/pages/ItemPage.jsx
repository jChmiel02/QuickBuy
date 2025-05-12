import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./../styles/_itemPage.scss";

const ItemPage = () => {
    const [items, setItems] = useState([]);
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(null);

    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState("");
    const [city, setCity] = useState("");
    const [category, setCategory] = useState("");


    const navigate = useNavigate();

    const sellerId = localStorage.getItem("userId");

    const fetchItems = async () => {
        try {
            const response = await fetch(
                `https://localhost:7176/Item/GetItemsBySellerId?sellerId=${sellerId}`
            );
            if (!response.ok) {
                const errorData = await response.text();
                throw new Error(errorData || "Błąd pobierania przedmiotów.");
            }
            const data = await response.json();
            setItems(data);
        } catch (err) {
            console.error("Błąd przy pobieraniu przedmiotów:", err);
            setError("Nie udało się pobrać przedmiotów. Spróbuj ponownie później.");
        }
    };

    useEffect(() => {
        fetchItems();
    }, [sellerId]);

    const handleCreateItem = async (e) => {
        e.preventDefault();

        const newItem = {
            title,
            description,
            price: parseFloat(price),
            sellerId: parseInt(sellerId, 10),
            city,
            category,
            isSold: false,
            createdAt: new Date().toISOString(),
        };


        try {
            const response = await fetch("https://localhost:7176/Item/CreateItem", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(newItem),
            });

            if (!response.ok) {
                const errorData = await response.text();
                throw new Error(errorData || "Błąd przy tworzeniu przedmiotu.");
            }

            setSuccess("Przedmiot został pomyślnie dodany.");
            setError(null);
            setTitle("");
            setDescription("");
            setPrice("");
            fetchItems();
        } catch (err) {
            console.error("Błąd przy tworzeniu przedmiotu:", err);
            setError("Nie udało się dodać przedmiotu. Spróbuj ponownie.");
            setSuccess(null);
        }
    };

    const handleMarkAsSold = async (itemId) => {
        try {
            const response = await fetch(
                `https://localhost:7176/Item/MarkItemAsSold?itemId=${itemId}`,
                {
                    method: "PATCH",
                }
            );
            if (!response.ok) {
                const errorData = await response.text();
                throw new Error(errorData || "Błąd przy aktualizacji przedmiotu.");
            }
            setSuccess("Przedmiot oznaczony jako sprzedany.");
            setError(null);
            fetchItems();
        } catch (err) {
            console.error("Błąd przy oznaczaniu przedmiotu jako sprzedany:", err);
            setError("Nie udało się zmienić statusu przedmiotu.");
            setSuccess(null);
        }
    };

    return (
        <div className="item-page">
            <header className="item-header">
                <nav className="navbar">
                    <a href="/" className="navbar-logo">
                        Eventify
                    </a>
                    <div className="navbar-buttons">
                        <button className="nav-btn" onClick={() => navigate("/")}>
                            Strona główna
                        </button>
                        <button className="nav-btn" onClick={() => navigate("/login")}>
                            Logowanie
                        </button>
                        <button className="nav-btn" onClick={() => navigate("/register")}>
                            Rejestracja
                        </button>
                    </div>
                </nav>
            </header>
            <main className="item-content">
                <h1>Zarządzanie przedmiotami</h1>

                {error && <p className="error-message">{error}</p>}
                {success && <p className="success-message">{success}</p>}

                <section className="create-item">
                    <h2>Dodaj nowy przedmiot</h2>
                    <form onSubmit={handleCreateItem} className="create-item-form">
                        <div className="input-group">
                            <input
                                type="text"
                                placeholder="Tytuł"
                                value={title}
                                onChange={(e) => setTitle(e.target.value)}
                                required
                            />
                        </div>
                        <div className="input-group">
                            <textarea
                                placeholder="Opis"
                                value={description}
                                onChange={(e) => setDescription(e.target.value)}
                                required
                            ></textarea>
                        </div>
                        <div className="input-group">
                            <input
                                type="number"
                                placeholder="Cena"
                                step="0.01"
                                value={price}
                                onChange={(e) => setPrice(e.target.value)}
                                required
                            />
                        </div>
                        <div className="input-group">
                            <input
                                type="text"
                                placeholder="Miasto"
                                value={city}
                                onChange={(e) => setCity(e.target.value)}
                                required
                            />
                        </div>
                        <div className="input-group">
                            <input
                                type="text"
                                placeholder="Kategoria"
                                value={category}
                                onChange={(e) => setCategory(e.target.value)}
                                required
                            />
                        </div>

                        <button type="submit" className="create-btn">
                            Dodaj przedmiot
                        </button>
                    </form>
                </section>

                <section className="items-list">
                    <h2>Twoje przedmioty</h2>
                    {items && items.length > 0 ? (
                        items.map((item) => (
                            <div key={item.id} className="item-card">
                                <h3>{item.title}</h3>
                                {item.description && <p>{item.description}</p>}
                                <p>
                                    <strong>Cena:</strong> {Number(item.price).toFixed(2)} PLN
                                </p>
                                <p><strong>Miasto:</strong> {item.city}</p>
                                <p><strong>Kategoria:</strong> {item.category}</p>

                                <p>
                                    <strong>Status:</strong>{" "}
                                    {item.isSold ? "Sprzedany" : "Dostępny"}
                                </p>
                                {!item.isSold && (
                                    <button
                                        onClick={() => handleMarkAsSold(item.id)}
                                        className="mark-sold-btn"
                                    >
                                        Oznacz jako sprzedany
                                    </button>
                                )}
                            </div>
                        ))
                    ) : (
                        <p>Brak przedmiotów.</p>
                    )}
                </section>
            </main>
        </div>
    );
};

export default ItemPage;
