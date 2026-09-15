import { LoginBase } from "../components/LoginBase"
import { FormForgotPassword } from "../components/FormForgotPassword"
import { ForgotPasswordConfirmed } from "../components/ForgotPasswordConfirmed"
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
