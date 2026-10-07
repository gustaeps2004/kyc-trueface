import { useCallback } from 'react';
import { useTranslation } from 'react-i18next';

// Messages recorded by the server arrive as a translation key plus the values to interpolate.
// Anything that is not a known key (e.g. a reviewer's observation) is shown as written.
export function useTranslateMessage() {
  const { t, i18n } = useTranslation();

  return useCallback(
    (message, args) => message && i18n.exists(message) ? t(message, args) : message,
    [t, i18n]
  );
}
