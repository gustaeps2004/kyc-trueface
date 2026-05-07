import { Input } from "../Input"
import { useNavigate } from 'react-router-dom';
import { Button } from "../Button";
import { userService } from "../../api/endpoints/loginService";

export function FormLogin() {
  const navigate = useNavigate();

  const handlePostLogin = () => {
    
    const request = {
      email: document.getElementById("email").value,
      password: document.getElementById("password").value
    }

    //navigate('/home');
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

        <Button 
          handlerAction={handlePostLogin}
          title="Login"
        />
        
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
        onClick={() => handlerRedirectForgotPassword}
      >Forgot your password?</a>
    </div>
  )
}