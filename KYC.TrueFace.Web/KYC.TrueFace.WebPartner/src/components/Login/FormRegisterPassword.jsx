import { Input } from "../Input"
import { Button } from "../Button"
import { useNavigate, useSearchParams } from 'react-router-dom';

export function FormRegisterPassword() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate()
  const email = searchParams.get('e');

  const handlerRedirectToLogin = () => {
    navigate('/login')
  }

  return(
    <form className="space-y-3">
      <Input
        disabled={true}
        type="email"
        name="email"
        value={email}
      >
        E-mail
      </Input>
      <Input type="password" name="password">
        Password
      </Input>
      <Input type="password" name="confirmPassword">
        Confirm password
      </Input>

      <div className="pt-1">
        <Button
          handlerAction={handlerRedirectToLogin}
          title="Register"
        />
      </div>
    </form>
  )
}
