import { AlertCircle } from "lucide-react"
import { useTranslation } from 'react-i18next';

export function MobileAcess() {
  const { t } = useTranslation();

  return(
    <div className="
      flex
      min-h-screen
      items-center
      justify-center
      bg-base
      p-6
      text-center
    ">
      <div className="
        max-w-md
        rounded-2xl
        bg-surface
        border
        border-divider/40
        p-8
        shadow-2xl
      ">
        <div className="
          mx-auto
          mb-4
          flex
          items-center
          justify-center
          w-14
          h-14
          rounded-full
          bg-danger/15
        ">
          <AlertCircle size={28} className="text-danger-light" />
        </div>
        <h1 className="text-xl font-medium text-fg">{t('mobile.restrictedAccess')}</h1>
        <p className="mt-3 text-sm text-fg-muted leading-relaxed">
          {t('mobile.notAvailable')}
        </p>
      </div>
    </div>
  )
}
