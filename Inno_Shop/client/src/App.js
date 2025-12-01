import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Navbar from './components/Navbar';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import ProductsPage from './pages/ProductsPage';
import CreateProductPage from './pages/CreateProductPage';
import UsersPage from './pages/UsersPage'; // Админка
import { useContext } from 'react';
import { AuthContext } from './context/AuthContext';

function App() {
  const { token, role, isActive } = useContext(AuthContext);

  return (
    <Router>
      <Navbar />
      <div className="container mt-4">
        <Routes>
          <Route path="/" element={<Navigate to="/products" />} />
          <Route path="/login" element={!token ? <LoginPage /> : <Navigate to="/products" />} />
          <Route path="/register" element={!token ? <RegisterPage /> : <Navigate to="/products" />} />
          
          <Route path="/products" element={<ProductsPage />} />

          <Route path="/create-product" element={
            token && isActive ? <CreateProductPage /> : <Navigate to="/login" />
          } />

          {/* Защита админки */}
          <Route path="/admin/users" element={
            role === 'Admin' ? <UsersPage /> : <Navigate to="/products" />
          } />
        </Routes>
      </div>
    </Router>
  );
}

export default App;