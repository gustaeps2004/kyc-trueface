import { Modal } from "@/shared/modal/Modal"
import { Input } from '@/shared/ui/Input'
import { useTranslation } from 'react-i18next';
import { DateFormat, IdNumberFormat } from "@/shared/utils/formats";
import { OnboardingSituation } from "@/shared/utils/arrays";

export function OnboardingAnalysed(props) {
  const { t } = useTranslation();
  const onboarding = props.onboardingData;

  const situationKey = OnboardingSituation.find(x => x.value == onboarding.situation)?.labelKey;
  const similarity = onboarding.similarity == null
    ? t('history.similarityManual')
    : `${onboarding.similarity.toFixed(2)}%`;

  return(
    <Modal
      title={t('history.analysed')}
      closeModal={props.closeModal}
    >
      <Input type="text" name="name" value={onboarding.name} disabled={true}>
        {t('history.name')}
      </Input>
      <Input type="text" name="idNumber" value={IdNumberFormat(onboarding.idNumber)} disabled={true}>
        {t('history.idNumber')}
      </Input>
      <Input type="text" name="situation" value={situationKey ? t(situationKey) : ""} disabled={true}>
        {t('history.situation')}
      </Input>
      <Input type="text" name="dtSituation" value={DateFormat(onboarding.situationDt)} disabled={true}>
        {t('history.date')}
      </Input>
      <Input type="text" name="similarity" value={similarity} disabled={true}>
        {t('history.similarity')}
      </Input>
      <div>
        <label className="block text-xs text-fg-subtle font-medium mb-1.5">
          {onboarding.observation ? t('history.observation') : t('history.automaticResult')}
        </label>
        <textarea
          id="txAreaObservation"
          rows="6"
          disabled
          value={onboarding.observation ?? onboarding.situationMessage ?? ""}
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
