import { defineStore } from "pinia";

export const useLayoutStore = defineStore("layout", {
  state: () => ({
    isSidebarVisible: true
  }),

  actions: {
    hideSidebar() {
      this.isSidebarVisible = false;
    },
    showSidebar() {
      this.isSidebarVisible = true;
    }
  }
});