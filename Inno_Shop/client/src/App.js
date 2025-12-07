import React from 'react';
import {BrowserRouter as Router, Routes, Route, Navigate} from 'react-router-dom';
import Navbar from './components/Navbar';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import ProductsPage from './pages/ProductsPage';
import CreateProductPage from './pages/CreateProductPage';
import EditProductPage from './pages/EditProductPage'; // Новая страница
import MyProductsPage from './pages/MyProductsPage'; // Новая страница
import UsersPage from './pages/UsersPage';
import {useContext} from 'react';
import {AuthContext} from './context/AuthContext';
import ProductDetailsPage from './pages/ProductDetailsPage';

function App() {
    const {token, role, isActive} = useContext(AuthContext);
    return (
        <Router>
            <Navbar/>
            <div className="container mt-4">
                <Routes>
                    <Route path="/" element={<Navigate to="/products"/>}/>
                    <Route path="/login" element={!token ? <LoginPage/> : <Navigate to="/products"/>}/>
                    <Route path="/register" element={!token ? <RegisterPage/> : <Navigate to="/products"/>}/>
                    <Route path="/products/:id" element={<ProductDetailsPage/>}/>
                    <Route path="/products" element={<ProductsPage/>}/>

                    {/* Управление своими товарами */}
                    <Route path="/my-products" element={
                        token ? <MyProductsPage/> : <Navigate to="/login"/>
                    }/>

                    <Route path="/create-product" element={
                        token && isActive ? <CreateProductPage/> : <Navigate to="/login"/>
                    }/>

                    <Route path="/edit-product/:id" element={
                        token && isActive ? <EditProductPage/> : <Navigate to="/login"/>
                    }/>

                    {/* Админка */}
                    <Route path="/admin/users" element={
                        role === 'Admin' ? <UsersPage/> : <Navigate to="/products"/>
                    }/>
                </Routes>
            </div>
        </Router>
    );
}

export default App;