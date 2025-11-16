import { defineStore } from "pinia";
import { api } from '@/engine/api';

interface AuthState {
  token: string | null;
  userName: string | null;
}

export const useAuthStore = defineStore('auth', {
    state: (): AuthState => ({
        token: localStorage.getItem('token'),
        userName: localStorage.getItem('userName')
    }),
    getters: {
        isAuthenticated: (state) => !!state.token
    },
    actions: {
    async login(username: string, password: string) {
      try {
        const response = await api.post('/Users/Login', { username, password });
        console.log(response)
        this.token = response.data;
        this.userName = username;

        if (this.token) 
            localStorage.setItem('token', this.token);
        if (this.userName)
            localStorage.setItem('userName', this.userName);
      } catch (err) {
        console.error('Login failed', err);
        throw err;
      }
    },

    logout() {
      this.token = null;
      this.userName = null;
      localStorage.removeItem('token');
      localStorage.removeItem('userName');
    },

    initialize() {
      const token = localStorage.getItem('token');
      const userName = localStorage.getItem('userName');
      if (token) {
        this.token = token;
        this.userName = userName;
      }
    }
  }
})