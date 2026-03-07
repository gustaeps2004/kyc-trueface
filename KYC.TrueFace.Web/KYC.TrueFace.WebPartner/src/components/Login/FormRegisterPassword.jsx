import { Input } from "../Input"
import { Button } from "../Button"
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

        <div className="mt-8">
          <Button
            handlerAction={handlerRedirectToLogin}
            title="Register"
          />
        </div>
      </form>
    </div>
  )
}