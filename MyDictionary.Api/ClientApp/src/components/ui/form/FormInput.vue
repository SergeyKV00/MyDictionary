<script lang="ts" setup>
import { computed } from 'vue'

const props = defineProps({
  modelValue: { type: null as any },
  type: { type: String, default: 'text' },
  placeholder: String,
  name: String
})

const emit = defineEmits(['update:modelValue'])

const model = computed({
  get: () => props.modelValue,
  set: value => emit('update:modelValue', value)
})
</script>

<template>
  <el-form-item :prop="name">
	<el-input
		v-model="model"
	  :type="type"
	  :placeholder="placeholder"
	/>
  </el-form-item>
</template>