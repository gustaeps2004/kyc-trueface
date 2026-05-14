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
    <LoginBase title="Forgot password">
      <div className="
        w-full
        h-40
        mt-20
      ">
        { confirmed
          ? <ForgotPasswordConfirmed />
          : <FormForgotPassword handlerConfirmClick={handlerConfirm} />}
      </div>
    </LoginBase>
  )
}
