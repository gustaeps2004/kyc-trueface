import { LogOut } from "lucide-react";
import { useNavigate } from 'react-router-dom';

export default function Topbar() {
  const navigate = useNavigate();

  const handleRedirect = () => {
    navigate('/login');
  };

  return (
    <div className="
      h-16 
      bg-primary 
      border-slate-600 
      flex 
      items-center 
      justify-between 
      px-8"
    >
      <h1 className="text-2xl text-white font-medium">
        Welcome, Gustavo
      </h1>

      <button className="text-slate-300 hover:text-white transition">
        <LogOut onClick={handleRedirect} className="cursor-pointer"/>
      </button>
    </div>
  );
}