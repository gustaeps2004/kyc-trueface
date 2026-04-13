import { Modal } from "../../components/modal/Modal"

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
      <div className="flex justify-center">
        <h1 className="text-alert-txt text-2xl">
          This action cannot be undone.
        </h1>
      </div>
      <div className="h-full mt-3">
        <textarea 
          rows="10"
          placeholder="Observation"
          className="
            rounded-md
            border 
            border-gray-300
            bg-primary 
            w-full
            text-title
          ">
        </textarea>
      </div>
    </Modal>
  )
}