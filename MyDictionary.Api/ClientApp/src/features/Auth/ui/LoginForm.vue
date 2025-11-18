<script lang="ts" setup>
import { ref } from 'vue';
import { FormInput } from '@/components/ui/form';
import { ButtonLink, ButtonBase } from '@/components/ui/buttons';
import { ErrorLabel } from '@/components/ui/labels';

import type { LoginPayload } from '../types/LoginPayload';
import { useAuthStore } from '@/features/Auth/store/AuthStore'

const auth = useAuthStore();

const form = ref<LoginPayload>({
  userName: "",
  password: ""
});

const loginFormRef = ref();

const rules = {
  userName: [{ required: true, message: 'Введите логин', trigger: 'blur' }],
  password: [
    { required: true, message: 'Введите пароль', trigger: 'blur' },
    { min: 6, message: 'Пароль должен быть не менее 6 символов', trigger: 'blur' }
  ]
}

const signIn = async () => {
  const valid = await loginFormRef.value.validate();
  if (!valid) return;

  await auth.login(form.value);
}

</script>

<template>
    <el-card class="login-card">
    <h2>{{ $t('Auth.WelcomeBack') }}</h2>
    <el-form
      ref="loginFormRef"
      :model="form"
      :rules="rules"
    >
      <FormInput 
        v-model="form.userName" 
        :placeholder="$t('Auth.Login')"
        name="userName"
      />
      <FormInput 
        v-model="form.password" 
        type="password" 
        :placeholder="$t('Auth.Password')"
        name="password"
      />
      <ErrorLabel class="flex-center">
        {{ auth.loginError }}
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
        <ButtonLink >
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