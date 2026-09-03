import { Input } from "@/components/ui/Input"
import { Button } from "@/components/ui/Button"
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useState } from "react";
import { useTranslation } from 'react-i18next';
import { loginService } from "@/api/services/loginService";
import { useApi } from "@/hooks/useApi";

export function FormRegisterPassword() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate()
  const { t } = useTranslation();
  const { execute, isLoading } = useApi();

  const email = searchParams.get('e') ?? "";
  const token = searchParams.get('token') ?? "";

  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")

  const handleRegister = async (e) => {
    if (e && e.preventDefault)
      e.preventDefault();

    localStorage.setItem('token', token);

    const request = { email, password, confirmPassword };

    await execute(
      () => loginService.postResetPassword(request),
      {
        showSuccessPopup: true,
        successMessage: t('login.registerPasswordSuccess'),
        onSuccess: () => navigate('/login'),
      }
    );

    localStorage.removeItem('token');
  }

  return(
    <form className="space-y-3">
      <Input
        disabled={true}
        type="email"
        name="email"
        value={email}
      >
        {t('login.email')}
      </Input>
      <Input type="password" name="password" value={password} onChange={setPassword}>
        {t('login.password')}
      </Input>
      <Input type="password" name="confirmPassword" value={confirmPassword} onChange={setConfirmPassword}>
        {t('login.confirmPassword')}
      </Input>

      <div className="pt-1">
        <Button
          handlerAction={(e) => handleRegister(e)}
          title={isLoading ? t('login.registering') : t('login.register')}
          disabled={isLoading}
        />
      </div>
    </form>
  )
}
