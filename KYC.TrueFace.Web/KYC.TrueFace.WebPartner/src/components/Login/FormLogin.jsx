import { Input } from "../Input"
import { useNavigate } from 'react-router-dom';
import { Button } from "../Button";

export function FormLogin() {
  const navigate = useNavigate();

  const handleRedirectHome = () => {
    navigate('/home');
  };

  const handlerRedirectForgotPassword = () => {
    navigate('/forgot-password')
  }

  return(
    <div>
      <form className="space-y-4">

        <Input type="email" name="email">
          E-mail
        </Input>
        <Input type="password" name="password">
          Password
        </Input>

        <div className="pt-2">
          <Button
            handlerAction={handleRedirectHome}
            title="Login"
          />
        </div>

      </form>

      <div className="flex justify-end mt-3">
        <a
          href="#"
          onClick={handlerRedirectForgotPassword}
          className="
            text-sm
            text-fg-subtle
            hover:text-accent-light
            cursor-pointer
            transition-colors
            duration-150
          "
        >
          Forgot your password?
        </a>
      </div>
    </div>
  )
}
