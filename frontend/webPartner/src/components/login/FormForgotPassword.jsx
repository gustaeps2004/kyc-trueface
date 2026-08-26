import { useNavigate } from 'react-router-dom'
import { Input } from '@/components/ui/Input'
import { Button } from "@/components/ui/Button"
import { useTranslation } from 'react-i18next';
import { loginService } from "@/api/services/loginService";
import { useApi } from "@/hooks/useApi";

export function FormForgotPassword({ handlerConfirmClick }) {
  const navigate = useNavigate()
  const { t } = useTranslation();
  const { execute, isLoading } = useApi();

  const handlerRedirectToLogin = () => {
    navigate('/login')
  }

  const handlerConfirm = async (e) => {
    if (e && e.preventDefault)
      e.preventDefault();

    const request = {
      email: document.getElementById("email").value
    };

    await execute(
      () => loginService.postForgotPassword(request),
      {
        onSuccess: () => handlerConfirmClick(),
      }
    );
  }

  return(
    <div className="flex flex-col gap-4">
      <form className="space-y-3">
        <Input type="email" name="email">
          {t('login.email')}
        </Input>

        <div className="pt-1">
          <Button
            handlerAction={handlerConfirm}
            title={t('login.confirm')}
            disabled={isLoading}
          />
        </div>
      </form>

      <div className="flex justify-end">
        <a
          href="#"
          onClick={handlerRedirectToLogin}
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
          {t('login.backToLogin')}
        </a>
      </div>
    </div>
  )
}
