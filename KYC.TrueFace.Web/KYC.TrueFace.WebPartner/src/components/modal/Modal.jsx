import { useEffect } from "react";
import { ModalButton } from "./ModalButton";

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
          w-full
        ">
          { 
            props.showRedButton
            ? <div className="ml-auto">
                <ModalButton 
                  title={props.titleRedButton}
                  handlerAction={props.handlerRedAction}
                  bgColor="bg-btn-red"
                  borderColor="border-btn-red"
                />
              </div>
            : null
          }
          
          {
            props.showGreenButton 
            ? <div className="ml-2">
                <ModalButton 
                  title={props.titleGreenButton}
                  handlerAction={props.handlerGreenAction}
                />
              </div> 
            : null
          }
        </div>
        
      </div>
    </div>
  )
}