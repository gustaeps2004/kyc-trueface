import { useNavigate } from 'react-router-dom'
import { Input } from '../Input'
import { Button } from "../Button"
import { useTranslation } from 'react-i18next';

export function FormForgotPassword({ handlerConfirmClick }) {
  const navigate = useNavigate()
  const { t } = useTranslation();

  const handlerRedirectToLogin = () => {
    navigate('/login')
  }

  const handlerConfirm = () => {
    handlerConfirmClick()
  }

  return(
    <div className="flex flex-col gap-4">
      <form className="space-y-3">
        <Input type="email" name="email">
          {t('login.email')}
        </Input>

        <div className="pt-1">
          <Button
            handlerAction={handlerConfirm}
            title={t('login.confirm')}
          />
        </div>
      </form>

      <div className="flex justify-end">
        <a
          href="#"
          onClick={handlerRedirectToLogin}
          className="
            text-sm
            font-medium
            text-brand-soft
            hover:text-brand
            cursor-pointer
            transition-colors
            duration-150
          "
        >
          {t('login.backToLogin')}
        </a>
      </div>
    </div>
  )
}
