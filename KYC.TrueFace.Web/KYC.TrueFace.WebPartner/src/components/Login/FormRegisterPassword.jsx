import { Input } from "../Input"
import { useNavigate, useSearchParams } from 'react-router-dom';

export function FormRegisterPassword() {
  const [searchParams, setSearchParams] = useSearchParams();
  const navigate = useNavigate()
  const email = searchParams.get('e');

  const handlerRedirectToLogin = () => {
    navigate('/login')
  }

  return(
    <div className="    
      w-full
      h-70
      mt-15">
      <form className="space-y-4">
        <Input 
          disabled="true" 
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
          mt-4
          cursor-pointer
          hover:bg-btn-login
          hover:text-title
          transition-colors 
          duration-400"
          onClick={handlerRedirectToLogin}
        >
          Register
        </button>
      </form>
    </div>
  )
}