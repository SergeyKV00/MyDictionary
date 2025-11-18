<script lang="ts" setup>
import { ref } from 'vue';
import { FormInput } from '@/components/ui/form';

import type { LoginPayload } from '../types/LoginPayload';

const props = defineProps<{
  model: LoginPayload
}>()

const loginFormRef = ref()

const rules = {
  userName: [{ required: true, message: 'Введите логин', trigger: 'blur' }],
  password: [
    { required: true, message: 'Введите пароль', trigger: 'blur' },
    { min: 6, message: 'Пароль должен быть не менее 6 символов', trigger: 'blur' }
  ]
}

const validateForm = async () => {
  return await loginFormRef.value.validate();
}

defineExpose({ validateForm })

</script>

<template>
  <FormInput 
    v-model="props.model.userName" 
    :placeholder="$t('Auth.Login')"
    name="userName"
  />
  <FormInput 
    v-model="props.model.password" 
    type="password" 
    :placeholder="$t('Auth.Password')"
    name="password"
  />
</template>
