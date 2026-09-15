export function IconButton({ label, onClick, className = "", children, type = "button", ...rest }) {
  return (
    <button
      type={type}
      onClick={onClick}
      aria-label={label}
      title={label}
      className={`
        inline-flex
        items-center
        justify-center
        h-11
        w-11
        transition-all
        duration-150
        cursor-pointer
        focus:outline-none
        focus:ring-2
        focus:ring-brand/40
        ${className}
      `}
      {...rest}
    >
      {children}
    </button>
  );
}
