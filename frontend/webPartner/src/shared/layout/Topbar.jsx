import { LogOut, Menu, Moon, Sun } from "lucide-react";
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { Logout } from "@/shared/utils/logout";
import { useTheme } from "@/shared/context/ThemeContext";
import { IconButton } from "@/shared/ui/IconButton";

export default function Topbar({ name, onToggleSidebar }) {
  const navigate = useNavigate();
  const { t, i18n } = useTranslation();
  const { theme, toggleTheme } = useTheme();

  const handleRedirect = () => {
    Logout()
    navigate('/login');
  };

  const toggleLanguage = () => {
    const next = i18n.language === 'en' ? 'pt' : 'en';
    i18n.changeLanguage(next);
    localStorage.setItem('language', next);
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
      px-4
      lg:px-8
    ">
      <div className="flex items-center gap-3">
        <IconButton
          onClick={onToggleSidebar}
          label="Open menu"
          className="flex lg:hidden rounded-md text-fg-subtle hover:text-fg hover:bg-raised"
        >
          <Menu size={20} />
        </IconButton>

        <h1 className="text-lg lg:text-xl text-fg font-medium truncate">
          {name}
        </h1>
      </div>

      <div className="flex items-center gap-2">
        <IconButton
          onClick={toggleLanguage}
          label={t('topbar.switchLanguage')}
          className="rounded-md text-fg-subtle hover:text-fg hover:bg-raised text-lg leading-none"
        >
          {i18n.language === 'en' ? '🇺🇸' : '🇧🇷'}
        </IconButton>

        <IconButton
          onClick={toggleTheme}
          label={t('topbar.switchTheme')}
          className="rounded-md text-fg-subtle hover:text-fg hover:bg-raised"
        >
          {theme === 'dark' ? <Sun size={18} /> : <Moon size={18} />}
        </IconButton>

        <IconButton
          onClick={handleRedirect}
          label={t('topbar.logout')}
          className="rounded-md text-fg-subtle hover:text-fg hover:bg-raised"
        >
          <LogOut size={18} />
        </IconButton>
      </div>
    </div>
  )
}
