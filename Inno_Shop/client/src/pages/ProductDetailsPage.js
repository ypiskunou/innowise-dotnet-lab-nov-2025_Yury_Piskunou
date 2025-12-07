import React, { useEffect, useState, useContext } from 'react'; // <--- Добавь useContext
import { useParams, Link, useNavigate } from 'react-router-dom'; // <--- Добавь useNavigate
import axios from 'axios';
import { PRODUCT_API_URL } from '../api/config';
import { AuthContext } from '../context/AuthContext'; // <--- Импорт контекста

const ProductDetailsPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { token } = useContext(AuthContext); // <--- Достаем токен

    const [product, setProduct] = useState(null);
    const [error, setError] = useState('');

    useEffect(() => {
        axios.get(`${PRODUCT_API_URL}/products/${id}`)
            .then(res => setProduct(res.data))
            .catch(err => setError("Товар не найден"));
    }, [id]);

    // Обработчик нажатия "Купить"
    const handleBuy = () => {
        if (!token) {
            // Если не залогинен - предлагаем регистрацию
            if(window.confirm("Для оформления заказа необходимо войти или зарегистрироваться. Перейти к регистрации?")) {
                navigate('/register');
            }
        } else {
            // Если залогинен - пока просто заглушка
            alert("Спасибо за интерес! Сервис заказов находится в разработке (OrderService coming soon).");
        }
    };

    if (error) return <div className="alert alert-danger mt-4">{error}</div>;
    if (!product) return <div className="text-center mt-5">Загрузка...</div>;

    return (
        <div className="container mt-4">
            <Link to="/products" className="btn btn-outline-secondary mb-3">&larr; Назад в каталог</Link>

            <div className="card shadow-lg">
                <div className="card-body">
                    <div className="row">
                        <div className="col-md-8">
                            <h1 className="card-title display-5">{product.name}</h1>
                            <h4 className="text-muted mb-4">{product.categoryName}</h4>
                            <p className="lead">{product.description}</p>
                        </div>
                        <div className="col-md-4 border-start">
                            <div className="p-3">
                                <h2 className="text-primary fw-bold mb-3">${product.price}</h2>

                                {/* Кнопка с нашим обработчиком */}
                                <button className="btn btn-success w-100 btn-lg" onClick={handleBuy}>
                                    Купить
                                </button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ProductDetailsPage;