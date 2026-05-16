import { useNavigate } from "react-router-dom"
import { Button } from "../Button"

export function ForgotPasswordConfirmed() {
  const navigate = useNavigate()

  const handlerRedirectToLogin = () => {
    navigate('/login')
  }

  return(
    <div className="space-y-6">
      <Button
        handlerAction={handlerRedirectToLogin}
        title="Back to login"
      />
    </div>
  )
}
