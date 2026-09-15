import { ShieldCheck } from "lucide-react"
import { useTranslation } from 'react-i18next';

export function LoginBase(props) {
  const { t, i18n } = useTranslation();

  const toggleLanguage = () => {
    const next = i18n.language === 'en' ? 'pt' : 'en';
    i18n.changeLanguage(next);
    localStorage.setItem('language', next);
  };

  return(
    <div className="
      relative
      min-h-screen
      bg-base
      flex
      items-stretch
    ">
      <button
        title={t('topbar.switchLanguage')}
        onClick={toggleLanguage}
        aria-label={t('topbar.switchLanguage')}
        className="
          absolute
          top-4
          right-4
          z-10
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
      <div className="
        hidden
        lg:flex
        relative
        flex-1
        flex-col
        justify-between
        p-12
        overflow-hidden
        bg-linear-to-br
        from-indigo-950
        via-slate-900
        to-slate-950
      ">
        <div
          aria-hidden="true"
          className="
            absolute
            inset-0
            opacity-40
            pointer-events-none
          "
          style={{
            backgroundImage: "radial-gradient(circle at 1px 1px, rgba(165, 180, 252, 0.15) 1px, transparent 0)",
            backgroundSize: "24px 24px"
          }}
        />

        <div
          aria-hidden="true"
          className="
            absolute
            -top-20
            -right-20
            w-80
            h-80
            bg-indigo-500
            rounded-full
            blur-3xl
            opacity-30
            pointer-events-none
          "
        />

        <div
          aria-hidden="true"
          className="
            absolute
            -bottom-32
            -left-20
            w-96
            h-96
            bg-cyan-500
            rounded-full
            blur-3xl
            opacity-15
            pointer-events-none
          "
        />

        <div className="relative flex items-center gap-3">
          <div className="
            w-11
            h-11
            rounded-xl
            bg-indigo-500/20
            border
            border-indigo-300/30
            flex
            items-center
            justify-center
            backdrop-blur-sm
          ">
            <ShieldCheck size={22} className="text-indigo-300" />
          </div>
          <span className="text-lg font-medium text-white tracking-tight">
            KYC TrueFace
          </span>
        </div>

        <div className="relative max-w-md">
          <h2 className="
            text-4xl
            font-medium
            text-white
            leading-tight
            tracking-tight
            mb-4
          ">
            {t('login.heroTitleStart')}{" "}
            <span className="text-indigo-300">{t('login.heroTitleHighlight')}</span>.
          </h2>
          <p className="text-base text-slate-300 leading-relaxed">
            {t('login.heroSubtitle')}
          </p>
        </div>
        <div className="relative"></div>
      </div>

      <div className="
        flex-1
        flex
        items-center
        justify-center
        px-6
        py-10
        lg:max-w-xl
      ">
        <div className="w-full max-w-sm flex flex-col gap-6">

          <div className="lg:hidden flex items-center justify-center gap-2 mb-2">
            <div className="
              w-9
              h-9
              rounded-lg
              bg-brand/10
              flex
              items-center
              justify-center
            ">
              <ShieldCheck size={18} className="text-brand" />
            </div>
            <span className="text-sm font-medium text-fg">
              KYC TrueFace
            </span>
          </div>

          <div className="flex flex-col gap-1">
            <h1 className="text-2xl font-medium text-fg">
              {props.title}
            </h1>
            <p className="text-sm text-fg-subtle">
              {props.subtitle !== undefined
                ? props.subtitle
                : t('login.signInToAccount')}
            </p>
          </div>

          {props.children}
        </div>
      </div>
    </div>
  )
}
