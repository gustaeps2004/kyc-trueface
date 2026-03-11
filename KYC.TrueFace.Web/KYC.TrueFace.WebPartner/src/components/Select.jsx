import { useState, useRef, useEffect } from "react"

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
    <div className="flex flex-col gap-1 w-full relative" ref={ref}>

      {label && (
        <label className="text-sm text-title font-semibold">
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
          px-3
          py-2
          rounded-md
          w-full
          h-11
          bg-primary
          text-title
          border
          border-title
          hover:brightness-110
          transition"
      >
        {selected ? selected.label : placeholder}

        <span className={`transition ${open ? "rotate-180" : ""}`}>
          ▼
        </span>
      </button>

      {open && (
        <div
          className="
            absolute
            top-full
            mt-1
            w-full
            rounded-md
            border
            border-secondary
            bg-primary
            shadow-lg
            z-50"
        >

          <div className="p-2 border-b border-secondary">
            <input
              type="text"
              placeholder="Search..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              className="
                w-full
                px-2
                py-1
                rounded
                bg-secondary
                text-title
                outline-none
                text-sm
              "
            />
          </div>

          <div className="max-h-48 overflow-y-auto">
            {filteredOptions.map(option => (
              <div
                key={option.value}
                onClick={() => {
                  onChange(option.value)
                  setOpen(false)
                  setSearch("")
                }}
                className="
                  px-3
                  py-2
                  cursor-pointer
                  text-title
                  hover:bg-slate-700
                  transition
                "
              >
                {option.label}
              </div>
            ))}
          </div>
        </div>
      )}
    </div>
  )
}