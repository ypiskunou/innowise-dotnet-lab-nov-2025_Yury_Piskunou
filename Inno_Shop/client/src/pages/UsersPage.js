import React, { useEffect, useState, useContext } from 'react'; 
import apiClient from '../api/axiosClient';
import { USER_API_URL } from '../api/config';
import { AuthContext } from '../context/AuthContext';

const UsersPage = () => {
    const [users, setUsers] = useState([]);
    const [filter, setFilter] = useState('all');
    
    const { user: currentUser } = useContext(AuthContext);

    const fetchUsers = async () => {
        try {
            let params = {};
            if (filter === 'active') params.isActive = true;
            if (filter === 'banned') params.isActive = false;

            const res = await apiClient.get(`${USER_API_URL}/users`, { params });
            setUsers(res.data);
        } catch (err) {
            console.error("Ошибка загрузки юзеров", err);
        }
    };

    useEffect(() => {
        fetchUsers();
    }, [filter]);

    const handleBan = async (id) => {
        if (!window.confirm("Забанить? Товары юзера будут скрыты.")) return;
        try {
            await apiClient.put(`${USER_API_URL}/users/${id}/deactivate`);
            fetchUsers();
        } catch (e) { alert("Ошибка при бане: " + (e.response?.data?.message || e.message)); }
    };

    const handleUnban = async (id) => {
        try {
            await apiClient.put(`${USER_API_URL}/users/${id}/activate`);
            fetchUsers();
        } catch (e) { alert("Ошибка при разбане"); }
    };

    return (
        <div>
            <h2>Управление пользователями</h2>

            <div className="btn-group mb-3">
                <button className={`btn btn-${filter === 'all' ? 'primary' : 'outline-primary'}`}
                        onClick={() => setFilter('all')}>Все</button>
                <button className={`btn btn-${filter === 'active' ? 'success' : 'outline-success'}`}
                        onClick={() => setFilter('active')}>Активные</button>
                <button className={`btn btn-${filter === 'banned' ? 'danger' : 'outline-danger'}`}
                        onClick={() => setFilter('banned')}>Забаненные</button>
            </div>

            <table className="table table-striped table-hover">
                <thead>
                <tr>
                    <th>Имя</th>
                    <th>Email</th>
                    <th>Статус</th>
                    <th>Действия</th>
                </tr>
                </thead>
                <tbody>
                {users.map(u => (
                    <tr key={u.id}>
                        <td>{u.name}</td>
                        <td>{u.email}</td>
                        <td>
                            {u.isActive ?
                                <span className="badge bg-success">Активен</span> :
                                <span className="badge bg-danger">Бан</span>}
                        </td>
                        <td>
                            {/* Проверяем: если ID юзера в строке совпадает с моим ID */}
                            {currentUser && u.id === currentUser.id ? (
                                <span className="text-muted fst-italic">Это вы</span>
                            ) : (
                                u.isActive ? (
                                    <button className="btn btn-sm btn-danger" onClick={() => handleBan(u.id)}>Забанить</button>
                                ) : (
                                    <button className="btn btn-sm btn-success" onClick={() => handleUnban(u.id)}>Разбанить</button>
                                )
                            )}
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
};

export default UsersPage;