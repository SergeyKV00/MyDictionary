<template>
  <v-container class="mt-4">
    <v-row>
      <v-col>
        <h2>Welcome, {{ auth.userName }}!</h2>
        <v-btn @click="logout">Logout</v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts" setup>
import { useAuthStore } from '@/store/auth';
import { useRouter } from 'vue-router';

const auth = useAuthStore();
const router = useRouter();

const logout = () => {
  auth.logout();
  router.push('/login');
};
</script>