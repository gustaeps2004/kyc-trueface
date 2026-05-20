import { useState, useCallback } from 'react';
import { useNotification } from '../context/NotificationContext';

/**
 * Hook padrão para chamadas de API.
 *
 * @returns {{ execute: Function, isLoading: boolean }}
 *
 * execute(apiCall, options)
 *   apiCall          — função que retorna a promise da chamada (ex: () => userService.postLogin(body))
 *   options:
 *     onSuccess(response)    — callback executado em caso de sucesso
 *     onError(error)         — callback executado em caso de erro
 *     showSuccessPopup       — exibe popup de sucesso (default: false)
 *     successMessage         — mensagem do popup de sucesso
 *     showErrorPopup         — exibe popup de erro (default: true)
 *     errorMessage           — sobrescreve a mensagem de erro da API
 */
export function useApi() {
  const [isLoading, setIsLoading] = useState(false);
  const { notify } = useNotification();

  const execute = useCallback(async (
    apiCall,
    {
      onSuccess,
      onError,
      showSuccessPopup = false,
      successMessage = 'Operação realizada com sucesso.',
      showErrorPopup = true,
      errorMessage,
    } = {}
  ) => {
    setIsLoading(true);
    try {
      const response = await apiCall();

      if (showSuccessPopup) {
        notify({ message: successMessage, type: 'success' });
      }

      onSuccess?.(response);
      return response;
    } catch (error) {
      const msg =
        errorMessage ??
        error.response?.data?.message ??
        'Ocorreu um erro inesperado. Tente novamente mais tarde.';

      if (showErrorPopup) {
        notify({ message: msg, type: 'error' });
      }

      onError?.(error);
      return null;
    } finally {
      setIsLoading(false);
    }
  }, [notify]);

  return { execute, isLoading };
}
