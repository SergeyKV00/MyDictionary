<script lang="ts" setup>
import { ref, onUnmounted } from 'vue';
import { useAuthStore } from '@/features/Auth/store/AuthStore'
import { useRouter } from 'vue-router';
import ErrorMessage from '@/engine/errorConfig';

import { ButtonLink, ButtonBase } from '@/components/ui/buttons';
import { ErrorLabel } from '@/components/ui/labels';
import FormInput from '@/components/ui/form/FormInput.vue';

import AuthCredentials from './components/AuthCredentials.vue';
import  { rules as authRules } from './components/AuthCredentials.vue';
import type { RegistractionPayload } from '../types/RegistrationPayload';

const auth = useAuthStore();
const router = useRouter();
auth.loading = true;

const form = ref<RegistractionPayload>({
  email: "",
  userName: "",
  password: ""
});

const regFormRef = ref();
const regRules = {
  email: [{ required: true, message: 'Введите почту', trigger: 'change' }] // TODO_U
}
const rules = { ...regRules, ...authRules};

const singUp = async () => {
  try {
    const valid = await regFormRef.value.validate();
    if (!valid) return;

    await auth.register(form.value);
    await auth.login(form.value);
  } catch {
    
  }
}

const routeToLogin = () => {
  router.push({name: 'Login'});
}
setTimeout(() => {
  form.value = {
    email: "",
    userName: "",
    password: ""
  };
  regFormRef.value.resetFields();
  auth.loading = false;
}, 500)

onUnmounted(() => {
  auth.errorCode = null;
});
</script>

<template>
    <el-card class="login-card" v-loading=auth.loading>
    <h2>{{ $t('Auth.Registraction') }}</h2>
    <el-skeleton v-if="auth.loading" :rows="4" animated />

    <el-form
      v-show="!auth.loading"
      ref="regFormRef"
      :model="form"
      :rules="rules"
    >
      <FormInput 
        v-model="form.email" 
        :placeholder="$t('Auth.Email')"
        name="email"
      />
      <AuthCredentials v-model="form"/>     
      <ErrorLabel
        v-if="ErrorMessage.has(auth.errorCode)"
        class="flex-center">
        {{ ErrorMessage.show(auth.errorCode) }}
      </ErrorLabel>
      <ButtonBase
        class="wh-100"
        @click="singUp"
      >
        {{ $t('Auth.SingUp') }}
      </ButtonBase>
      <div class="flex-center">
        <ButtonLink style="margin-top: 15px;"
          @click="routeToLogin"
        >
          {{ $t('Auth.SignIn') }}
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
</style>