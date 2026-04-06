import { useEffect } from "react";
import { 
  MoveRight,
  RotateCw,
  Download
} from 'lucide-react';

export function ModalImages(props) {
  
  useEffect(() => {
    const handleEsc = (e) => {
      if (e.key === "Escape") props.closeModal();
    };
  
    window.addEventListener("keydown", handleEsc);
  
    return () => window.removeEventListener("keydown", handleEsc);
  }, []);

  const downloadImage = () => {
    console.log("downloadImage")
  }

  const rotateImage = () => {
    console.log("rotateImage")
  }

  const nextImage = () => {
    console.log("nextImage")
  }

  const listIcon = [
    { icon: <Download />, actionAtr: downloadImage },
    { icon: <RotateCw />, actionAtr: rotateImage },
    { icon: <MoveRight />, actionAtr: nextImage}
  ]

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
          m-3
          flex
          flex-col
          space-y-3
          h-85
          overflow-y-auto
          scrollbar
        ">
         <img
            src=""
            alt="Exemplo"
            className="w-full h-full object-cover rounded-lg shadow-md"
          />
        </div>

        <div className="
          flex
          justify-center
          w-full
          h-10
          space-x-4
          bg-secondary
          rounded-3xl
        ">
          {
            listIcon.map((iconObj, index) => (
              <button 
                key={index}
                onClick={() => iconObj.actionAtr()} 
                className="
                cursor-pointer  
                text-slate-300 
                hover:text-title 
                transition
                hover:scale-105
              ">
                {iconObj.icon}
              </button>
            ))
          }
        </div>
      </div>
    </div>
  )
}