import { LoginBase } from "../components/LoginBase"
import { FormRegisterPassword } from "../components/FormRegisterPassword"
import { useTranslation } from 'react-i18next';

export function RegisterPassword() {
  const { t } = useTranslation();

  return(
    <LoginBase
      title={t('login.registerPasswordTitle')}
      subtitle={t('login.registerPasswordSubtitle')}
    >
      <FormRegisterPassword />
    </LoginBase>
  )
}
