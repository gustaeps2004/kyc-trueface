import { useEffect } from "react";
import { Button } from "./Button";

export function Modal(props) {
  useEffect(() => {
    const handleEsc = (e) => {
      if (e.key === "Escape") props.closeModal();
    };
  
    window.addEventListener("keydown", handleEsc);
  
    return () => window.removeEventListener("keydown", handleEsc);
  }, []);

  return(
    <div className="
      fixed 
      inset-0 
      flex 
      items-center 
      justify-center 
      bg-black/50"
    >
      <div className="
        relative 
        bg-primary 
        p-6 
        rounded-lg 
        shadow-lg 
        w-100"
      >
        <span className="
          absolute
          text-slate-300 
          hover:text-title 
          transition
          right-5
          -mt-8
          -mr-3
          text-3xl
          cursor-pointer" 
          onClick={props.closeModal}
        >
          &times;
        </span>

        <div className=" 
          -mt-2
          flex
          justify-center"
        >
          <h2 className="
            text-2xl 
            font-semibold 
            mb-4 
            text-title"
          >
            {props.title}
          </h2>
        </div>
        
        <div className="
          p-4
          flex
          flex-col
          space-y-3
          h-85
          overflow-y-auto
          scrollbar
        ">
          {props.children}
        </div>

        <div className="
          flex
          justify-end
          w-35
          ml-auto
        ">
          <Button 
            title={props.titleButton}
            handlerAction={props.handlerAction}
          />
        </div>
        
      </div>
    </div>
  )
}