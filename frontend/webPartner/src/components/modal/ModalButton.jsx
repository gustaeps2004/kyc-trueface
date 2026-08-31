export function ModalButton({
  title,
  handlerAction,
  variant = "success",
  disabled = false
}) {
  const variants = {
    success: "bg-success hover:brightness-110 text-white",
    danger:  "bg-danger hover:brightness-110 text-white",
    brand:   "bg-brand hover:bg-brand-hover text-white",
  }

  return(
    <button
      type="submit"
      onClick={handlerAction}
      disabled={disabled}
      className={`
        ${variants[variant]}
        flex
        items-center
        justify-center
        font-medium
        px-5
        min-w-20
        h-9
        text-sm
        cursor-pointer
        rounded-full
        transition-all
        duration-200
        focus:outline-none
        focus:ring-2
        focus:ring-white/30
        hover:-translate-y-0.5
        active:translate-y-0
        disabled:cursor-not-allowed
        disabled:opacity-50
        disabled:grayscale
        disabled:hover:translate-y-0
      `}
    >
      {title}
    </button>
  )
}
