import { Input } from "../Input"
import { useNavigate } from 'react-router-dom';

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

        <button type="submit" className="
          bg-primary
          text-btn-login
          border 
          border-solid
          border-btn-login
          font-semibold
          rounded-lg
          py-2
          px-4
          w-full
          cursor-pointer
          hover:bg-btn-login
          hover:text-title
          transition-colors 
          duration-400"
        onClick={handleRedirectHome}
        >
          Login
        </button>
      </form>
      <a href="#" className="
        text-sm
        text-center
        block
        text-title
        cursor-pointer
        hover:underline
        mt-0
        ml-55"
        onClick={handlerRedirectForgotPassword}
      >Forgot your password?</a>
    </div>
  )
}