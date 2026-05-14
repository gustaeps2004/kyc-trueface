import { useState, useRef, useEffect } from "react"
import { ChevronDown } from "lucide-react"

export function Select({
  label,
  options = [],
  value,
  onChange,
  placeholder="Select"
}) {
  const [open, setOpen] = useState(false)
  const [search, setSearch] = useState("")
  const ref = useRef(null)

  const selected = options.find(o => o.value === value)

  const filteredOptions = options.filter(option =>
    option.label.toLowerCase().includes(search.toLowerCase())
  )

  useEffect(() => {
    function handleClickOutside(e) {
      if (ref.current && !ref.current.contains(e.target)) {
        setOpen(false)
      }
    }

    document.addEventListener("mousedown", handleClickOutside)
    return () => document.removeEventListener("mousedown", handleClickOutside)
  }, [])

  return (
    <div className="flex flex-col gap-1.5 w-full relative" ref={ref}>

      {label && (
        <label className="text-xs text-fg-subtle font-medium">
          {label}
        </label>
      )}

      <button
        type="button"
        onClick={() => setOpen(!open)}
        className="
          flex
          items-center
          justify-between
          px-4
          py-2
          rounded-lg
          w-full
          h-11
          bg-surface
          text-fg
          border
          border-divider/60
          transition-all
          duration-200
          hover:border-brand/50
          focus:outline-none
          focus:border-brand
          focus:ring-2
          focus:ring-brand/30
        "
      >
        <span className={selected ? "text-fg" : "text-fg-faint"}>
          {selected ? selected.label : placeholder}
        </span>

        <ChevronDown
          size={16}
          className={`transition-transform duration-200 text-fg-subtle ${open ? "rotate-180" : ""}`}
        />
      </button>

      {open && (
        <div
          className="
            absolute
            top-full
            mt-1.5
            w-full
            rounded-lg
            border
            border-divider/40
            bg-surface
            shadow-xl
            z-50
            overflow-hidden
          "
        >

          <div className="p-2 border-b border-divider/40">
            <input
              type="text"
              placeholder="Search..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              autoFocus
              className="
                w-full
                px-3
                py-1.5
                rounded-md
                bg-base
                text-fg
                placeholder:text-fg-faint
                border
                border-divider/40
                focus:outline-none
                focus:border-brand
                text-sm
              "
            />
          </div>

          <div className="max-h-48 overflow-y-auto scrollbar">
            {filteredOptions.length === 0 ? (
              <div className="px-3 py-3 text-sm text-fg-faint text-center">
                No results
              </div>
            ) : (
              filteredOptions.map(option => (
                <div
                  key={option.value}
                  onClick={() => {
                    onChange(option.value)
                    setOpen(false)
                    setSearch("")
                  }}
                  className={`
                    px-3
                    py-2
                    cursor-pointer
                    text-sm
                    transition-colors
                    duration-150
                    ${option.value === value
                      ? "bg-brand/15 text-brand-soft"
                      : "text-fg-muted hover:bg-raised hover:text-fg"}
                  `}
                >
                  {option.label}
                </div>
              ))
            )}
          </div>
        </div>
      )}
    </div>
  )
}
