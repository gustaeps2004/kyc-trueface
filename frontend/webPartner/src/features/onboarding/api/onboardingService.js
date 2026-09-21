import apiClient from '@/shared/api/client';

const BASE_PATH = '/v1/onboarding';

export const onboardingService = {
  listPendingManualReview: (signal) =>
    apiClient.get(`${BASE_PATH}/manual-review`, { signal }),

  listReviewed: (situation, signal) =>
    apiClient.get(`${BASE_PATH}/reviewed`, {
      params: situation ? { situation } : undefined,
      signal,
    }),

  upload: (formData) =>
    apiClient.post(BASE_PATH, formData, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }),

  getImage: (code, kind, signal) =>
    apiClient.get(`${BASE_PATH}/${code}/image/${kind}`, { responseType: 'blob', signal }),

  review: (code, request) =>
    apiClient.post(`${BASE_PATH}/${code}/review`, request),
};
