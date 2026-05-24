import apiClient from '../client';

const BASE_PATH = '/v1/user';

export const userService = {
  listByPartner: () => apiClient.get(BASE_PATH)
};
