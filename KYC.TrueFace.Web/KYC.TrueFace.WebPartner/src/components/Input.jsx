export function Input(props) {
  return(
    <div className="relative w-full">
      <input
        type={props.type}
        id={props.name}
        placeholder=" "
        className="
          peer 
          w-full 
          border 
          border-gray-300
          rounded-md
          px-4
          py-2
          pt-5 
          pb-2 
          text-sm
          text-title
          focus:outline-none "
      />

      <label
        htmlFor={props.name}
        className="
          absolute 
          left-3 
          top-0
          text-title 
          text-sm
          transition-all
          peer-placeholder-shown:top-3.5
          peer-placeholder-shown:text-base
          peer-placeholder-shown:text-title
          peer-focus:top-0
          peer-focus:text-sm
        "
      >
        {props.children}
      </label>
    </div>
  )
}