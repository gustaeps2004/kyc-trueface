import { LoginBase } from "@/components/login/LoginBase"
import { FormRegisterPassword } from "@/components/login/FormRegisterPassword"
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
