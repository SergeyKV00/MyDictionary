<template>
  <v-container class="fill-height d-flex justify-center align-center">
    <v-card width="400">
      <v-card-title class="text-h5">Login</v-card-title>
      <v-card-text>
        <v-text-field v-model="username" label="Username" />
        <v-text-field v-model="password" label="Password" type="password" />
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="submit">Login</v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/store/auth';

const username = ref('');
const password = ref('');
const router = useRouter();
const auth = useAuthStore();

const submit = async () => {
  try {
    await auth.login(username.value, password.value);
    router.push('/');
  } catch {
    alert('Login failed');
  }
};
</script>
