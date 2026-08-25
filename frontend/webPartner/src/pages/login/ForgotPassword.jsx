import { LoginBase } from "@/components/login/LoginBase"
import { FormForgotPassword } from "@/components/login/FormForgotPassword"
import { ForgotPasswordConfirmed } from "@/components/login/ForgotPasswordConfirmed"
import { useState } from "react";
import { useTranslation } from 'react-i18next';

export function ForgotPassword() {
  const [confirmed, setConfirmed] = useState(false)
  const { t } = useTranslation();

  const handlerConfirm = () => {
    setConfirmed(true)
  }

  return(
    <LoginBase
      title={t('login.forgotPasswordTitle')}
      subtitle={confirmed ? t('login.resetLinkSent') : t('login.resetLinkDescription')}
    >
      { confirmed
        ? <ForgotPasswordConfirmed />
        : <FormForgotPassword handlerConfirmClick={handlerConfirm} />}
    </LoginBase>
  )
}
