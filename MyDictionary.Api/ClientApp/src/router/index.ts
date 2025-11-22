import { createRouter, createWebHistory } from 'vue-router';

const Login = () => import('@/features/Auth/views/LoginPage.vue')
const Main = () => import('@/pages/main/main.vue')
const DictionaryList = () => import('@/pages/dictionaries/DictionaryList.vue')
const RegistrationPage = () => import('@/features/Auth/views/RegistrationPage.vue')
const DictionaryItemPage = () => import('@/pages/dictionaryItems/dictionaryItemPage.vue')

import { useAuthStore } from '@/features/Auth/store/AuthStore';

const routes = [
  { 
    name: 'Login',
    path: '/login', 
    component: Login 
  },
  {
    name: 'Registration',
    path: '/registration',
    component: RegistrationPage
  },
  { 
    name: 'Home',
    path: '/', 
    component: Main, 
    meta: { requiresAuth: true } 
  },
  { 
    name: 'Dictionaries',
    path: '/dictionaries', 
    component: DictionaryList, 
    meta: { requiresAuth: true }
  },
  {
    name: 'DictionaryItems',
    path: '/dictionaryItems',
    component: DictionaryItemPage
  }
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