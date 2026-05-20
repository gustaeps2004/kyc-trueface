import { Input } from "../Input"
import { useNavigate } from 'react-router-dom';
import { Button } from "../Button";
import { loginService } from "../../api/endpoints/loginService";
import { useApi } from "../../hooks/useApi";

export function FormLogin() {
  const { execute, isLoading } = useApi();
  const navigate = useNavigate();

  const handlePostLogin = async (e) => {
    if (e && e.preventDefault)
      e.preventDefault();

    const request = {
      email: document.getElementById("email").value,
      password: document.getElementById("password").value
    };

    await execute(
      () => loginService.postLogin(request),
      {
        onSuccess: (response) => {
          localStorage.setItem('token', response.data.accessToken);
          navigate('/home');
        },
      }
    );
  };

  const handlerRedirectForgotPassword = () => {
    navigate('/forgot-password');
  };

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
            title={isLoading ? "Entrando..." : "Login"}
            disabled={isLoading}
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
  );
}
