import { FormLogin } from "../components/FormLogin"
import { LoginBase } from "../components/LoginBase"
import { useTranslation } from 'react-i18next';

export function Login() {
  const { t } = useTranslation();

  return(
    <LoginBase title={t('login.login')}>
      <FormLogin />
    </LoginBase>
  )
}
