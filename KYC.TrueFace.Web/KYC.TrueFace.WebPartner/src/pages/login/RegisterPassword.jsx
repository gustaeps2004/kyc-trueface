import { LoginBase } from "../../components/Login/LoginBase"
import { FormRegisterPassword } from "../../components/Login/FormRegisterPassword"

export function RegisterPassword() {
  return(
    <LoginBase title="Register password">
      <FormRegisterPassword />
    </LoginBase>
  )
}