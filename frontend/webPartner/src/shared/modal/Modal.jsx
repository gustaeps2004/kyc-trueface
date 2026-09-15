import { useEffect } from "react";
import { X } from "lucide-react";
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
        w-110
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
          "
        >
          <X size={18} />
        </button>

        <div className="flex justify-center mb-5">
          <h2 className="text-xl font-medium text-fg">
            {props.title}
          </h2>
        </div>

        <div className="
          flex
          flex-col
          space-y-3
          h-100
          overflow-y-auto
          scrollbar
          pr-1
        ">
          {props.children}
        </div>

        <div className="
          flex
          justify-end
          gap-2
          w-full
          mt-5
          pt-4
          border-t
          border-divider/30
        ">
          {
            props.showRedButton
            ? <ModalButton
                title={props.titleRedButton}
                handlerAction={props.handlerRedAction}
                variant="danger"
                disabled={props.disabled}
              />
            : null
          }

          {
            props.showGreenButton
            ? <ModalButton
                title={props.titleGreenButton}
                handlerAction={props.handlerGreenAction}
                variant={props.greenVariant || "success"}
                disabled={props.disabled}
              />
            : null
          }
        </div>

      </div>
    </div>
  )
}
