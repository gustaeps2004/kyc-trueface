import { Modal } from "../../components/modal/Modal"
import { Input } from '@/components/ui/Input'
import { useTranslation } from 'react-i18next';
import { DateFormat } from "@/utils/formats";
import { Situation } from "@/utils/arrays";

export function OnboardingAnalysed(props) {
  const { t } = useTranslation();

  const situationLabel = t(Situation.find(x => x.value == props.onboardingData.situation)?.labelKey)

  return(
    <Modal
      title={t('history.analysed')}
      closeModal={props.closeModal}
    >
      <Input type="name" name="name" value={props.onboardingData.name} disabled={true}>
        {t('history.name')}
      </Input>
      <Input type="situation" name="situation" value={situationLabel} disabled={true}>
        {t('history.situation')}
      </Input>
      <Input type="dtSituation" name="dtSituation" value={DateFormat(props.onboardingData.inclusionDate)} disabled={true}>
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
          value={props.onboardingData.observation}
          readOnly
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
