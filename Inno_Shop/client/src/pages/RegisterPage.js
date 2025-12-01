import React, { useState } from 'react';
import axios from 'axios';
import { USER_API_URL } from '../api/config';
import { useNavigate } from 'react-router-dom';

const RegisterPage = () => {
    const [formData, setFormData] = useState({ name: '', email: '', password: '', confirmPassword: '' });
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await axios.post(`${USER_API_URL}/users/register`, formData);
            setSuccess('Регистрация успешна! Проверьте почту (MailHog) и подтвердите аккаунт.');
            setError('');
        } catch (err) {
            const serverMsg = err.response?.data?.message || JSON.stringify(err.response?.data?.errors);
            setError(serverMsg || 'Ошибка регистрации');
        }
    };

    return (
        <div className="row justify-content-center">
            <div className="col-md-4">
                <h2>Регистрация</h2>
                {error && <div className="alert alert-danger">{error}</div>}
                {success && <div className="alert alert-success">{success}</div>}
                
                <form onSubmit={handleSubmit}>
                    <div className="mb-2">
                        <label>Имя</label>
                        <input className="form-control" onChange={e => setFormData({...formData, name: e.target.value})} />
                    </div>
                    <div className="mb-2">
                        <label>Email</label>
                        <input className="form-control" type="email" onChange={e => setFormData({...formData, email: e.target.value})} />
                    </div>
                    <div className="mb-2">
                        <label>Пароль</label>
                        <input className="form-control" type="password" onChange={e => setFormData({...formData, password: e.target.value})} />
                    </div>
                    <div className="mb-3">
                        <label>Подтвердите пароль</label>
                        <input className="form-control" type="password" onChange={e => setFormData({...formData, confirmPassword: e.target.value})} />
                    </div>
                    <button className="btn btn-success w-100">Зарегистрироваться</button>
                </form>
            </div>
        </div>
    );
};

export default RegisterPage;