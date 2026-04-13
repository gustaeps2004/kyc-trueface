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
        <textarea 
          id="txAreaObservation"
          rows="10"
          disabled
          className="
            rounded-md
            border 
            border-gray-300
            bg-primary 
            w-full
            text-title
            focus:border-gray-300
          ">
        </textarea>
      </div>
    </Modal>
  )
}