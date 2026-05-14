import { Modal } from "../../components/modal/Modal"
import { AlertTriangle } from "lucide-react"

export function OnboardingAnalyse(props) {
  return(
    <Modal
      title="Analyse"
      closeModal={props.closeModal}
      showRedButton={true}
      titleRedButton="Deny"
      handlerRedAction={() => console.log("REPROVADO")}
      showGreenButton={true}
      titleGreenButton="Approve"
      handlerGreenAction={() => console.log("APROVADO")}
    >
      <div className="
        flex
        items-center
        justify-center
        gap-2
        bg-warning/10
        border
        border-warning/30
        rounded-lg
        px-4
        py-3
      ">
        <AlertTriangle size={18} className="text-warning-light shrink-0" />
        <p className="text-warning-light text-sm font-medium">
          This action cannot be undone.
        </p>
      </div>

      <div className="h-full mt-3">
        <textarea
          rows="10"
          placeholder="Observation"
          className="
            w-full
            rounded-lg
            border
            border-divider/60
            bg-base
            text-fg
            placeholder:text-fg-faint
            px-4
            py-3
            text-sm
            resize-none
            transition-all
            duration-200
            focus:outline-none
            focus:border-brand
            focus:ring-2
            focus:ring-brand/30
          "
        />
      </div>
    </Modal>
  )
}
