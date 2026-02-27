import { Input } from "../Input"

export function FormLogin() {
  return(
    <div>
      <form className="space-y-4">
        
        <Input type="email">
          E-mail
        </Input>
        <Input type="password">
          Password
        </Input>

        <button type="submit" className="
          bg-btn-login
          text-title
          font-semibold
          rounded-lg
          py-2
          px-4
          w-full
          cursor-pointer
          hover:bg-transparent
          transition-colors duration-200
        ">Login</button>
      </form>
      <a href="#" className="
        text-sm
        text-center
        block
        text-title
        cursor-pointer
        hover:underline
        mt-0
        ml-55
      ">Forgot your password?</a>
    </div>
  )
}