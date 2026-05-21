import { Modal } from "../../components/modal/Modal"
import { Input } from '@/components/ui/Input'
import { useEffect } from "react";
import { useTranslation } from 'react-i18next';
import { DateFormat } from "@/utils/formats";
import { Situation } from "@/utils/arrays";

export function OnboardingAnalysed(props) {
  const { t } = useTranslation();

  useEffect(() => {
    document.getElementById('name').value = props.onboardingData.name
    document.getElementById('txAreaObservation').value = props.onboardingData.observation
    document.getElementById('situation').value = t(Situation.find(x => x.value == props.onboardingData.situation)?.labelKey)
    document.getElementById('dtSituation').value = DateFormat(props.onboardingData.inclusionDate)
  }, []);

  return(
    <Modal
      title={t('history.analysed')}
      closeModal={props.closeModal}
    >
      <Input type="name" name="name" disabled={true}>
        {t('history.name')}
      </Input>
      <Input type="situation" name="situation" disabled={true}>
        {t('history.situation')}
      </Input>
      <Input type="dtSituation" name="dtSituation" disabled={true}>
        {t('history.date')}
      </Input>
      <div>
        <label className="block text-xs text-fg-subtle font-medium mb-1.5">
          {t('history.observation')}
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
