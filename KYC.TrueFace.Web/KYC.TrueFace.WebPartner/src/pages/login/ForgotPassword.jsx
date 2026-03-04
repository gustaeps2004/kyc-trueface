import { LoginBase } from "../../components/Login/LoginBase"
import { Input } from "../../components/Input"
import { useNavigate } from 'react-router-dom';

export function ForgotPassword() {
  const navigate = useNavigate()

  const handlerRedirectToLogin = () => {
    navigate('/login')
  }

  return(
    <LoginBase title="Forgot password">
      <div className="
        w-full
        h-40
        mt-20
      ">
        <Input type="email" name="email">
          E-mail
        </Input>
        <button type="submit" className="
          bg-primary
          text-btn-login
          border 
          border-solid
          border-btn-login
          font-semibold
          rounded-lg
          mt-5
          py-2
          px-4
          w-full
          cursor-pointer
          hover:bg-btn-login
          hover:text-title
          transition-colors 
          duration-400"
        >
          Confirm
        </button>
        <a href="#" className="
          text-sm
          text-center
          block
          text-title
          cursor-pointer
          hover:underline
          ml-50"
          onClick={handlerRedirectToLogin}
        >Already have an account?</a>
      </div>
    </LoginBase>
  )
}