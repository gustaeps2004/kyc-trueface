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
            handlerAction={(e) => handlePostLogin(e)}
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
