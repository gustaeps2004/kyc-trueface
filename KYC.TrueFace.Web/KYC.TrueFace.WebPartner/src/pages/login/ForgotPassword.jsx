import { LoginBase } from "../../components/Login/LoginBase"
import { FormForgotPassword } from "../../components/Login/FormForgotPassword"
import { ForgotPasswordConfirmed } from "../../components/Login/ForgotPasswordConfirmed"
import { useState } from "react";

export function ForgotPassword() {
  const [confirmed, setConfirmed] = useState(false)

  const handlerConfirm = () => {
    setConfirmed(true)
  }

  return(
    <LoginBase
      title="Forgot password"
      subtitle={confirmed ? "An email has been sent to change your password" : "We will send you a reset link"}
    >
      { confirmed
        ? <ForgotPasswordConfirmed />
        : <FormForgotPassword handlerConfirmClick={handlerConfirm} />}
    </LoginBase>
  )
}
