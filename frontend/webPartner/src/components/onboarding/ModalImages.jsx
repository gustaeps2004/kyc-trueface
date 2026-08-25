import { useEffect, useState } from "react";
import {
  MoveRight,
  RotateCw,
  Download,
  X,
} from 'lucide-react';

export function ModalImages(props) {
  const [rotate, setRotate] = useState(90)
  const [linkImage, setLinkImage] = useState()
  const [imageName, setImageName] = useState()

  useEffect(() => {
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
    { icon: <RotateCw size={18} />, actionAtr: rotateImage, label: "Rotate" },
    { icon: <Download size={18} />, actionAtr: downloadImage, label: "Download" },
    { icon: <MoveRight size={18} />, actionAtr: nextImage, label: "Next image" },
  ]

  return(
    <div className="
      fixed
      inset-0
      flex
      items-center
      justify-center
      bg-black/60
      backdrop-blur-sm
      z-50
      p-4
    ">
      <div className="
        relative
        bg-surface
        border
        border-divider/40
        p-6
        rounded-2xl
        shadow-2xl
        w-120
      ">
        <button
          onClick={props.closeModal}
          aria-label="Close modal"
          className="
            absolute
            right-4
            top-4
            text-fg-subtle
            hover:text-fg
            hover:bg-raised
            rounded-full
            p-1.5
            transition-all
            duration-150
            cursor-pointer
            z-10
          "
        >
          <X size={18} />
        </button>

        <div className="mb-4">
          <p className="text-xs text-fg-subtle uppercase tracking-wide mb-1">
            Images
          </p>
        </div>

        <div
          id="image-validate"
          className="
            flex
            flex-col
            space-y-3
            h-110
            overflow-hidden
            rounded-lg
            bg-base
            border
            border-divider/30
          "
        >
         <img
            id="image-container"
            src={linkImage}
            alt={imageName}
            className="w-full h-full object-cover"
          />
        </div>

        <div className="
          mt-4
          flex
          justify-center
          gap-2
          bg-base
          border
          border-divider/30
          rounded-full
          p-1.5
        ">
          {listIcon.map((iconObj, index) => (
            <button
              key={index}
              onClick={() => iconObj.actionAtr()}
              aria-label={iconObj.label}
              className="
                flex
                items-center
                justify-center
                w-9
                h-9
                rounded-full
                text-fg-subtle
                hover:text-fg
                hover:bg-raised
                transition-all
                duration-150
                cursor-pointer
              "
            >
              {iconObj.icon}
            </button>
          ))}
        </div>
      </div>
    </div>
  )
}
