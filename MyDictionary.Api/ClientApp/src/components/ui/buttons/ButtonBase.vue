<script lang="ts" setup>

const props = defineProps({
  size: { type: String, default: 'mini' },
  type: { type: String, default: 'primary' },
  circle: { type: Boolean, default : false }
})

</script>

<template>
  <el-button 
    :size="size" 
    :type="type"
    :circle="circle"
    style="margin: 0px;"
  >
    <slot></slot>
  </el-button>
</template>