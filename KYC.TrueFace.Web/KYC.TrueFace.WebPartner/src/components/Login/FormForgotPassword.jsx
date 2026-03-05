import { useNavigate } from 'react-router-dom'
import { Input } from '../Input'

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
        <button type="submit" className="
          bg-primary
          text-btn-login
          border 
          border-solid
          border-btn-login
          font-semibold
          rounded-lg
          py-2
          px-4
          w-full
          cursor-pointer
          hover:bg-btn-login
          hover:text-title
          transition-colors 
          duration-400"
          onClick={handlerConfirm}
        >
          Confirm
        </button>
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