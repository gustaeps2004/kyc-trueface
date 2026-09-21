import { useState } from "react";
import { Modal } from "@/shared/modal/Modal"
import { AlertTriangle } from "lucide-react"
import { useTranslation } from 'react-i18next';
import { CanWrite } from "@/shared/utils/permissions";
import { useApi } from "@/shared/hooks/useApi";
import { useNotification } from "@/shared/context/NotificationContext";
import { IdNumberFormat } from "@/shared/utils/formats";
import { onboardingService } from "../api/onboardingService";

const OBSERVATION_MAX_LENGTH = 500;

export function OnboardingAnalyse(props) {
  const { execute, isLoading } = useApi();
  const { notify } = useNotification();
  const { t } = useTranslation();
  const [observation, setObservation] = useState("");

  if (!CanWrite()) return null;

  const handlerReview = async (approved) => {
    if (!observation.trim()) {
      notify({ message: t('onboarding.validation.observationEmpty'), type: 'warning' });
      return
    }

    await execute(
      () => onboardingService.review(props.onboardingData.code, {
        approved,
        observation: observation.trim(),
      }),
      {
        onSuccess: () => {
          props.onSuccess?.();
          props.closeModal();
        },
        showSuccessPopup: true,
        successMessage: t(approved ? 'onboarding.approved' : 'onboarding.denied')
      }
    );
  }

  return(
    <Modal
      title={t('onboarding.analyse')}
      closeModal={props.closeModal}
      showRedButton={true}
      titleRedButton={t('onboarding.deny')}
      handlerRedAction={() => handlerReview(false)}
      showGreenButton={true}
      titleGreenButton={t('onboarding.approve')}
      handlerGreenAction={() => handlerReview(true)}
      disabled={isLoading}
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
          {t('onboarding.actionUndone')}
        </p>
      </div>

      <div className="
        rounded-lg
        border
        border-divider/40
        bg-base
        px-4
        py-3
        space-y-1
      ">
        <p className="text-sm text-fg font-medium">{props.onboardingData.name}</p>
        <p className="font-mono text-xs text-fg-muted">
          {IdNumberFormat(props.onboardingData.idNumber)}
        </p>
        <p className="text-xs text-warning-light pt-1">
          {props.onboardingData.situationMessage}
        </p>
      </div>

      <div className="h-full mt-3">
        <textarea
          rows="8"
          value={observation}
          maxLength={OBSERVATION_MAX_LENGTH}
          onChange={(e) => setObservation(e.target.value)}
          placeholder={t('onboarding.observation')}
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
