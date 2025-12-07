import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { PRODUCT_API_URL } from '../api/config';

const CategorySidebar = ({ selectedCategoryId, onSelectCategory }) => {
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        axios.get(`${PRODUCT_API_URL}/categories`)
            .then(res => setCategories(res.data))
            .catch(e => console.error(e));
    }, []);

    // Считаем общее количество
    const totalAll = categories.reduce((sum, cat) => sum + cat.totalProducts, 0);

    // Функция закрытия меню при уходе мышки
    const handleMouseLeave = () => {
        const sidebarElement = document.getElementById('categorySidebar');
        // Обращаемся к глобальному объекту bootstrap
        if (window.bootstrap) {
            const bsInstance = window.bootstrap.Offcanvas.getInstance(sidebarElement);
            if (bsInstance) {
                bsInstance.hide();
            }
        }
    };

    return (
        <div
            className="offcanvas offcanvas-start"
            tabIndex="-1"
            id="categorySidebar"
            // Добавляем обработчик ухода мыши
            onMouseLeave={handleMouseLeave}
            style={{
                backgroundColor: 'rgba(255, 255, 255, 0.55)', // Чуть менее прозрачный для читаемости
                backdropFilter: 'blur(10px)'
            }}
        >
            <div className="offcanvas-header">
                <h5 className="offcanvas-title">Категории</h5>
                <button type="button" className="btn-close text-reset" data-bs-dismiss="offcanvas"></button>
            </div>
            <div className="offcanvas-body">
                <div className="list-group list-group-flush">
                    {/* Кнопка "Все товары" */}
                    <button
                        // Логика классов: если активна - стандартный синий, если нет - прозрачный
                        className={`list-group-item list-group-item-action d-flex justify-content-between align-items-center ${!selectedCategoryId ? 'active' : 'bg-transparent'}`}
                        onClick={() => onSelectCategory('')}
                    >
                        <span>Все товары</span>
                        <span className={`badge rounded-pill ${!selectedCategoryId ? 'bg-light text-primary' : 'bg-secondary'}`}>
                            {totalAll}
                        </span>
                    </button>

                    {categories.map(c => (
                        <button
                            key={c.id}
                            className={`list-group-item list-group-item-action d-flex justify-content-between align-items-center ${selectedCategoryId === c.id ? 'active' : 'bg-transparent'}`}
                            onClick={() => onSelectCategory(c.id)}
                        >
                            <span>{c.name}</span>
                            <span className={`badge rounded-pill ${selectedCategoryId === c.id ? 'bg-light text-primary' : 'bg-primary'}`}>
                                {c.totalProducts}
                            </span>
                        </button>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default CategorySidebar;