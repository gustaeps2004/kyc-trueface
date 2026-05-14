import { Modal } from "../../components/modal/Modal"
import { Input } from '../../components/Input'
import { useEffect } from "react";
import { DateFormat } from "../../utils/functions/Formats";
import { Situation } from "../../utils/Arrays";

export function OnboardingAnalysed(props) {
  useEffect(() => {
    document.getElementById('name').value = props.onboardingData.name
    document.getElementById('txAreaObservation').value = props.onboardingData.observation
    document.getElementById('situation').value = Situation.find(x => x.value == props.onboardingData.situation)?.label
    document.getElementById('dtSituation').value = DateFormat(props.onboardingData.inclusionDate)
  }, []);

  return(
    <Modal
      title="Analysed"
      closeModal={props.closeModal}
    >
      <Input type="name" name="name" disabled={true}>
        Name
      </Input>
      <Input type="situation" name="situation" disabled={true}>
        Situation
      </Input>
      <Input type="dtSituation" name="dtSituation" disabled={true}>
        Date
      </Input>
      <div>
        <label className="block text-xs text-fg-subtle font-medium mb-1.5">
          Observation
        </label>
        <textarea
          id="txAreaObservation"
          rows="8"
          disabled
          className="
            w-full
            rounded-lg
            border
            border-divider/60
            bg-base/80
            text-fg-muted
            px-4
            py-3
            text-sm
            resize-none
            opacity-80
            cursor-not-allowed
            focus:outline-none
          "
        />
      </div>
    </Modal>
  )
}
