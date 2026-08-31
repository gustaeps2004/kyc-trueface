import { Routes, Route, Navigate  } from "react-router-dom";
import { lazy, Suspense } from "react";
import { useTranslation } from 'react-i18next';
import PrivateRoute from "./routes/PrivateRoute";

const Login = lazy(() => import("./pages/login/Login").then(m => ({ default: m.Login })));
const ForgotPassword = lazy(() => import("./pages/login/ForgotPassword").then(m => ({ default: m.ForgotPassword })));
const RegisterPassword = lazy(() => import("./pages/login/RegisterPassword").then(m => ({ default: m.RegisterPassword })));
const Dashboard = lazy(() => import("./pages/home/Dashboard").then(m => ({ default: m.Dashboard })));
const User = lazy(() => import("./pages/users/User").then(m => ({ default: m.User })));
const Onboarding = lazy(() => import("./pages/onboarding/Onboarding").then(m => ({ default: m.Onboarding })));
const OnboardingHistory = lazy(() => import("./pages/history/Onboarding").then(m => ({ default: m.OnboardingHistory })));

export default function App() {
  const allUsers = ['COMMUN', 'ADMINISTRATOR', 'MASTER']
  const admins = ['ADMINISTRATOR', 'MASTER']
  const { t } = useTranslation();

  return (
    <Suspense fallback={
      <div className="flex items-center justify-center h-screen bg-base text-fg-subtle text-sm">
        {t('notifications.loading')}
      </div>
    }>
      <Routes>
        <Route path="/"                   element={<Navigate to="/login" />} />
        <Route path="/login"              element={<Login />} />
        <Route path="/forgot-password"    element={<ForgotPassword />} />
        <Route path="/register-password"  element={<RegisterPassword />} />

        <Route element={<PrivateRoute allowedRoles={allUsers} />}>
          <Route path="/home"               element={<Dashboard />} />
          <Route path="/onboarding"         element={<Onboarding />} />
          <Route path="/history/onboarding" element={<OnboardingHistory />} />
        </Route>

        <Route element={<PrivateRoute allowedRoles={admins} />}>
          <Route path="/users"              element={<User />} />
        </Route>
      </Routes>
    </Suspense>
  )
}