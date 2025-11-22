<script lang="ts" setup>
import { ref, onUnmounted } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/features/Auth/store/AuthStore'
import ErrorMessage from '@/engine/errorConfig';

import { ButtonLink, ButtonBase } from '@/components/ui/buttons';
import { ErrorLabel } from '@/components/ui/labels';
import AuthCredentials from './components/AuthCredentials.vue';

import  { rules as authRules } from './components/AuthCredentials.vue';
import type { LoginPayload } from '../types/LoginPayload';

const auth = useAuthStore();
const route = useRouter();

const form = ref<LoginPayload>({
  userName: "",
  password: ""
});

const loginFormRef = ref();

const signIn = async () => {
  const valid = await loginFormRef.value.validate();
  if (!valid) return;

  await auth.login(form.value);
}

const routeToRegistration = () => {
  route.push({ name: 'Registration' });
}

onUnmounted(() => {
  auth.errorCode = null;
});
</script>

<template>
    <el-card class="login-card" v-loading=auth.loading>
    <h2>{{ $t('Auth.WelcomeBack') }}</h2>
    <el-form
      ref="loginFormRef"
      :model="form"
      :rules="authRules"
    >
      <AuthCredentials v-model="form"/>
      <ErrorLabel 
        v-if="ErrorMessage.has(auth.errorCode)"
        class="flex-center">
        {{ ErrorMessage.show(auth.errorCode) }}
      </ErrorLabel>
      <ButtonBase
        class="wh-100"
        @click="signIn"
      >
        {{ $t('Auth.SignIn') }}
      </ButtonBase>
      <div class="sing-up-text">
        <span>
          {{ $t('Auth.NoAccount') }}
        </span>
        <ButtonLink @click="routeToRegistration">
          {{ $t('Auth.SingUp') }}
        </ButtonLink>
      </div>
    </el-form>
  </el-card>
</template>

<style>
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