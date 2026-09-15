export function Button(props) {
  const variant = props.variant || "brand"

  const variants = {
    brand:   "bg-brand hover:bg-brand-hover text-white border-brand",
    success: "bg-success hover:brightness-110 text-white border-success",
    danger:  "bg-danger hover:brightness-110 text-white border-danger",
  }

  return(
    <div className="flex justify-center w-full">
      <button
        type="submit"
        onClick={props.handlerAction}
        disabled={props.disabled}
        className={`
          ${variants[variant]}
          flex
          items-center
          justify-center
          font-medium
          py-2
          px-4
          w-full
          min-w-full
          cursor-pointer
          rounded-full
          h-11
          border
          overflow-hidden
          transition-all
          duration-200
          focus:outline-none
          focus:ring-2
          focus:ring-brand/40
          hover:-translate-y-0.5
          active:translate-y-0
          disabled:cursor-not-allowed
          disabled:opacity-50
          disabled:grayscale
          disabled:hover:translate-y-0
        `}
      >
        {props.title}
      </button>
    </div>
  )
}
