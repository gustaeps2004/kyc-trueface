import { Input } from "@/components/ui/Input"
import { useNavigate } from 'react-router-dom';
import { Button } from "@/components/ui/Button";
import { loginService } from "@/api/services/loginService";
import { useApi } from "@/hooks/useApi";
import { useState } from "react";
import { useTranslation } from 'react-i18next';

export function FormLogin() {
  const { execute, isLoading } = useApi();
  const navigate = useNavigate();
  const { t } = useTranslation();
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const handlePostLogin = async (e) => {
    if (e && e.preventDefault)
      e.preventDefault();

    const request = {
      email,
      password
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
        <Input type="email" name="email" value={email} onChange={setEmail}>
          {t('login.email')}
        </Input>
        <Input type="password" name="password" value={password} onChange={setPassword}>
          {t('login.password')}
        </Input>

        <div className="pt-1">
          <Button
            handlerAction={(e) => handlePostLogin(e)}
            title={isLoading ? t('login.signingIn') : t('login.login')}
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
          {t('login.forgotPassword')}
        </a>
      </div>
    </div>
  );
}
