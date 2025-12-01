import React, { useEffect, useState, useContext } from 'react';
import axios from 'axios';
import { PRODUCT_API_URL } from '../api/config';
import { Link } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';
const ProductsPage = () => {
    const [products, setProducts] = useState([]);
    const [metaData, setMetaData] = useState(null);
    const { token, isActive } = useContext(AuthContext);
    const [filters, setFilters] = useState({
        pageNumber: 1,
        pageSize: 5,
        searchTerm: '',
        orderBy: 'name',  
        minPrice: '',
        maxPrice: ''
    });
    const fetchProducts = async () => {
        try {
            const params = {
                PageNumber: filters.pageNumber,
                PageSize: filters.pageSize,
                OrderBy: filters.orderBy
            };
            if (filters.searchTerm) params.SearchTerm = filters.searchTerm;
            if (filters.minPrice) params.MinPrice = filters.minPrice;
            if (filters.maxPrice) params.MaxPrice = filters.maxPrice;
            const res = await axios.get(`${PRODUCT_API_URL}/products`, { params });
            setProducts(res.data);
            if(res.headers['x-pagination']) {
                setMetaData(JSON.parse(res.headers['x-pagination']));
            }
        } catch (err) {
            console.error("Ошибка загрузки товаров", err);
        }
    };
    useEffect(() => {
        fetchProducts();
    }, [filters]);  
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFilters(prev => ({ ...prev, [name]: value, pageNumber: 1 }));
    };
    const handlePageChange = (newPage) => {
        setFilters(prev => ({ ...prev, pageNumber: newPage }));
    };
    return (
        <div>
            <div className="d-flex justify-content-between align-items-center mb-3">
                <h1>Каталог</h1>
                {token && isActive && <Link to="/create-product" className="btn btn-primary">Добавить товар +</Link>}
            </div>
            { }
            <div className="card p-3 mb-4 bg-light">
                <div className="row g-3">
                    { }
                    <div className="col-md-4">
                        <input
                            type="text"
                            className="form-control"
                            placeholder="Поиск по названию..."
                            name="searchTerm"
                            value={filters.searchTerm}
                            onChange={handleChange}
                        />
                    </div>
                    { }
                    <div className="col-md-2">
                        <input
                            type="number"
                            className="form-control"
                            placeholder="Мин. цена"
                            name="minPrice"
                            value={filters.minPrice}
                            onChange={handleChange}
                        />
                    </div>
                    { }
                    <div className="col-md-2">
                        <input
                            type="number"
                            className="form-control"
                            placeholder="Макс. цена"
                            name="maxPrice"
                            value={filters.maxPrice}
                            onChange={handleChange}
                        />
                    </div>
                    { }
                    <div className="col-md-2">
                        <select className="form-select" name="orderBy" value={filters.orderBy} onChange={handleChange}>
                            <option value="name">По имени (А-Я)</option>
                            <option value="name desc">По имени (Я-А)</option>
                            <option value="price">Сначала дешевые</option>
                            <option value="price desc">Сначала дорогие</option>
                        </select>
                    </div>
                    { }
                    <div className="col-md-2">
                        <select className="form-select" name="pageSize" value={filters.pageSize} onChange={handleChange}>
                            <option value="2">2 на стр.</option>
                            <option value="5">5 на стр.</option>
                            <option value="10">10 на стр.</option>
                            <option value="50">50 на стр.</option>
                        </select>
                    </div>
                </div>
            </div>
            { }
            <div className="row">
                {products.length > 0 ? products.map(p => (
                    <div className="col-md-4 mb-3" key={p.id}>
                        <div className="card h-100 shadow-sm">
                            <div className="card-body">
                                <h5 className="card-title text-truncate">{p.name}</h5>
                                <h6 className="card-subtitle mb-2 text-muted">{p.categoryName || 'Без категории'}</h6>
                                <p className="card-text text-truncate">{p.description}</p>
                                <div className="d-flex justify-content-between align-items-center mt-3">
                                    <span className="fs-5 fw-bold text-primary">${p.price}</span>
                                    { }
                                </div>
                            </div>
                        </div>
                    </div>
                )) : (
                    <div className="text-center text-muted py-5">
                        <h4>Ничего не найдено 😢</h4>
                    </div>
                )}
            </div>
            { }
            {metaData && (
                <div className="d-flex justify-content-center align-items-center mt-4 mb-5">
                    <button className="btn btn-outline-secondary me-3"
                            disabled={!metaData.HasPrevious}
                            onClick={() => handlePageChange(filters.pageNumber - 1)}>
                        &laquo; Назад
                    </button>
                    <span className="fw-bold">
                        Страница {metaData.CurrentPage} из {metaData.TotalPages}
                        <span className="text-muted ms-2 fw-normal">(Всего товаров: {metaData.TotalCount})</span>
                    </span>
                    <button className="btn btn-outline-secondary ms-3"
                            disabled={!metaData.HasNext}
                            onClick={() => handlePageChange(filters.pageNumber + 1)}>
                        Вперед &raquo;
                    </button>
                </div>
            )}
        </div>
    );
};
export default ProductsPage;