import { Input } from "../Input"
import { Button } from "../Button"
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

export function FormRegisterPassword() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate()
  const { t } = useTranslation();
  const email = searchParams.get('e');

  const handlerRedirectToLogin = () => {
    navigate('/login')
  }

  return(
    <form className="space-y-3">
      <Input
        disabled={true}
        type="email"
        name="email"
        value={email}
      >
        {t('login.email')}
      </Input>
      <Input type="password" name="password">
        {t('login.password')}
      </Input>
      <Input type="password" name="confirmPassword">
        {t('login.confirmPassword')}
      </Input>

      <div className="pt-1">
        <Button
          handlerAction={handlerRedirectToLogin}
          title={t('login.register')}
        />
      </div>
    </form>
  )
}
