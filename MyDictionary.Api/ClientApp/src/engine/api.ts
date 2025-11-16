import axios from 'axios';
import { useAuthStore } from '@/store/auth';

export const api = axios.create({
  baseURL: '/api',
  withCredentials: false
});

api.interceptors.request.use(config => {
  const auth = useAuthStore();
  if (auth.token) {
    config.headers = config.headers || {};
    config.headers['Authorization'] = `Bearer ${auth.token}`;
  }
  return config;
});

api.interceptors.response.use(
  res => res,
  async error => {
    if (error.response?.status === 401) {
      const auth = useAuthStore();
      auth.logout();
      // TODO_U Redirect
    }
    return Promise.reject(error);
  }
);