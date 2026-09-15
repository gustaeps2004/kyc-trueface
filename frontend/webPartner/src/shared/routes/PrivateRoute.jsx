import React, { useEffect } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { GetTokenData } from '../utils/getTokenData';
import { useNotification } from '../context/NotificationContext';

const PrivateRoute = ({ allowedRoles }) => {
  const { notify } = useNotification();
  const { t } = useTranslation();
  const decoded = GetTokenData();

  const isExpired = !!decoded && decoded.exp < Date.now() / 1000;
  const isForbidden =
    !!decoded && !isExpired && allowedRoles && !allowedRoles.includes(decoded.role);

  let denialReason = null;
  if (!decoded || isExpired) denialReason = 'session';
  else if (isForbidden) denialReason = 'forbidden';

  useEffect(() => {
    if (!denialReason) return;

    localStorage.removeItem('token');
    notify({
      message: t(
        denialReason === 'forbidden'
          ? 'notifications.accessDenied'
          : 'notifications.authRequired'
      ),
      type: denialReason === 'forbidden' ? 'error' : 'warning',
    });
  }, [denialReason, notify, t]);

  if (denialReason) {
    return <Navigate to="/login" replace />;
  }

  return <Outlet />;
};

export default PrivateRoute;
