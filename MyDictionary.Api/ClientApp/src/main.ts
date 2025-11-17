import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { i18n } from '@/locales/i18';

import App from './App.vue'
import router from './router'
import ElementPlus from 'element-plus'

import 'element-plus/dist/index.css'
import '@/assets/styles/variables.css'
import '@/assets/styles/style.css'

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(ElementPlus);
app.use(i18n);

app.mount('#app')
