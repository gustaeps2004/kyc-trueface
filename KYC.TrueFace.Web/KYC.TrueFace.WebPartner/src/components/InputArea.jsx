export function InputArea({ label, error, ...props }) {
  return(
    <div className="flex flex-col gap-1.5 w-full">
      {label && (
        <label className="text-xs font-medium text-fg-subtle">
          {label}
        </label>
      )}

      <input
        {...props}
        className={`
          px-4 py-2 rounded-lg border transition-all duration-200 outline-hidden
          bg-surface
          text-fg
          placeholder:text-fg-faint

          ${error
            ? "border-danger focus:border-danger focus:ring-2 focus:ring-danger/30"
            : "border-divider/60 hover:border-brand/50 focus:border-brand focus:ring-2 focus:ring-brand/30"}
        `}
      />

      {error && (
        <span className="text-xs text-danger-light font-medium mt-1">
          {error}
        </span>
      )}
    </div>
  )
}
