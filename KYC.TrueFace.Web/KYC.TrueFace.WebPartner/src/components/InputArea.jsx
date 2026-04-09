export function InputArea({ label, error, ...props }) {
  return(
    <div className="flex flex-col gap-1.5 w-full">
      {/* Label com tipografia refinada */}
      {label && (
        <label className="text-sm font-medium text-gray-700 dark:text-gray-300">
          {label}
        </label>
      )}
      
      <input
        {...props}
        className={`
          px-4 py-2 rounded-lg border transition-all duration-200 outline-hidden
          /* Estilos base v4 */
          bg-white dark:bg-gray-900 
          border-gray-300 dark:border-gray-700
          text-gray-900 dark:text-gray-100
          
          /* Estados: Focus e Hover */
          hover:border-blue-400
          focus:border-blue-600 focus:ring-2 focus:ring-blue-500/20
          
          /* Estilo de erro */
          ${error ? 'border-red-500 focus:border-red-500 focus:ring-red-500/20' : ''}
          
          /* Placeholder */
          placeholder:text-gray-400
        `}
      />

      {/* Mensagem de Erro */}
      {error && (
        <span className="text-xs text-red-500 font-medium mt-1">
          {error}
        </span>
      )}
    </div>
  )
} 