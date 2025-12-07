import React, { useState, useEffect } from 'react';
import apiClient from '../api/axiosClient';
import { PRODUCT_API_URL } from '../api/config';
import { useNavigate, useParams } from 'react-router-dom';
import axios from 'axios';

const EditProductPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    // Начальное состояние
    const [formData, setFormData] = useState({
        name: '',
        price: '',
        description: '',
        categoryId: '',
        isActive: true
    });

    const [categories, setCategories] = useState([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true); // Флаг загрузки

    useEffect(() => {
        const loadData = async () => {
            try {
                setLoading(true);
                // Параллельная загрузка категорий и продукта
                const [catsRes, prodRes] = await Promise.all([
                    axios.get(`${PRODUCT_API_URL}/categories`),
                    apiClient.get(`${PRODUCT_API_URL}/products/${id}`)
                ]);

                setCategories(catsRes.data);
                const p = prodRes.data;

                // Заполняем форму данными с сервера
                setFormData({
                    name: p.name,
                    price: p.price,
                    description: p.description || '', // Если null, ставим пустую строку
                    categoryId: p.categoryId,
                    isActive: p.isActive
                });
            } catch (err) {
                console.error(err);
                setError("Ошибка загрузки. Возможно, товара не существует.");
            } finally {
                setLoading(false);
            }
        };
        loadData();
    }, [id]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await apiClient.put(`${PRODUCT_API_URL}/products/${id}`, {
                ...formData,
                price: parseFloat(formData.price) // Гарантируем число
            });
            navigate('/my-products');
        } catch (err) {
            setError('Не удалось обновить. ' + (err.response?.data?.message || err.message));
        }
    };

    if (loading) return <div className="text-center mt-5"><div className="spinner-border text-primary"></div></div>;

    return (
        <div className="row justify-content-center">
            <div className="col-md-6">
                <h2>Редактирование товара</h2>
                {error && <div className="alert alert-danger">{error}</div>}

                <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                        <label className="form-label">Название</label>
                        <input className="form-control"
                               value={formData.name}
                               onChange={e => setFormData({...formData, name: e.target.value})}
                               required />
                    </div>

                    <div className="mb-3">
                        <label className="form-label">Цена</label>
                        <input className="form-control" type="number"
                               value={formData.price}
                               onChange={e => setFormData({...formData, price: e.target.value})}
                               required />
                    </div>

                    <div className="mb-3">
                        <label className="form-label">Категория</label>
                        <select className="form-select"
                                value={formData.categoryId}
                                onChange={e => setFormData({...formData, categoryId: e.target.value})}
                                required>
                            <option value="">Выберите категорию</option>
                            {categories.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                        </select>
                    </div>

                    <div className="mb-3">
                        <label className="form-label">Описание</label>
                        <textarea className="form-control" rows="3"
                                  value={formData.description}
                                  onChange={e => setFormData({...formData, description: e.target.value})}>
                        </textarea>
                    </div>

                    <div className="mb-4 form-check form-switch">
                        <input className="form-check-input" type="checkbox" id="activeSwitch"
                               checked={formData.isActive}
                               onChange={e => setFormData({...formData, isActive: e.target.checked})} />
                        <label className="form-check-label" htmlFor="activeSwitch">
                            Товар активен
                        </label>
                    </div>

                    <div className="d-flex justify-content-between">
                        <button type="button" className="btn btn-secondary" onClick={() => navigate('/my-products')}>
                            Отмена
                        </button>
                        <button type="submit" className="btn btn-primary">
                            Сохранить
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default EditProductPage;