<template>
  <div class="login-layout">
    <el-card class="login-card">
      <h2>{{ $t('Auth.WelcomeBack') }}</h2>
      <el-form @submit.prevent="submit">
        <el-form-item>
          <el-input v-model="username" :placeholder="$t('Auth.Login')"/>
        </el-form-item>
        <el-form-item>
          <el-input type="password" v-model="password" :placeholder="$t('Auth.Password')" />
        </el-form-item>

        <div v-if="loginError" class="span-error flex-center">
          {{ loginError }}
        </div>

        <el-button type="primary" class="wh-100" @click="submit">{{ $t('Auth.SignIn')}}</el-button>
        <div class="sing-up-text">
          <span>{{ $t('Auth.NoAccount') }}</span>
          <el-button size="mini" type="primary" link >{{ $t('Auth.SingUp') }}</el-button>
        </div>
        
      </el-form>
    </el-card>
  </div>

</template>

<script lang="ts" setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/store/auth';
import type { FrontendError } from '@/models/base/FrontendError';

const username = ref('');
const password = ref('');
const router = useRouter();
const auth = useAuthStore();

const loginError = ref<string | null>(null);

const submit = async () => {
  loginError.value = null;
  try {
    await auth.login(username.value, password.value);
    router.push('/');
  } catch (err: unknown) {
    const fe = err as FrontendError;
    if (fe.code === 'User.InvalidCredentials') {
      loginError.value = 'Неверные логин или пароль';
    } else {
      console.error(fe);
    }
  }
};
</script>
<style scoped>
.login-layout {
  display: flex;
  justify-content: center;
}
.login-card {
  margin-top: 50px;
  width: 450px;
}
.sing-up-text {
  font-size: 14px;
  color: #5b5b5b;
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 15px;
}
</style>
