import { useState, useEffect } from "react";
import { NavLink } from "react-router-dom";
import {
  LayoutDashboard,
  Users,
  Fingerprint,
  History,
  Menu,
} from "lucide-react";

export default function Sidebar() {
  const [collapsed, setCollapsed] = useState(() => {
    var closed = localStorage.getItem("sidebar")

    if (closed === null)
      localStorage.setItem("sidebar", "open")

    return closed == "closed"
  });

  const handlerSidebar = () => {
    setCollapsed(!collapsed)
    localStorage.setItem("sidebar", !collapsed ? "closed" : "open")
  }

  const menuItems = [
    { label: "Dashboard", icon: <LayoutDashboard size={18} />, to: "/home" },
    { label: "Users", icon: <Users size={18} />, to: "/users" },
    { label: "Onboarding", icon: <Fingerprint size={18} />, to: "/onboarding" },
    { label: "History", icon: <History size={18} />, to: "/history/onboarding" },
  ];

  return (
    <aside
      className={`
        ${collapsed ? "w-20" : "w-64"}
        bg-secondary
        text-white
        flex
        flex-col
        transition-all
        duration-500
      `}
    >
      <div
        className="
          h-16
          flex
          items-center
          px-6
          text-lg
          font-semibold
        "
      >
        <button onClick={() => handlerSidebar()}>
          <Menu size={20} className="mr-3 ml-1 cursor-pointer" />
        </button>

        {!collapsed && <span className="">KYC TrueFace</span>}
      </div>

      <nav className="flex-1 px-3 py-6 space-y-2">
        {menuItems.map((item) => (
          <NavLink
            title={collapsed ? item.label : null}
            key={item.label}
            to={item.to}
            className={({ isActive }) =>
              `flex items-center 
              ${collapsed ? "justify-center" : "gap-3"}
              px-4 py-3 rounded-lg transition-all
              ${
                isActive
                  ? "bg-slate-700 text-title"
                  : "text-slate-400 hover:bg-slate-700 hover:text-title"
              }`
            }
          >
            {item.icon}
            {!collapsed && <span>{item.label}</span>}
          </NavLink>
        ))}
      </nav>
    </aside>
  );
}