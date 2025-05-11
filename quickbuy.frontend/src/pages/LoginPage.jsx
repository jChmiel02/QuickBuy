import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import CryptoJS from "crypto-js";
import "./../styles/_loginPage.scss";

const LoginPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const authToken = localStorage.getItem("authToken");
        if (authToken) {
            navigate("/dashboard");
        }
    }, [navigate]);

    const handleLogin = async (e) => {
        e.preventDefault();

        const hashedPassword = CryptoJS.SHA256(password).toString(CryptoJS.enc.Base64);
        const loginDto = {
            email: email,
            password: hashedPassword,
        };

        try {
            const response = await fetch("https://localhost:7176/Auth/Login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(loginDto),
            });

            if (!response.ok) {
                const errorData = await response.text();
                throw new Error(errorData || "Błąd logowania.");
            }

            const data = await response.json();

            localStorage.setItem("authToken", data.token);
            localStorage.setItem("userId", data.userId);
            localStorage.setItem("userName", data.name);

            setSuccess("Pomyślnie zalogowano. Przekierowywanie...");
            setError(null);

            setTimeout(() => {
                navigate("/dashboard");
            }, 1500);
        } catch (err) {
            console.error("Błąd logowania:", err);
            setError("Błąd logowania. Sprawdź, czy email i hasło są poprawne.");
            setSuccess(null);
        }
    };

    return (
        <div className="login-page">
            <header className="login-header">
                <nav className="navbar">
                    <a href="/" className="navbar-logo">
                        Eventify
                    </a>
                    <div className="navbar-buttons">
                        <button className="nav-btn" onClick={() => navigate("/")}>
                            Strona główna
                        </button>
                        <button className="nav-btn" onClick={() => navigate("/register")}>
                            Rejestracja
                        </button>
                    </div>
                </nav>
            </header>
            <main className="login-content">
                <h1>Logowanie</h1>
                {error && <p className="error-message">{error}</p>}
                {success && <p className="success-message">{success}</p>}
                <form className="login-form" onSubmit={handleLogin}>
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
                            type="password"
                            placeholder="Hasło"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>
                    <button type="submit" className="login-btn">
                        Zaloguj się
                    </button>
                </form>
            </main>
        </div>
    );
};

export default LoginPage;
