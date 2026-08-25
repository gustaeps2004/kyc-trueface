import { useNavigate } from "react-router-dom"
import { Button } from "@/components/ui/Button"
import { useTranslation } from 'react-i18next';

export function ForgotPasswordConfirmed() {
  const navigate = useNavigate()
  const { t } = useTranslation();

  const handlerRedirectToLogin = () => {
    navigate('/login')
  }

  return(
    <div className="space-y-6">
      <Button
        handlerAction={handlerRedirectToLogin}
        title={t('login.backToLogin')}
      />
    </div>
  )
}
