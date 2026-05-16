import { Input } from "../Input"
import { useNavigate } from 'react-router-dom';
import { Button } from "../Button";
import { userService } from "../../api/endpoints/loginService";
import { useState } from 'react';
import Popup from "../../ui/Popup";

export function FormLogin() {
  const [showPopup, setShowPopup] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
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
      localStorage.setItem('token', response.data.accessToken)
      
      navigate('/home');
    }
    catch (error) {
      setErrorMessage(error.response?.data?.message ?? "A general error occurred. Please try again later.")
      setShowPopup(true)
    }
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
            handlerAction={(e) => handlePostLogin(e)}
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
          {showPopup && (
              <Popup
                  iconColor="text-red-600"
                  message={errorMessage}
                  onClose={() => setShowPopup(false)}
              />
          )}
    </div>
  )
}
