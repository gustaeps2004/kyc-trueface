import { LogOut } from "lucide-react";
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { Logout } from "@/utils/logout";

export default function Topbar({ name }) {
  const navigate = useNavigate();
  const { t, i18n } = useTranslation();

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
      px-8
    ">
      <h1 className="text-xl text-fg font-medium">
        {name}
      </h1>

      <div className="flex items-center gap-2">
        <button
          title={t('topbar.switchLanguage')}
          onClick={toggleLanguage}
          aria-label={t('topbar.switchLanguage')}
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
            text-lg
            leading-none
          "
        >
          {i18n.language === 'en' ? '🇺🇸' : '🇧🇷'}
        </button>

        <button
          title={t('topbar.logout')}
          onClick={handleRedirect}
          aria-label={t('topbar.logout')}
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
    </div>
  )
}
