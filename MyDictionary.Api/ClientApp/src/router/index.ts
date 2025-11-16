import { createRouter, createWebHistory } from 'vue-router';

import Login from '@/pages/auth/Login.vue'
import Home from '@/pages/Home.vue'

import { useAuthStore } from '@/store/auth';

const routes = [
  { path: '/', component: Home, meta: { requiresAuth: true } },
  { path: '/login', component: Login },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const auth = useAuthStore();
  if (to.meta.requiresAuth && !auth.isAuthenticated) {
    return next('/login');
  }
  if (to.path === '/login' && auth.isAuthenticated) {
    return next('/');
  }
  next();
});

export default router;