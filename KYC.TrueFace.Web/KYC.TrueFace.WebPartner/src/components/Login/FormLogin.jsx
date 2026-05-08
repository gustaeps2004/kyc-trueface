import { Input } from "../Input"
import { useNavigate } from 'react-router-dom';
import { Button } from "../Button";
import { userService } from "../../api/endpoints/loginService";
import { useState } from 'react';
import Popup from "../../ui/Popup";

export function FormLogin() {
  const [showPopup, setShowPopup] = useState(false);
  const navigate = useNavigate();

  const handlePostLogin = async (e) => {
    if (e && e.preventDefault) 
      e.preventDefault();

    const request = {
      email: document.getElementById("email").value,
      password: document.getElementById("password").value
    }

    try {
      var response = await userService.postLogin(request)
      console.log(response.data)
    }
    catch (error) {
      setShowPopup(true)
      console.error('Erro no login:', error.response?.data?.message);
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
          handlerAction={(e) => handlePostLogin(e)}
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

      {showPopup && (
        <Popup 
          message="Sua ação foi concluída com sucesso!" 
          onClose={() => setShowPopup(false)} 
        />
      )}
    </div>
  )
}