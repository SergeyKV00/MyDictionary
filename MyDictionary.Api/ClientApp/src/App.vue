<template>
  <el-container style="height: 100vh; margin: 0; padding: 0;">
    <!-- Header -->
    <el-header height="60px" class="header" style="padding:0 16px;">
      <div class="header-left">My Dictionary</div>
      <div class="header-right">
        <el-select
          v-model="currentLang"
          size="small"
          placeholder="Язык"
          @change="changeLang"
          style="margin-right: 12px;"
        >
          <el-option label="English" value="en" />
          <el-option label="Русский" value="ru" />
        </el-select>

        <el-button v-if="showTopRight" type="primary" size="small" @click="logout">Выйти</el-button>
      </div>
    </el-header>

    <el-container>
      <!-- Sidebar -->
      <el-aside v-if="showSidebar" width="200px" style="padding:0; font-weight: bold; font-size: 18px !important;">
        <el-menu :default-active="activeRoute" router>
          <el-menu-item index="/"><el-icon><House /></el-icon>Главная</el-menu-item>
          <el-menu-item index="/dictionaries"><el-icon><Notebook /></el-icon>Мои словари</el-menu-item>
        </el-menu>
      </el-aside>

      <!-- Main content -->
      <el-main style="padding: 15px;">
        <router-view />
      </el-main>
    </el-container>
  </el-container>
</template>

<script lang="ts" setup>
import { ref, watch, computed } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { useAuthStore } from '@/features/Auth/store/AuthStore';
import { useI18n } from 'vue-i18n';
import { House, Notebook } from '@element-plus/icons-vue'
import { useLayoutStore } from './stores/layoutStore';

const auth = useAuthStore();
const layout = useLayoutStore();
const router = useRouter();
const route = useRoute();

const activeRoute = ref(route.path);
watch(route, () => (activeRoute.value = route.path));

const showSidebar = computed(() => route.path !== '/login' && route.path !== '/registration' && layout.isSidebarVisible);
const showTopRight = computed(() => auth.isAuthenticated && route.path !== '/login');

const { locale } = useI18n();
const currentLang = ref(locale.value);

function changeLang(lang: string) {
  locale.value = lang;
}

function logout() {
  auth.logout();
  router.push('/login');
}
</script>

<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: var(--el-color-primary);
  color: white;
  /* border-bottom: 5px solid var(--el-color-primary); */
}

.header-left {
  font-size: 18px;
  font-weight: bold;
}

.header-right {
  display: flex;
  align-items: center;
}

.mr-3 {
  margin-right: 12px;
}
</style>