import apiClient from '../client';

const BASE_PATH = '/v1/user';

export const userService = {
  listByPartner: (filter) => apiClient.get(`${BASE_PATH}?filter=${filter}`),
};
