import { CirclePlus } from 'lucide-react';
import { InputFilter } from '../InputFilter';

export function Content(props) {
  return(
    <div className="
      bg-slate-700/40
      shadow-2xl
      h-full
      rounded-lg
    ">
      <div className="
        p-2
        flex
        flex-row
      ">
        <div className="w-1/2">
          { 
            props.isShowAdd 
            ? <button 
                onClick={props.openModal} 
                className="
                  text-slate-300 
                  hover:text-title 
                  transition"
              >
                <CirclePlus size={28} className="cursor-pointer" />
              </button> 
            : ""
          }
        </div>
        <div className="
          w-1/2 
          flex
          justify-end
          pr-2"
        >
          { props.isShowFilter ? <InputFilter placeholder={props.placeholderFilter} /> : ""}
        </div>
      </div>
      {props.children}
    </div>
  )
}