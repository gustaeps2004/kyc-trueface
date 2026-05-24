import { Routes, Route, Navigate  } from "react-router-dom";
import { Login } from "./pages/login/Login";
import { ForgotPassword } from "./pages/login/ForgotPassword";
import { RegisterPassword } from "./pages/login/RegisterPassword"
import { Dashboard } from "./pages/home/Dashboard";
import { User } from "./pages/users/User";
import { Onboarding } from "./pages/onboarding/Onboarding";
import { OnboardingHistory } from "./pages/history/Onboarding";
import PrivateRoute from "./routes/PrivateRoute";

export default function App() {
  const allUsers = ['COMMUN', 'ADMINISTRATOR', 'MASTER']
  const admins = ['ADMINISTRATOR', 'MASTER']

  return (
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
  )
}