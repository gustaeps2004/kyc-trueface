import { Modal } from "../../components/modal/Modal";

export function OnboardingAnalyse(props) {
  return(
    <Modal
      title="Analyse"
      closeModal={props.closeModal}
      showRedButton={true}
      titleRedButton="Reprove"
      handlerRedAction={() => console.log("REPROVADO")}
      showGreenButton={true}
      titleGreenButton="Approve"
      handlerGreenAction={() => console.log("APROVADO")}
    >
      <div>
        <h1 className="text-alert-txt">
          This action cannot be undone.
        </h1>
      </div>
      <div className="h-full">
        INPUT AREA
      </div>
    </Modal>
  )
}