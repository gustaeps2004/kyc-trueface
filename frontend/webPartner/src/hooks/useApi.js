import { useState, useCallback } from 'react';
import axios from 'axios';
import { useNotification } from '../context/NotificationContext';
import { useTranslation } from 'react-i18next';

export function useApi() {
  const [isLoading, setIsLoading] = useState(false);
  const { notify } = useNotification();
  const { t } = useTranslation();

  const execute = useCallback(async (
    apiCall,
    {
      onSuccess,
      onError,
      showSuccessPopup = false,
      successMessage,
      showErrorPopup = true,
      errorMessage,
    } = {}
  ) => {
    setIsLoading(true);
    try {
      const response = await apiCall();

      if (showSuccessPopup) {
        notify({ message: successMessage ?? t('notifications.successDefault'), type: 'success' });
      }

      onSuccess?.(response);
      return response;
    } catch (error) {
      if (axios.isCancel(error)) {
        return null;
      }

      const msg =
        errorMessage ??
        error.response?.data?.message ??
        'notifications.errorDefault';
      
      if (showErrorPopup) {
        notify({ message: t(msg), type: 'error' });
      }

      onError?.(error);
      return null;
    } finally {
      setIsLoading(false);
    }
  }, [notify, t]);

  return { execute, isLoading };
}
