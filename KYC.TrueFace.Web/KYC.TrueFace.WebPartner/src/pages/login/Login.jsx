import { FormLogin } from "../../components/Login/FormLogin"

export function Login() {
  return(
    <div className="
      bg-gray-100 
      min-h-screen 
      flex items-center 
      justify-center"
    >
      <div className="
        bg-primary 
        max-w-md 
        w-full 
        rounded-2xl 
        shadow-xl 
        p-8"
      >
        <h1 className="
          text-3xl 
          font-bold
          text-title
          text-center 
          mb-6"
        >
          Login</h1>
        <FormLogin />
      </div>
    </div>
  )
}