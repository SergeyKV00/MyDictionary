<script setup lang="ts">
import { computed, onMounted, ref, watch } from 'vue';
import type { DictionaryItemExampleType } from '@/features/DictionaryItem/types/models/DictionaryItemExampleType';
import DictionaryItemExampleApi from '@/features/DictionaryItem/api/DictionaryItemExampleApi';
import type { DictionaryItemExampleListRequest } from '@/features/DictionaryItem/types/requests/DictionaryItemExampleListRequest';

const props = defineProps<{
  dictionaryItemId: string
}>()

const examples = ref<DictionaryItemExampleType[]>([])
const expanded = ref<Record<string, boolean>>({})
const loading = ref(false)

const examplesRequest = computed<DictionaryItemExampleListRequest>(() => ({
  type: "DictionaryItemExampleListRequest",
  dictionaryItemId: props.dictionaryItemId
}))

watch(() => props.dictionaryItemId, fetchExamples, {immediate: true})

async function fetchExamples() {
  loading.value = true;
  return DictionaryItemExampleApi.list(examplesRequest.value)
    .then(response => {
      if (response?.error != null) return
      examples.value = response?.data?.data ?? []
      
    })
    .finally(() => loading.value = false)
}

function onToggle(id: string) {
  expanded.value[id] = !expanded.value[id];
}

</script>
<template>
  <ul class="examples">
    <li
      v-for="item in examples"
      :key="item.id"
      class="item"
      @click="onToggle(item.id)"
    >
      <div class="example-text">{{ item.example }}</div>

      <div class="translation-wrapper">
        <div
          class="translation"
          :class="{ open: expanded[item.id] }"
        >
          {{ item.translation }}
        </div>
        <div
          class="translation-noise"
          :class="{ open: !expanded[item.id] && item.translation }"
        >
          {{ item.translation }}
        </div>
      </div>
    </li>
  </ul>
</template>

<style scoped>
.examples {
  display: flex;
  flex-wrap: wrap;
  gap: 15px;
  list-style: none;
  padding: 0;
  margin: 0;
}

.item {
  padding: 10px;
  cursor: pointer;
  user-select: none;
  border: 1px dashed var(--el-color-primary);
  border-left: 5px solid var(--el-color-primary);
  border-radius: 5px;
}

.example-text {
  font-weight: 500;
}

.translation-wrapper {
  position: relative;
  min-height: 1em;
  overflow: hidden;
}

.translation-noise {
  color: transparent;
  position: absolute;
  top: 5px;
  left: 0;
  width: 100%;
  height: 60%;
  opacity: 0;
  filter: blur(2px);
  background-image: url(/src/assets/image/noise.png);
  background-repeat: repeat;
  background-size: auto;
  mix-blend-mode: normal;
  pointer-events: none;
  border-radius: 5px;
  transition: opacity 0.5s ease;
  animation: noiseShift 45s infinite linear;
}

.translation-noise.open {
  opacity: 0.5;
}

.translation {
  display: inline;
  opacity: 0;
  transition: opacity 0.3s ease;
  white-space: pre-wrap;
}

.translation.open {
  opacity: 1;
}

 @keyframes noiseShift {
  0%   { background-position: 0 0; }
  25%  { background-position: 0 64px; }
  50%  { background-position: 0 128px; }
  75%  { background-position: 0 192px; }
  100% { background-position: 0 256px; }
}
</style>