import apiClient from '@/shared/api/client';

const BASE_PATH = '/v1/dashboard';

export const dashboardService = {
  getSummary: (signal) =>
    apiClient.get(`${BASE_PATH}/summary`, { signal }),
};
