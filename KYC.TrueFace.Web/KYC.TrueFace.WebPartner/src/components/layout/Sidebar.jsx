import { useState } from "react";
import { NavLink } from "react-router-dom";
import { useTranslation } from 'react-i18next';
import {
  LayoutDashboard,
  Users,
  Fingerprint,
  History,
  Menu,
  ShieldCheck,
} from "lucide-react";

export default function Sidebar() {
  const { t } = useTranslation();
  const [collapsed, setCollapsed] = useState(() => {
    var closed = localStorage.getItem("sidebar")

    if (closed === null) {
      localStorage.setItem("sidebar", "open")
      return false
    }

    return closed == "closed"
  });

  const handlerSidebar = () => {
    setCollapsed(!collapsed)
    localStorage.setItem("sidebar", !collapsed ? "closed" : "open")
  }

  const menuItems = [
    { labelKey: "sidebar.dashboard", icon: <LayoutDashboard size={18} />, to: "/home" },
    { labelKey: "sidebar.users", icon: <Users size={18} />, to: "/users" },
    { labelKey: "sidebar.onboarding", icon: <Fingerprint size={18} />, to: "/onboarding" },
    { labelKey: "sidebar.history", icon: <History size={18} />, to: "/history/onboarding" },
  ];

  return (
    <aside
      className={`
        ${collapsed ? "w-20" : "w-64"}
        bg-surface
        border-r
        border-divider/30
        text-fg
        flex
        flex-col
        transition-all
        duration-500
      `}
    >
      <div className="h-16 flex items-center px-5 gap-3 border-b border-divider/30">
        <button
          onClick={handlerSidebar}
          aria-label="Toggle sidebar"
          className="
            text-fg-subtle
            hover:text-fg
            hover:bg-raised
            rounded-md
            p-1.5
            transition-all
            duration-150
            cursor-pointer
          "
        >
          <Menu size={20} />
        </button>

        {!collapsed && (
          <div className="flex items-center gap-2">
            <ShieldCheck size={18} className="text-brand" />
            <span className="text-sm font-medium text-fg">KYC TrueFace</span>
          </div>
        )}
      </div>

      <nav className="flex-1 px-3 py-5 space-y-1">
        {menuItems.map((item) => (
          <NavLink
            title={collapsed ? t(item.labelKey) : null}
            key={item.labelKey}
            to={item.to}
            end
            className={({ isActive }) =>
              `flex items-center
              ${collapsed ? "justify-center" : "gap-3"}
              px-3 py-2.5 rounded-lg
              text-sm font-medium
              transition-all duration-150
              ${isActive
                ? "bg-raised text-fg [&_svg]:text-accent-light"
                : "text-fg-subtle hover:bg-raised/60 hover:text-fg"}`
            }
          >
            {item.icon}
            {!collapsed && <span>{t(item.labelKey)}</span>}
          </NavLink>
        ))}
      </nav>
    </aside>
  );
}
