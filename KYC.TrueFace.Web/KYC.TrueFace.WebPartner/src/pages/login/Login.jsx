import { FormLogin } from "../../components/Login/FormLogin"

export function Login() {
  return(
    // container principal - ocupa tela inteira
    <div className="bg-gray-100 min-h-screen flex items-center justify-center">

      {/* card login*/}
      <div className="bg-white max-w-md w-full rounded-2xl shadow-xl p-8">
        <h1 className="text-3xl font-bold text-gray-800 text-center mb-6">Bem-vindo</h1>
        <FormLogin />
      </div>
    </div>
  )
}