import { useTranslation } from 'react-i18next';

export function SituationBadge({ situationValue, array, isUser = false }) {
  const { t } = useTranslation();
  const situation = array.find(x => x.value == situationValue);
  if (!situation) return null;

  const tones = {
    success: `${isUser ? "" : "bg-success/15"} text-success-light`,
    danger: `${isUser ? "" : "bg-danger/15"} text-danger-light`,
    warning: "bg-warning/15 text-warning-light",
  };

  const styles = situation.tone
    ? tones[situation.tone]
    : situationValue === 1
    ? tones.success
    : situationValue === 2
    ? tones.danger
    : tones.warning;

  return (
    <span className={`
      inline-block
      text-xs
      font-medium
      px-3
      py-1
      rounded-full
      ${styles}
    `}>
      {t(situation.labelKey)}
    </span>
  );
}