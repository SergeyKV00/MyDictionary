import { createRouter, createWebHistory } from 'vue-router';

const Login = () => import('@/pages/auth/Login.vue')
const Main = () => import('@/pages/main/main.vue')
const DictionaryList = () => import('@/pages/dictionaries/DictionaryList.vue')

import { useAuthStore } from '@/store/auth';

const routes = [
  { path: '/', component: Main, meta: { requiresAuth: true } },
  { path: '/dictionaries', component: DictionaryList, meta: { requiresAuth: true }},
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