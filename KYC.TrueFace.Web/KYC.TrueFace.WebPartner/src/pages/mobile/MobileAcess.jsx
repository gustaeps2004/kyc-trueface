export function MobileAcess() {
  return(
    <div className="
      flex 
      min-h-screen 
      items-center 
      justify-center 
      bg-primary
      p-6 
      text-center"
    >
      <div className="
        max-w-md 
        rounded-2xl 
        bg-white 
        p-8 
        shadow-xl"
      >
        <svg className="
          mx-auto 
          mb-4 
          size-12 
          text-red-500" 
          fill="none" 
          viewBox="0 0 24 24" 
          stroke="currentColor"
        >
          <path 
            strokeLinecap="round" 
            strokeLinejoin="round" 
            strokeWidth={2} 
            d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
        </svg>
        <h1 className="text-2xl font-bold text-gray-900">Restricted Access</h1>
        <p className="mt-3 text-gray-600">
          This application is not available for mobile devices. Please access it via a computer.
        </p>
      </div>
    </div>
  )
}