import { LogOut } from "lucide-react";
import { useNavigate } from 'react-router-dom';
import { Logout } from "../../utils/functions/Logout";

export default function Topbar({ name }) {
  const navigate = useNavigate();

  const handleRedirect = () => {
    Logout()
    navigate('/login');
  };

  return (
    <div className="
      h-16
      bg-base
      border-b
      border-divider/30
      flex
      items-center
      justify-between
      px-8
    ">
      <h1 className="text-xl text-fg font-medium">
        {name}
      </h1>

      <button
        title="Log out"
        onClick={handleRedirect}
        aria-label="Log out"
        className="
          flex
          items-center
          justify-center
          w-9
          h-9
          rounded-md
          text-fg-subtle
          hover:text-fg
          hover:bg-raised
          transition-all
          duration-150
          cursor-pointer
          focus:outline-none
          focus:ring-2
          focus:ring-brand/40
        "
      >
        <LogOut size={18} />
      </button>
    </div>
  )
}
