<script setup lang="ts">
import { defineProps, defineEmits, computed } from 'vue';

interface PaginationProps {
  total: number;
  pageSize?: number;
  currentPage?: number;
}

const props = defineProps<PaginationProps>();
const emit = defineEmits<{
  (e: 'update:currentPage', page: number): void;
  (e: 'update:pageSize', size: number): void;
}>();

const computedPage = computed({
  get: () => props.currentPage ?? 1,
  set: (value: number) => emit('update:currentPage', value)
});

const computedSize = computed({
  get: () => props.pageSize ?? 20,
  set: (value: number) => emit('update:pageSize', value)
});

function updatePage(page: number) {
  computedPage.value = page;
}

function updateSize(size: number) {
  computedSize.value = size;
}
</script>

<style scoped>
.pagination-wrapper {
  display: flex;
  justify-content: center;
  margin: 20px 0;
}
</style>
<template>
  <div class="pagination-wrapper">
    <el-pagination
      background
      layout="prev, pager, next, total"
      :current-page="computedPage"
      :page-size="computedSize"
      :total="total"
      @current-change="updatePage"
      @size-change="updateSize"
      :page-sizes="[10, 20, 50, 100]"
    />
  </div>
</template>