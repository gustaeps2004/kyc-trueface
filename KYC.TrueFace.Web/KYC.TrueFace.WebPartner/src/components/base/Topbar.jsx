import { LogOut, Moon, Sun } from "lucide-react";
import { useNavigate } from 'react-router-dom';
import { useState } from "react";

export default function Topbar({ name }) {
  const navigate = useNavigate();
  const [darkMode, setDarkMode] = useState()

  const handleRedirect = () => {
    navigate('/login');
  };

  const callSetDarkMode = () => {
    setDarkMode(!darkMode)
  }

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
      <h1 className="text-2xl text-title font-medium">
        {name}
      </h1>

      <div className="
        justify-around 
        m-3
        flex
        items-center
        space-x-3"
      >
        <button 
          title={darkMode ? "Light mode" : "Dark mode"}
          className="
            text-slate-300 
            hover:text-title 
            transition"
        >
          { 
            darkMode 
            ? <Sun onClick={callSetDarkMode} className="cursor-pointer" /> 
            : <Moon onClick={callSetDarkMode} className="cursor-pointer" />
          }
        </button>

        <button title="Log out" className="text-slate-300 hover:text-title transition">
          <LogOut onClick={handleRedirect} className="cursor-pointer" />
        </button>
      </div>
    </div>
  )
}