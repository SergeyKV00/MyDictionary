<script  lang="ts">
export const rules = {
  userName: [{ required: true, message: 'Введите логин', trigger: 'change' }], // TODO_U
  password: [
    { required: true, message: 'Введите пароль', trigger: 'change' }, // TODO_U
    { min: 6, message: 'Пароль должен быть не менее 6 символов', trigger: 'change' } // TODO_U
  ]
}
</script>
<script lang="ts" setup>
import { computed } from 'vue';
import { FormInput } from '@/components/ui/form';
import type { AuthCredentialsType } from '../../types/AuthCredentialsType';

const props = defineProps<{
  modelValue: AuthCredentialsType
}>()

const emit = defineEmits(['update:modelValue'])
const model = computed({
  get: () => props.modelValue,
  set: (value) => emit("update:modelValue", value)
});
</script>

<template>
  <FormInput 
    v-model="model.userName" 
    :placeholder="$t('Auth.Login')"
    name="userName"
  />
  <FormInput 
    v-model="model.password" 
    type="password" 
    :placeholder="$t('Auth.Password')"
    name="password"
  />
</template>
