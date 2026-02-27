import { Routes, Route, Navigate  } from "react-router-dom";
import { Login } from "./pages/login/Login";
import { Dashboard } from "./pages/home/Dashboard";
import { User } from "./pages/users/User";

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/login" />} />      
      <Route path="/login" element={<Login />} />
      <Route path="/home" element={<Dashboard />} />
      <Route path="/users" element={<User />} />
    </Routes>
  )
}