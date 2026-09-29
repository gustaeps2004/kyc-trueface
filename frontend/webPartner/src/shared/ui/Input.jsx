import { useState } from "react"
import { Eye, EyeOff } from "lucide-react"
import { useTranslation } from 'react-i18next';
import { ApplyMask } from '@/shared/utils/mask'

export function Input(props) {
  const { t } = useTranslation();
  const [showPassword, setShowPassword] = useState(false)

  const isPassword = props.type === "password"
  const inputType = isPassword && showPassword ? "text" : props.type

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
        type={inputType}
        id={props.name}
        disabled={props.disabled}
        value={props.value}
        onChange={handleChange}
        placeholder=" "
        className={`
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
          ${isPassword ? "pr-11" : ""}
        `}
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

      {isPassword && (
        <button
          type="button"
          onClick={() => setShowPassword((prev) => !prev)}
          disabled={props.disabled}
          aria-label={showPassword ? t('input.hidePassword') : t('input.showPassword')}
          aria-pressed={showPassword}
          title={showPassword ? t('input.hidePassword') : t('input.showPassword')}
          className="
            absolute
            right-2
            top-1/2
            -translate-y-1/2
            inline-flex
            items-center
            justify-center
            h-8
            w-8
            rounded-md
            text-fg-faint
            hover:text-fg
            cursor-pointer
            transition-colors
            duration-150
            focus:outline-none
            focus:ring-2
            focus:ring-brand/40
            disabled:cursor-not-allowed
            disabled:opacity-60
          "
        >
          {showPassword ? <EyeOff size={18} /> : <Eye size={18} />}
        </button>
      )}
    </div>
  )
}
