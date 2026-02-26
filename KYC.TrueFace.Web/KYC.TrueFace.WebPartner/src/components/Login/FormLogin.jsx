import { Input } from "../Input"

export function FormLogin() {
  return(
    <div>
      <form className="space-y-4">
        <Input name="email" type="email">
            E-mail
        </Input>
        <Input name="password" type="password">
          Password
        </Input>
        <button type="submit" className="
          bg-blue-600
          text-white
          font-semibold

          rounded-lg
          py-2
          px-4
          w-full
        ">Login</button>
      </form>
      <a href="#" className="
        text-sm
        text-center
        block
        text-gray-500
        hover:underline
        mt-6
      ">Forgot password</a>
    </div>
  )
}