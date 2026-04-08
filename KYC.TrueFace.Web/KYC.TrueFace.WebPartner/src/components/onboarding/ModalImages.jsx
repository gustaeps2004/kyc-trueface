import { useEffect, useState } from "react";
import { 
  MoveRight,
  RotateCw,
  Download
} from 'lucide-react';

export function ModalImages(props) {
  const [rotate, setRotate] = useState(90)
  const [onboardingData, setOnboardingData] = useState()
  const [linkImage, setLinkImage] = useState()
  const [imageName, setImageName] = useState()
  
  useEffect(() => {
    setOnboardingData(props.onboardingData)
    
    setLinkImage(props.onboardingData[0].linkImage)
    setImageName(props.onboardingData[0].nameImage)

    const handleEsc = (e) => {
      if (e.key === "Escape") props.closeModal();
    };
  
    window.addEventListener("keydown", handleEsc);
  
    return () => window.removeEventListener("keydown", handleEsc);
  }, []);

  const downloadImage = async () => {
    const response = await fetch(linkImage)
    const blob = await response.blob()
    
    const url = window.URL.createObjectURL(blob);
    
    const link = document.createElement('a');
    link.href = url;
    link.download = imageName
    
    document.body.appendChild(link);
    link.click();

    document.body.removeChild(link);
    window.URL.revokeObjectURL(url);
  }

  const rotateImage = () => {
    setRotate(rotate + 90)

    const img = document.getElementById("image-validate");

    img.style.transition = "transform 0.5s"; 
    img.style.transform = `rotate(${rotate}deg)`;
  }

  const nextImage = () => {
    var indexImage = props
                        .onboardingData
                        .findIndex(x => x.nameImage == imageName)

    if (props.onboardingData[indexImage + 1]?.nameImage != undefined) {
      setLinkImage(props.onboardingData[indexImage + 1].linkImage)
      setImageName(props.onboardingData[indexImage + 1].nameImage)

      return
    }

    setLinkImage(props.onboardingData[0].linkImage)
    setImageName(props.onboardingData[0].nameImage)
  }

  const listIcon = [
    { icon: <Download />, actionAtr: downloadImage },
    { icon: <RotateCw />, actionAtr: rotateImage },
    { icon: <MoveRight />, actionAtr: nextImage }
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
        
        <div
          id="image-validate"  
          className="
          m-3
          flex
          flex-col
          space-y-3
          h-85
          overflow-y-auto
          scrollbar
        ">
         <img
            id="image-container"
            src={linkImage}
            alt={imageName}
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