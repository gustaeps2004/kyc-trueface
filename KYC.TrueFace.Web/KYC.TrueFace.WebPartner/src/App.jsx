import { Routes, Route, Navigate  } from "react-router-dom";
import { Login } from "./pages/login/Login"

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/login" />} />      
      <Route path="/login" element={<Login />} />
    </Routes>
  )
}