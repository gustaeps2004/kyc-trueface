export function Button(props) {
  return(
    <button type="submit" className="
        bg-primary
        text-btn-login
        border 
        border-solid
        border-btn-login
        font-semibold
        rounded-lg
        py-2
        px-4
        w-full
        cursor-pointer
        hover:bg-btn-login
        hover:text-title
        transition-colors 
        duration-400"
      onClick={props.handlerAction}
      >
        {props.title}
    </button>
  )
}