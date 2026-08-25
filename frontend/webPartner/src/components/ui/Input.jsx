import { ApplyMask } from '@/utils/mask'

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
          rounded-lg
          border
          border-divider/60
          bg-surface
          px-4
          pt-5
          pb-2
          text-sm
          text-fg
          transition-all
          duration-200
          focus:outline-none
          focus:border-brand
          focus:ring-2
          focus:ring-brand/30
          disabled:opacity-60
          disabled:cursor-not-allowed
        "
      />

      <label
        htmlFor={props.name}
        className="
          absolute
          left-4
          top-1
          text-xs
          text-fg-subtle
          transition-all
          duration-200
          pointer-events-none
          peer-placeholder-shown:top-3.5
          peer-placeholder-shown:text-sm
          peer-placeholder-shown:text-fg-faint
          peer-focus:top-1
          peer-focus:text-xs
          peer-focus:text-brand-soft
        "
      >
        {props.children}
      </label>
    </div>
  )
}
