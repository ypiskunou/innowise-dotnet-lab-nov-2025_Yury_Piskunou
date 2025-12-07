import axios from 'axios';

const apiClient = axios.create();

// 1. Request Interceptor (Добавляет токен)
apiClient.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
}, (error) => {
    return Promise.reject(error);
});

// 2. Response Interceptor (Ловит 401 и разлогинивает)
apiClient.interceptors.response.use(
    (response) => response, // Если успех - просто возвращаем ответ
    (error) => {
        // Если ошибка 401 (нет доступа / токен протух)
        if (error.response && (error.response.status === 401)) {

            // Чистим следы
            localStorage.removeItem('token');

            // Жесткий редирект на логин
            // (Используем window.location, так как хук useNavigate здесь недоступен)
            window.location.href = '/login';
        }
        return Promise.reject(error);
    }
);

export default apiClient;