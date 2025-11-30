<script setup lang="ts">
import { computed, watch } from 'vue';
import type { DictionaryItemListRequest } from '../../types/requests/DictionaryItemListRequest';

const props = defineProps<{
  filter: DictionaryItemListRequest
}>()
const emit = defineEmits<{
  (e: 'update:filter', filter: DictionaryItemListRequest): void
}>()

const filter = computed(() => props.filter);

watch(
  filter,
  () => emit('update:filter', filter.value),
  {deep: true}
)


</script>
<template>
<div>
  <el-input v-model="filter.term" :placeholder="$t('DictionaryItem.Term')"/>
</div>
</template>