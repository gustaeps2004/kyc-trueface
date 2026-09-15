import { Routes, Route, Navigate  } from "react-router-dom";
import { lazy, Suspense } from "react";
import { useTranslation } from 'react-i18next';
import PrivateRoute from "@/shared/routes/PrivateRoute";
import { AllRoles } from "@/shared/utils/permissions";

const Login = lazy(() => import("@/features/auth/pages/Login").then(m => ({ default: m.Login })));
const ForgotPassword = lazy(() => import("@/features/auth/pages/ForgotPassword").then(m => ({ default: m.ForgotPassword })));
const RegisterPassword = lazy(() => import("@/features/auth/pages/RegisterPassword").then(m => ({ default: m.RegisterPassword })));
const Dashboard = lazy(() => import("@/features/dashboard/pages/Dashboard").then(m => ({ default: m.Dashboard })));
const User = lazy(() => import("@/features/users/pages/User").then(m => ({ default: m.User })));
const Onboarding = lazy(() => import("@/features/onboarding/pages/Onboarding").then(m => ({ default: m.Onboarding })));
const OnboardingHistory = lazy(() => import("@/features/onboarding/pages/OnboardingHistory").then(m => ({ default: m.OnboardingHistory })));

export default function App() {
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

        <Route element={<PrivateRoute allowedRoles={AllRoles} />}>
          <Route path="/home"               element={<Dashboard />} />
          <Route path="/onboarding"         element={<Onboarding />} />
          <Route path="/history/onboarding" element={<OnboardingHistory />} />
          <Route path="/users"              element={<User />} />
        </Route>
      </Routes>
    </Suspense>
  )
}