import { defineStore } from "pinia";
import AuthApi from "../api/AuthApi";

import type { LoginPayload } from "../types/LoginPayload";
import type { RegistractionPayload } from "../types/RegistrationPayload";

export const useAuthStore = defineStore("auth",  {
  state: () => ({
    token: localStorage.getItem("token"),
    loading: false,
    errorCode: null as string | null
  }),

  getters: {
    isAuthenticated: (state) => !!state.token
  },

  actions: {
    async login(payload: LoginPayload) {
      try {
        this.loading = true;
        this.errorCode = null;

        const response = await AuthApi.login(payload);
        if (response?.error != null) {
          this.errorCode = response.error.code;
          return Promise.reject();
        }

        this.token = response?.data ?? null;
        window.location.href = '/';

        if (this.token)
          localStorage.setItem("token", this.token!);
      } finally {
        this.loading = false;
      }
    },

    async register(payload: RegistractionPayload) {
      try {
        this.loading = true;
        this.errorCode = null;

        const response = await AuthApi.register(payload);
        if (response?.error != null) {
          this.errorCode = response.error.code;
          return Promise.reject();
        }
      } finally {
        this.loading = false;
      }
    },
  
    logout() {
      this.token = null;
      localStorage.removeItem("token");
    },
  }
})