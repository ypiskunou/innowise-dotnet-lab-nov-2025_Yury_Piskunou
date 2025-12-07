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

    return (
        <div className="offcanvas offcanvas-start" tabIndex="-1" id="categorySidebar">
            <div className="offcanvas-header">
                <h5 className="offcanvas-title">Категории</h5>
                <button type="button" className="btn-close text-reset" data-bs-dismiss="offcanvas"></button>
            </div>
            <div className="offcanvas-body">
                <div className="list-group list-group-flush">
                    {/* Кнопка "Все категории" */}
                    <button
                        className={`list-group-item list-group-item-action ${!selectedCategoryId ? 'active' : ''}`}
                        onClick={() => onSelectCategory('')}
                        data-bs-dismiss="offcanvas" // Закрывать меню при клике
                    >
                        Все товары
                    </button>

                    {categories.map(c => (
                        <button
                            key={c.id}
                            className={`list-group-item list-group-item-action ${selectedCategoryId === c.id ? 'active' : ''}`}
                            onClick={() => onSelectCategory(c.id)}
                            data-bs-dismiss="offcanvas"
                        >
                            {c.name}
                        </button>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default CategorySidebar;