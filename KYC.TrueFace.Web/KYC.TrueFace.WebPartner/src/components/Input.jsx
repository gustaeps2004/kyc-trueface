export function Input(props) {
  return(
    <div>
      <label htmlFor={props.name} className="
        block
        text-sm
        font-medium
        text-gray-600
        mb-1
      ">{props.children}</label>
      <input id={props.name} type={props.type} className="
        border
        border-gray-300
        w-full
        px-4
        py-2
        rounded-lg
        focus:outline-none
        focus:ring-2
        focus:ring-blue-500
        focus:border-transparent
        transition
      "/>
    </div>
  )
}