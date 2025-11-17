import axios from 'axios';
import { useAuthStore } from '@/store/auth';
import type { FrontendError } from '@/models/base/FrontendError';
import type { ApiResponse } from '@/models/base/ApiResponse';
import { NotificationService } from '@/services/notification'
import { NotifiableErrors } from "@/engine/errorConfig";

export async function apiPost<T>(url: string, payload?: any): Promise<T | null> {
  const response = await api.post<ApiResponse<T>>(url, payload);
  return response as unknown as T | null;
}

export async function apiGet<T>(url: string): Promise<T | null> {
  const response = await api.get<ApiResponse<T>>(url);
  return response as unknown as T | null;
}

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
  response => {
    const data = response.data as ApiResponse<any>;

    if (!data) return undefined;

    if (data.isSuccess === true) {
      return data.value ?? undefined;
    }

    if (data.isSuccess === false) {
      throw toFrontendError(data, response.status);
    }

    const status = data.status ?? response.status ?? 500;
    if (status >= 400) {
      throw toFrontendError(data, response.status);
    }

    return data.value ?? undefined;
  },
  error => {
    if (error.response?.status === 401) {
      const auth = useAuthStore();
      auth.logout();
      window.location.href = "/";
    }

    if (!error.response) {
      throw {
        code: "Network.Error",
        message: error.message ?? "Network error",
        status: 0,
        raw: error
      } as FrontendError;
    }

    const { data, status } = error.response;
    throw toFrontendError(data, status);
  }
);

function toFrontendError(data: ApiResponse<any> | any, responseStatus?: number): FrontendError {
  const status = data.status ?? responseStatus ?? 500;

  if (data.error) {
    const err: FrontendError = {
      code: data.error.code,
      message: data.error.description,
      status,
      validation: undefined,
      raw: data
    };

    if (data.error.metadata && Array.isArray(data.error.metadata)) {
      err.validation = {};

      for (const item of data.error.metadata) {
        if (!item.property)
          continue;

        if (!err.validation[item.property]) {
          err.validation[item.property] = [];
        }
        err.validation[item.property]!.push(item.message);
      }
    }

    if (NotifiableErrors[err.code]) {
      NotificationService.notifyError(err);
    }

    return err;
  }

  return {
    code: `Server.${status}`,
    message: data.title ?? "Server error",
    status,
    validation: undefined,
    raw: data
  };
}