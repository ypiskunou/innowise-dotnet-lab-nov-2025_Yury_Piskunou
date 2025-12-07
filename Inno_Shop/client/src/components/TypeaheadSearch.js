import React, { useState, useEffect, useRef } from 'react';
import axios from 'axios';
import { PRODUCT_API_URL } from '../api/config';
import { useNavigate } from 'react-router-dom';

const TypeaheadSearch = () => {
    const [text, setText] = useState('');
    const [suggestions, setSuggestions] = useState([]);
    const [isOpen, setIsOpen] = useState(false);

    // Индекс активного элемента (для клавиатуры)
    const [activeIndex, setActiveIndex] = useState(-1);

    const navigate = useNavigate();
    const wrapperRef = useRef(null);

    useEffect(() => {
        const timer = setTimeout(() => {
            if (text.length >= 2) {
                loadSuggestions(text);
            } else {
                setSuggestions([]);
                setIsOpen(false);
            }
        }, 300);
        return () => clearTimeout(timer);
    }, [text]);

    useEffect(() => {
        const handleClickOutside = (event) => {
            if (wrapperRef.current && !wrapperRef.current.contains(event.target)) {
                setIsOpen(false);
            }
        };
        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    const loadSuggestions = async (query) => {
        try {
            const res = await axios.get(`${PRODUCT_API_URL}/products/previews?searchTerm=${query}`);
            setSuggestions(res.data);
            setIsOpen(true);
            setActiveIndex(-1); // Сброс выбора при новом поиске
        } catch (e) {
            console.error(e);
        }
    };

    const handleSelect = (id) => {
        setIsOpen(false);
        setText('');
        navigate(`/products/${id}`);
    };

    // Обработка клавиш
    const handleKeyDown = (e) => {
        if (!isOpen) return;

        if (e.key === 'ArrowDown') {
            e.preventDefault(); // Чтобы курсор в инпуте не бегал
            setActiveIndex(prev => (prev < suggestions.length - 1 ? prev + 1 : 0));
        } else if (e.key === 'ArrowUp') {
            e.preventDefault();
            setActiveIndex(prev => (prev > 0 ? prev - 1 : suggestions.length - 1));
        } else if (e.key === 'Enter') {
            e.preventDefault();
            if (activeIndex >= 0 && suggestions[activeIndex]) {
                handleSelect(suggestions[activeIndex].id);
            }
        } else if (e.key === 'Escape') {
            setIsOpen(false);
        }
    };

    return (
        <div className="position-relative me-3" ref={wrapperRef} style={{ width: '300px' }}>
            <input
                type="text"
                className="form-control"
                placeholder="Быстрый поиск..."
                value={text}
                onChange={(e) => setText(e.target.value)}
                onKeyDown={handleKeyDown} // Слушаем клавиши
            />

            {isOpen && suggestions.length > 0 && (
                <div className="list-group position-absolute w-100 shadow" style={{ zIndex: 1000, marginTop: '5px' }}>
                    {suggestions.map((p, index) => (
                        <button
                            key={p.id}
                            // Добавляем класс 'active', если индекс совпадает
                            className={`list-group-item list-group-item-action d-flex justify-content-between align-items-center ${index === activeIndex ? 'active' : ''}`}
                            onClick={() => handleSelect(p.id)}
                            // При наведении мышкой тоже обновляем индекс
                            onMouseEnter={() => setActiveIndex(index)}
                        >
                            <span>{p.name}</span>
                            <span className={`badge rounded-pill ${index === activeIndex ? 'bg-light text-primary' : 'bg-primary'}`}>
                                ${p.price}
                            </span>
                        </button>
                    ))}
                </div>
            )}

            {isOpen && suggestions.length === 0 && text.length >= 2 && (
                <div className="list-group position-absolute w-100 shadow" style={{ zIndex: 1000, marginTop: '5px' }}>
                    <div className="list-group-item text-muted">Ничего не найдено</div>
                </div>
            )}
        </div>
    );
};

export default TypeaheadSearch;