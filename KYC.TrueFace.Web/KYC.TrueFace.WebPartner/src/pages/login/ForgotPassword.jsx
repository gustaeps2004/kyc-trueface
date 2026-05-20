import { LoginBase } from "../../components/Login/LoginBase"
import { FormForgotPassword } from "../../components/Login/FormForgotPassword"
import { ForgotPasswordConfirmed } from "../../components/Login/ForgotPasswordConfirmed"
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
