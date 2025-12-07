import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import axios from 'axios';
import { PRODUCT_API_URL } from '../api/config';

const ProductDetailsPage = () => {
    const { id } = useParams();
    const [product, setProduct] = useState(null);
    const [error, setError] = useState('');

    useEffect(() => {
        axios.get(`${PRODUCT_API_URL}/products/${id}`)
            .then(res => setProduct(res.data))
            .catch(err => setError("Товар не найден"));
    }, [id]);

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
                                <button className="btn btn-success w-100 btn-lg">Купить</button>
                                <div className="mt-3 text-muted small">
                                    ID: {product.id}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ProductDetailsPage;