import { useNavigate } from "react-router-dom"
import { Button } from "../Button"

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
      
      <Button
        handlerAction={handlerRedirectToLogin}
        title="Back to login"
      />
    </div>
  )
}