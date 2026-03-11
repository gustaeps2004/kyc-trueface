export function ModalButton({
  title,
  handlerAction,
  bgColor = "bg-btn-login",
  textColor = "text-title",
  borderColor = "border-btn-login"
}) {
  return(
    <button type="submit" className={`
      ${bgColor}
      ${textColor}
      ${borderColor}
      flex
      items-center
      justify-center
      font-semibold
      py-2
      px-4
      w-20
      cursor-pointer
      rounded-full 
      h-8 
      overflow-hidden 
      focus:brightness-110 
      transition-all 
      hover:scale-90
      border-2! 
      border-brand-primary! 
      transparent`}
    onClick={handlerAction}
    >
      {title}
    </button>
  )
}