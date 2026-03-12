import { ApplyMask } from "../utils/Mask"

export function Input(props) {
  
  function handleChange(e) {
    let newValue = e.target.value

    if (props.mask) 
      newValue = ApplyMask(newValue, props.mask)

    if (props.onChange)
      props.onChange(newValue)
  }

  return(
    <div className="relative w-full">
      <input
        type={props.type}
        id={props.name}
        disabled={props.disabled}
        value={props.value}
        onChange={handleChange}
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
          focus:outline-none"
      />

      <label
        htmlFor={props.name}
        className="
          absolute 
          left-3 
          top-0
          text-btn-login
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