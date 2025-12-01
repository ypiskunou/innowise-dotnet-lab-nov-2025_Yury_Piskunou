import React, { useEffect, useState } from 'react';
import apiClient from '../api/axiosClient';
import { PRODUCT_API_URL } from '../api/config';
import { Link } from 'react-router-dom';
const MyProductsPage = () => {
const [products, setProducts] = useState([]);
const fetchMyProducts = async () => {
    try {
        const res = await apiClient.get(`${PRODUCT_API_URL}/products/my`);
        setProducts(res.data);
    } catch (err) {
        console.error("Ошибка загрузки моих товаров", err);
    }
};

useEffect(() => {
    fetchMyProducts();
}, []);

const handleDelete = async (id) => {
    if(!window.confirm("Удалить этот товар?")) return;
    try {
        await apiClient.delete(`${PRODUCT_API_URL}/products/${id}`);
        fetchMyProducts(); 
    } catch (err) {
        alert("Не удалось удалить товар.");
    }
};

return (
    <div>
        <div className="d-flex justify-content-between align-items-center mb-4">
            <h1>Мои товары</h1>
            <Link to="/create-product" className="btn btn-success">Добавить новый</Link>
        </div>

        {products.length === 0 ? (
            <div className="alert alert-info">У вас пока нет товаров.</div>
        ) : (
            <div className="table-responsive">
                <table className="table table-striped table-hover align-middle">
                    <thead className="table-light">
                        <tr>
                            <th>Название</th>
                            <th>Категория</th>
                            <th>Цена</th>
                            <th>Статус</th>
                            <th>Действия</th>
                        </tr>
                    </thead>
                    <tbody>
                        {products.map(p => (
                            <tr key={p.id}>
                                <td className="fw-bold">{p.name}</td>
                                <td>{p.categoryName}</td>
                                <td>${p.price}</td>
                                <td>
                                    {p.isActive ? 
                                        <span className="badge bg-success">Активен</span> : 
                                        <span className="badge bg-secondary">Черновик</span>
                                    }
                                </td>
                                <td>
                                    <Link to={`/edit-product/${p.id}`} className="btn btn-sm btn-primary me-2">
                                        Редактировать
                                    </Link>
                                    <button className="btn btn-sm btn-danger" onClick={() => handleDelete(p.id)}>
                                        Удалить
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        )}
    </div>
);
};
export default MyProductsPage;