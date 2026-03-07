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

        <Button
          handlerAction={handlerConfirm}
          title="Confirm"
        />
      </form>
      <a href="#" className="
        text-sm
        text-center
        block
        text-title
        cursor-pointer
        hover:underline
        ml-68"
        onClick={handlerRedirectToLogin}
      >Back to login</a>
    </div>
  )
}