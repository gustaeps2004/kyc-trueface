import { Routes, Route, Navigate  } from "react-router-dom";
import { Login } from "./pages/login/Login";
import { ForgotPassword } from "./pages/login/ForgotPassword";
import { Dashboard } from "./pages/home/Dashboard";
import { User } from "./pages/users/User";
import { Onboarding } from "./pages/onboarding/Onboarding";
import { OnboardingHistory } from "./pages/history/Onboarding";

export default function App() {
  return (
    <Routes>
      <Route path="/"                   element={<Navigate to="/login" />} />      
      <Route path="/login"              element={<Login />} />
      <Route path="/forgot-password"    element={<ForgotPassword />} />
      <Route path="/home"               element={<Dashboard />} />
      <Route path="/users"              element={<User />} />
      <Route path="/onboarding"         element={<Onboarding />} />
      <Route path="history/onboarding"  element={<OnboardingHistory />} />
    </Routes>
  )
}