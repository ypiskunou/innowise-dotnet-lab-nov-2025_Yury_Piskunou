import React, { useState, useEffect } from 'react';
import apiClient from '../api/axiosClient'; 
import { PRODUCT_API_URL } from '../api/config';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const CreateProductPage = () => {
    const [formData, setFormData] = useState({ name: '', price: '', description: '', categoryId: '' });
    const [categories, setCategories] = useState([]);
    const [error, setError] = useState('');
    const navigate = useNavigate();
    
    useEffect(() => {
        axios.get(`${PRODUCT_API_URL}/categories`)
            .then(res => setCategories(res.data))
            .catch(console.error);
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await apiClient.post(`${PRODUCT_API_URL}/products`, {
                ...formData,
                price: parseFloat(formData.price)
            });
            navigate('/products');
        } catch (err) {
            setError('Не удалось создать товар. Проверьте данные.');
        }
    };

    return (
        <div className="row justify-content-center">
            <div className="col-md-6">
                <h2>Новый товар</h2>
                {error && <div className="alert alert-danger">{error}</div>}
                <form onSubmit={handleSubmit}>
                    <div className="mb-2">
                        <label>Название</label>
                        <input className="form-control" onChange={e => setFormData({...formData, name: e.target.value})} required />
                    </div>
                    <div className="mb-2">
                        <label>Цена</label>
                        <input className="form-control" type="number" onChange={e => setFormData({...formData, price: e.target.value})} required />
                    </div>
                    <div className="mb-2">
                        <label>Категория</label>
                        <select className="form-select" onChange={e => setFormData({...formData, categoryId: e.target.value})} required>
                            <option value="">Выберите категорию</option>
                            {categories.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                        </select>
                    </div>
                    <div className="mb-3">
                        <label>Описание</label>
                        <textarea className="form-control" onChange={e => setFormData({...formData, description: e.target.value})}></textarea>
                    </div>
                    <button className="btn btn-primary">Создать</button>
                </form>
            </div>
        </div>
    );
};

export default CreateProductPage;