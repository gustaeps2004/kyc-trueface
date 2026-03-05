import { useNavigate } from "react-router-dom"

export function ForgotPasswordConfirmed() {
  const navigate = useNavigate()

  const handlerRedirectToLogin = () => {
    navigate('/login')
  }

  return(
    <div className="space-y-5">
      <h2 className="
        -mt-8
        text-title
        text-lg
      ">
        An email has been sent to change your password.
      </h2>
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
          onClick={handlerRedirectToLogin}
        >
          Back to login
        </button>
    </div>
  )
}