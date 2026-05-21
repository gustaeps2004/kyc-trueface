import { Search } from "lucide-react"

export function InputFilter(props) {
  return(
    <div className="relative w-1/2">
      <Search
        size={16}
        className="
          absolute
          left-3
          top-1/2
          -translate-y-1/2
          text-fg-faint
          pointer-events-none
        "
      />
      <input
        type="text"
        placeholder={props.placeholder}
        autoComplete="off"
        className="
          w-full
          rounded-lg
          bg-base/60
          border
          border-divider/40
          text-fg
          placeholder:text-fg-faint
          pl-9
          pr-3
          py-2
          text-sm
          transition-all
          duration-200
          focus:outline-none
          focus:border-brand
          focus:ring-2
          focus:ring-brand/30
        "
      />
    </div>
  )
}
