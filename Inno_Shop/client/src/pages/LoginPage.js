import React, { useState, useContext } from 'react';
import axios from 'axios';
import { AuthContext } from '../context/AuthContext';
import { USER_API_URL } from '../api/config';

const LoginPage = () => {
    const [formData, setFormData] = useState({ email: '', password: '' });
    const [error, setError] = useState('');
    const { login } = useContext(AuthContext);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const res = await axios.post(`${USER_API_URL}/authentication/login`, formData);
            login(res.data.accessToken);
        } catch (err) {
            setError(err.response?.data?.message || 'Ошибка входа');
        }
    };

    return (
        <div className="row justify-content-center">
            <div className="col-md-4">
                <h2>Вход</h2>
                {error && <div className="alert alert-danger">{error}</div>}
                <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                        <label>Email</label>
                        <input className="form-control" type="email" 
                            onChange={e => setFormData({...formData, email: e.target.value})} />
                    </div>
                    <div className="mb-3">
                        <label>Пароль</label>
                        <input className="form-control" type="password" 
                            onChange={e => setFormData({...formData, password: e.target.value})} />
                    </div>
                    <button className="btn btn-primary w-100">Войти</button>
                </form>
            </div>
        </div>
    );
};

export default LoginPage;