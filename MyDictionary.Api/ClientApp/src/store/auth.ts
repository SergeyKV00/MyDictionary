import { defineStore } from "pinia";
import UserService from "@/services/user"
import type { FrontendError } from "@/models/base/FrontendError"

interface AuthState {
  token: string | null;
  userName: string | null;
}

export const useAuthStore = defineStore("auth", {
  state: (): AuthState => ({
    token: localStorage.getItem("token"),
    userName: localStorage.getItem("userName")
  }),

  getters: {
    isAuthenticated: (state) => !!state.token
  },

  actions: {
    async login(username: string, password: string) {
      try {
        const token = await UserService.login(username, password);

        this.token = token;
        this.userName = username;

        if (this.token)
          localStorage.setItem("token", token!);
        if (this.userName)
          localStorage.setItem("userName", username);
      } catch (err: unknown) {
        throw err;
      }
    },

    logout() {
      this.token = null;
      this.userName = null;
      localStorage.removeItem("token");
      localStorage.removeItem("userName");
    },

    initialize() {
      const token = localStorage.getItem("token");
      const userName = localStorage.getItem("userName");
      if (token) {
        this.token = token;
        this.userName = userName;
      }
    }
  }
});