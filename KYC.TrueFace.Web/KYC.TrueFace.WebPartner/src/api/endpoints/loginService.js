import apiClient from '../client';

const BASE_PATH = '/v1/auth';

export const loginService = {
  postLogin: (request) => apiClient.post(BASE_PATH, request)
};