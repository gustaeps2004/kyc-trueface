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
    <div className="flex flex-col gap-4">
      <form className="space-y-3">
        <Input type="email" name="email">
          E-mail
        </Input>
        <Input type="password" name="password">
          Password
        </Input>

        <div className="pt-1">
          <Button
            handlerAction={handleRedirectHome}
            title="Login"
          />
        </div>
      </form>

      <div className="flex justify-end">
        <a
          href="#"
          onClick={handlerRedirectForgotPassword}
          className="
            text-sm
            font-medium
            text-brand-soft
            hover:text-brand
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
