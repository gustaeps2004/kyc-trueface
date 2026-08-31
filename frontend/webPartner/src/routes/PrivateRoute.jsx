import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { GetTokenData } from '../utils/getTokenData';

const PrivateRoute = ({ allowedRoles }) => {
  const decoded = GetTokenData();

  if (!decoded) {
    localStorage.removeItem('token');
    return <Navigate to="/login" replace />;
  }

  const isExpired = decoded.exp < Date.now() / 1000;

  if (isExpired) {
    localStorage.removeItem('token');
    return <Navigate to="/login" replace />;
  }

  if (allowedRoles && !allowedRoles.includes(decoded.role)) {
    localStorage.removeItem('token');
    return <Navigate to="/login" replace />;
  }

  return <Outlet />;
};

export default PrivateRoute;
