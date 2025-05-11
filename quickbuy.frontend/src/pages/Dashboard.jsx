import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./../styles/_dashboard.scss";

const Dashboard = () => {
    const [userId, setUserId] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const storedUserId = localStorage.getItem("userId");

        if (!storedUserId) {
            navigate("/login");
        } else if (!userId) {
            setUserId(storedUserId);
        }
    }, [navigate, userId]);

    const handleLogout = () => {
        localStorage.removeItem("authToken");
        localStorage.removeItem("userId");
        navigate("/login");
    };

    const handleGoToItems = () => {
        navigate("/item");
    };

    const handleGoToUserChats = () => {
        navigate("/user-chats");
    };

    if (!userId) {
        return <div>Ładowanie...</div>;
    }

    return (
        <div className="dashboard">
            <header className="dashboard-header">
                <nav className="navbar">
                    <a href="/" className="navbar-logo">Eventify</a>
                    <div className="navbar-buttons">
                        <button className="nav-btn" onClick={handleGoToItems}>
                            Zarządzaj przedmiotami
                        </button>
                        <button className="nav-btn" onClick={handleGoToUserChats}>
                            Moje Czat
                        </button>
                        <button className="nav-btn" onClick={handleLogout}>
                            Wyloguj się
                        </button>
                    </div>
                </nav>
            </header>
            <main className="dashboard-content">
                <h1>Dashboard</h1>
                <p>Witaj, użytkowniku o id: {userId}</p>
            </main>
        </div>
    );
};

export default Dashboard;
