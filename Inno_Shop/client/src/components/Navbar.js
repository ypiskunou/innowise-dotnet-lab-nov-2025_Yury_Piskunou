import React, { useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';

const Navbar = () => {
    const { user, role, logout } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <div className="container">
                <Link className="navbar-brand" to="/">InnoShop</Link>
                <div className="collapse navbar-collapse">
                    <ul className="navbar-nav me-auto">
                        <li className="nav-item">
                            <Link className="nav-link" to="/products">Товары</Link>
                        </li>
                        {role === 'Admin' && (
                            <li className="nav-item">
                                <Link className="nav-link text-warning" to="/admin/users">Пользователи 
                                    <span className="badge bg-danger ms-1">Admin</span></Link>
                            </li>
                        )}
                    </ul>
                    <div className="d-flex">
                        {user ? (
                            <>
                                <span className="navbar-text me-3">
                                    Привет, {user.name} {role === 'Admin' ? 
                                    <span className="badge bg-danger ms-1">Admin</span> : ''}
                                </span>
                                <button className="btn btn-outline-light btn-sm" onClick={handleLogout}>Выход</button>
                            </>
                        ) : (
                            <>
                                <Link className="btn btn-outline-primary me-2" to="/login">Вход</Link>
                                <Link className="btn btn-primary" to="/register">Регистрация</Link>
                            </>
                        )}
                    </div>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;