import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";

const ItemDetailsPage = () => {
    const { itemId } = useParams();
    const navigate = useNavigate();
    const [item, setItem] = useState(null);
    const [error, setError] = useState(null);

    const userId = localStorage.getItem("userId");

    useEffect(() => {
        const fetchItemDetails = async () => {
            try {
                const response = await fetch(`https://localhost:7176/Item/GetItemById?id=${itemId}`);
                if (!response.ok) throw new Error("Błąd pobierania danych przedmiotu.");
                const data = await response.json();
                setItem(data);
            } catch (err) {
                console.error(err);
                setError("Nie udało się załadować szczegółów przedmiotu.");
            }
        };

        fetchItemDetails();
    }, [itemId]);

    const handleStartChat = async () => {
        if (!userId || !item) {
            alert("Musisz być zalogowany, aby rozpocząć czat.");
            navigate("/login");
            return;
        }

        try {
            const chatsResponse = await fetch(`https://localhost:7176/Chat/GetChatsByUserId?userId=${userId}`);
            let chatsData;
            if (chatsResponse.status === 404) {
                chatsData = [];
            } else if (!chatsResponse.ok) {
                throw new Error("Błąd pobierania czatów.");
            } else {
                chatsData = await chatsResponse.json();
            }

            const existingChat = chatsData.find(chat =>
                chat.itemId === parseInt(itemId) && chat.sellerId === item.sellerId
            );
            if (existingChat) {
                navigate(`/chat/${existingChat.id}`);
                return;
            }
        } catch (err) {
            console.error(err);
            setError("Nie udało się pobrać listy czatów.");
            return;
        }
        const newChat = {
            itemId: parseInt(itemId),
            buyerId: parseInt(userId),
            sellerId: item.sellerId,
            createdAt: new Date().toISOString()
        };

        try {
            const createResponse = await fetch("https://localhost:7176/Chat/CreateChat", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(newChat)
            });
            if (!createResponse.ok) throw new Error("Błąd przy tworzeniu czatu.");

            const createdChat = await createResponse.json();
            navigate(`/chat/${createdChat.id}`);
        } catch (err) {
            console.error("Błąd przy tworzeniu czatu:", err);
            setError("Nie udało się rozpocząć czatu.");
        }
    };

    if (error) return <div>{error}</div>;
    if (!item) return <div>Ładowanie...</div>;

    return (
        <div className="item-details">
            <h1>{item.title}</h1>
            <p>
                <strong>Opis:</strong> {item.description}
            </p>
            <p>
                <strong>Cena:</strong> {Number(item.price).toFixed(2)} PLN
            </p>
            <p>
                <strong>Data dodania:</strong> {new Date(item.createdAt).toLocaleString()}
            </p>
            <p>
                <strong>Status:</strong> {item.isSold ? "Sprzedany" : "Dostępny"}
            </p>
            <p>
                <strong>ID Sprzedawcy:</strong> {item.sellerId}
            </p>

            {userId && parseInt(userId) !== item.sellerId && (
                <button onClick={handleStartChat}>Rozpocznij czat ze sprzedawcą</button>
            )}
            {!userId && (
                <p style={{ color: "red" }}>
                    Zaloguj się, aby rozpocząć czat z tym sprzedawcą.
                </p>
            )}
            {userId && parseInt(userId) === item.sellerId && (
                <p>Jesteś sprzedawcą tego przedmiotu.</p>
            )}
        </div>
    );
};

export default ItemDetailsPage;
