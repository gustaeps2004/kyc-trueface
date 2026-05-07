import apiClient from '../client';

const LOGIN_PREFIX = '/v1/auth';

export const userService = {
  postLogin: (request) => apiClient.post(USER_PREFIX, request)
};