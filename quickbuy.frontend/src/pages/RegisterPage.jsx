import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import CryptoJS from "crypto-js";
import "./../styles/_registerPage.scss";

const RegisterPage = () => {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(null);

    const navigate = useNavigate();

    const handleRegister = async (e) => {
        e.preventDefault();

        const hashedPassword = CryptoJS.SHA256(password).toString(CryptoJS.enc.Base64);

        const userDto = {
            name,
            email,
            phoneNumber,
            password: hashedPassword,

        };

        try {
            const response = await fetch("https://localhost:7176/User/CreateUser", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(userDto),
            });

            if (!response.ok) {
                const errorData = await response.text();
                throw new Error(errorData || "Błąd rejestracji.");
            }

            setSuccess("Rejestracja zakończona sukcesem. Przekierowywanie do logowania...");
            setError(null);

            setTimeout(() => {
                navigate("/login");
            }, 2000);
        } catch (err) {
            console.error("Błąd rejestracji:", err);
            setError("Wystąpił problem podczas rejestracji. Spróbuj ponownie później.");
            setSuccess(null);
        }
    };

    return (
        <div className="register-page">
            <header className="register-header">
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
                    </div>
                </nav>
            </header>
            <main className="register-content">
                <h1>Rejestracja</h1>
                {error && <p className="error-message">{error}</p>}
                {success && <p className="success-message">{success}</p>}
                <form className="register-form" onSubmit={handleRegister}>
                    <div className="input-group">
                        <input
                            type="text"
                            placeholder="Imię i nazwisko"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                        />
                    </div>
                    <div className="input-group">
                        <input
                            type="email"
                            placeholder="Email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                    </div>
                    <div className="input-group">
                        <input
                            type="text"
                            placeholder="Numer telefonu"
                            value={phoneNumber}
                            onChange={(e) => setPhoneNumber(e.target.value)}
                            required
                        />
                    </div>
                    <div className="input-group">
                        <input
                            type="password"
                            placeholder="Hasło"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>
                    <button type="submit" className="register-btn">
                        Zarejestruj się
                    </button>
                </form>
            </main>
        </div>
    );
};

export default RegisterPage;
