import { Routes, Route, Navigate  } from "react-router-dom";
import { useState, useEffect } from 'react';
import { Login } from "./pages/login/Login";
import { ForgotPassword } from "./pages/login/ForgotPassword";
import { RegisterPassword } from "./pages/login/RegisterPassword"
import { Dashboard } from "./pages/home/Dashboard";
import { User } from "./pages/users/User";
import { MobileAccess } from "./pages/mobile/MobileAccess";
import { Onboarding } from "./pages/onboarding/Onboarding";
import { OnboardingHistory } from "./pages/history/Onboarding";
import PrivateRoute from "./routes/PrivateRoute";

export default function App() {
  const [isMobile, setIsMobile] = useState(false);

  useEffect(() => {
    const userAgent = typeof window.navigator === "undefined" ? "" : navigator.userAgent;
    const mobileRegex = /Android|BlackBerry|iPhone|iPad|iPod|Opera Mini|IEMobile|WPDesktop/i;
    
    if (Boolean(userAgent.match(mobileRegex))) {
      setIsMobile(true);
    }
  }, []);

  const allUsers = ['COMMUN', 'ADMINISTRATOR', 'MASTER']
  const admins = ['ADMINISTRATOR', 'MASTER']

  if (isMobile) {
    return(
      <MobileAccess />
    )
  } else {
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
}