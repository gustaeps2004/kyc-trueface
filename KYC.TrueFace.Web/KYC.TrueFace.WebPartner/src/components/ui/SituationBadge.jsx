import { useTranslation } from 'react-i18next';

export function SituationBadge({ situationValue, array }) {
  const { t } = useTranslation();
  const situation = array.find(x => x.value == situationValue);
  if (!situation) return null;

  const styles = situationValue === 1
    ? "bg-success/15 text-success-light"
    : situationValue === 2
    ? "bg-danger/15 text-danger-light"
    : "bg-warning/15 text-warning-light";

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