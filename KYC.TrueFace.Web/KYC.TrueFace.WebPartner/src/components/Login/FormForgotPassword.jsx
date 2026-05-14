import { useNavigate } from 'react-router-dom'
import { Input } from '../Input'
import { Button } from "../Button"

export function FormForgotPassword({ handlerConfirmClick }) {
  const navigate = useNavigate()

  const handlerRedirectToLogin = () => {
    navigate('/login')
  }

  const handlerConfirm = () => {
    handlerConfirmClick()
  }

  return(
    <div>
      <form className="space-y-4">
        <Input type="email" name="email">
          E-mail
        </Input>

        <div className="pt-2">
          <Button
            handlerAction={handlerConfirm}
            title="Confirm"
          />
        </div>
      </form>

      <div className="flex justify-end mt-3">
        <a
          href="#"
          onClick={handlerRedirectToLogin}
          className="
            text-sm
            text-fg-subtle
            hover:text-accent-light
            cursor-pointer
            transition-colors
            duration-150
          "
        >
          Back to login
        </a>
      </div>
    </div>
  )
}
