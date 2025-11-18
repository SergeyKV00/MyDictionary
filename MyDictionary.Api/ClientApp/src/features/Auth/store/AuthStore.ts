import { defineStore } from "pinia";
import AuthApi from "../api/AuthApi";
import type { LoginPayload } from "../types/LoginPayload";
import type { FrontendError } from "@/models/base/FrontendError";

export const useAuthStore = defineStore("auth",  {
    state: () => ({
        token: localStorage.getItem("token"),
        loading: false,
        loginError: null as string | null,
    }),

    getters: {
        isAuthenticated: (state) => !!state.token
    },

    actions: {
        async login(payload: LoginPayload) {
          try {
            this.loading = true;
            this.loginError = null;

            this.token = await AuthApi.login(payload);
            window.location.href = "/";
    
            if (this.token)
              localStorage.setItem("token", this.token!);
          } catch (err: unknown) {
            const fe = err as FrontendError;
            if (fe.code === 'User.InvalidCredentials') {
              this.loginError = 'Неверные логин или пароль';
            }

            throw err;
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