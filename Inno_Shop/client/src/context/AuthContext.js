import React, { createContext, useState, useEffect } from 'react';
import { jwtDecode } from "jwt-decode";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(localStorage.getItem('token'));
    const [role, setRole] = useState(null);
    const [isActive, setIsActive] = useState(false);

    useEffect(() => {
        if (token) {
            try {
                const decoded = jwtDecode(token);
                setUser({ 
                    name: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
                    email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
                    id: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]
                });
                
                const roles = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
                if (Array.isArray(roles)) {
                    setRole(roles.includes("Admin") ? "Admin" : "User");
                } else {
                    setRole(roles);
                }

                const userIsActive = decoded.IsActive === 'True';
                setIsActive(userIsActive);
                
            } catch (e) {
                console.error("Invalid token", e);
                logout();
            }
        }
    }, [token]);

    const login = (newToken) => {
        localStorage.setItem('token', newToken);
        setToken(newToken);
    };

    const logout = () => {
        localStorage.removeItem('token');
        setToken(null);
        setUser(null);
        setRole(null);
        setIsActive(false);
    };

    return (
        <AuthContext.Provider value={{ user, token, role, isActive, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};