import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css'; 
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import ItemPage from './pages/ItemPage';
import Dashboard from './pages/Dashboard';
import ItemDetailsPage from './pages/ItemDetailsPage';
import ChatListPage from './pages/ChatListPage';
import ChatPage from './pages/ChatPage';
import UserChatsPage from './pages/UserChatsPage';





const App = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegisterPage />} />
                <Route path="/item" element={<ItemPage />} />
                <Route path="/dashboard" element={<Dashboard />} />
                <Route path="/item/:itemId" element={<ItemDetailsPage />} />
                <Route path="/chats" element={<ChatListPage />} />
                <Route path="/chat/:chatId" element={<ChatPage />} />
                <Route path="/user-chats" element={<UserChatsPage />} />



            </Routes>
        </Router>
    );
};

export default App;
