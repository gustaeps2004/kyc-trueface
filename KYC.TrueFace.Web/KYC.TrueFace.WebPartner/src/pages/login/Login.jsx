import { FormLogin } from "../../components/Login/FormLogin"
import { LoginBase } from "../../components/Login/LoginBase"

export function Login() {
  return(
    <LoginBase title="Login">
      <FormLogin />
    </LoginBase>
  )
}