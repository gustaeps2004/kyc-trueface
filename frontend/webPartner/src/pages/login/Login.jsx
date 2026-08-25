import { FormLogin } from "@/components/login/FormLogin"
import { LoginBase } from "@/components/login/LoginBase"
import { useTranslation } from 'react-i18next';

export function Login() {
  const { t } = useTranslation();

  return(
    <LoginBase title={t('login.login')}>
      <FormLogin />
    </LoginBase>
  )
}
