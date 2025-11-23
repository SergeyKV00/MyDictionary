<script lang="ts">
export class AsyncDialogResult {
  isSuccess: boolean;
  value: any;
  
  constructor(isSuccess: boolean, value: any) {
    this.isSuccess = isSuccess;
    this.value = value;
  }
}
</script>
<script lang="ts" setup>
import { ref } from 'vue';

const props = defineProps({
  title: { type: String, default: "" },
  width: { type: Number, default: 500 },
  loading: { type: Boolean, default: false }
});

const dialogVisible = ref(false);
let resolveEvent: ((value: any) => void) | null = null;
let rejectEvent: ((reason?: any) => void) | null = null;

async function openAndWaitResult() {
  dialogVisible.value = true;
  return new Promise((resolve, reject) => {
    resolveEvent = resolve;
    rejectEvent = reject;
  });
}

function onConfirm(value: any) {
  dialogVisible.value = false;
  resolveEvent?.(new AsyncDialogResult(true, value));
  clearCallbacks();
}

function onCancel(value: any) {
  dialogVisible.value = false;
  resolveEvent?.(new AsyncDialogResult(false, value));
  clearCallbacks();
}

function clearCallbacks() {
  resolveEvent = null;
  rejectEvent = null;
}

defineExpose({ openAndWaitResult, onConfirm, onCancel });
</script>

<template>
  <el-dialog
    v-model="dialogVisible"
    :title="props.title"
    :width="props.width"
    :close-on-click-modal="false"
    @cancel="onCancel"
  >
    <template #header>
      <slot name="header"></slot>
    </template>
    <div v-loading="props.loading">
      <slot></slot>
    </div>
    <template #footer>
      <slot name="footer"></slot>
    </template>
  </el-dialog>
</template>