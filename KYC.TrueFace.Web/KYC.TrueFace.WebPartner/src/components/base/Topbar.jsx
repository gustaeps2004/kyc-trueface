import { LogOut } from "lucide-react";
import { useNavigate } from 'react-router-dom';

export default function Topbar({ name }) {
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
        <button title="Log out" className="text-slate-300 hover:text-title transition hover:scale-105">
          <LogOut onClick={handleRedirect} className="cursor-pointer" />
        </button>
      </div>
    </div>
  )
}