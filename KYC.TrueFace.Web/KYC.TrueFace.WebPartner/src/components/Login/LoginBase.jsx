import ImgLogin from "../../assets/imgs/login.png"

export function LoginBase(props) {
	return(
    <div className="
      min-h-screen
      bg-primary
      flex
      items-center
      justify-center
      px-10"
    >
      <div className="
        flex 
        items-center 
        gap-20 
        max-w-7xl 
        w-full"
      >
        <div className="flex-1">
          <img 
            src={ImgLogin} 
            alt="login" 
            className="
              w-full
              max-h-150
              h-[85vh] 
							object-contain"
          />
        </div>
        <div className="
          w-full
          max-w-md
          bg-primary
          rounded-2xl
          shadow-2xl
          p-10"
        >
          <h1 className="
            text-3xl
            font-bold
            text-title
            text-center
            mb-8"
          >
            {props.title}
          </h1>
          {props.children}
        </div>
      </div>
    </div>
  )
}