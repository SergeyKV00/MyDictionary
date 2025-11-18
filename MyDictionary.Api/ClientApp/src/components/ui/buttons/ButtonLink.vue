<script lang="ts" setup>

const props = defineProps({
  size: { type: String, default: 'mini' },
  type: { type: String, default: 'primary' }
})

</script>

<template>
  <el-button 
    :size="size" 
    :type="type" 
    link
  >
    <slot></slot>
  </el-button>
</template>