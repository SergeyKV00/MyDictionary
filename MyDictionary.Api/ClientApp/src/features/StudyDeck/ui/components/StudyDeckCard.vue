<script lang="ts" setup>
import { computed } from 'vue';
import type { StudyDeckType } from '../../types/models/StudyDeckType';
import { Delete } from '@element-plus/icons-vue';
import { t } from '@/locales/i18';

const props = defineProps<{
  modelValue: StudyDeckType
}>();

const emit = defineEmits<{
  (e: 'delete', id: string): void
}>();

const deck = computed(() => props.modelValue);

</script>

<template>
  <el-card class="study-deck-card" shadow="hover">
    <template #header>
      <div class="card-header">
        <span class="title">{{ deck.name }}</span>
        <el-button 
            type="danger"
            class="study-deck-card__delete"
            :icon="Delete" 
            circle 
            text 
            @click.stop="emit('delete', deck.id)"
        />
      </div>
    </template>
    <div class="description">
      {{ deck.description || t('StudyDeck.NoDescription') }}
    </div>
  </el-card>
</template>

<style>
.study-deck-card .el-card__header {
  padding: 10px;
}
</style>
<style scoped>
.study-deck-card {
  width: 300px;
  cursor: pointer;
  border-bottom: 5px solid var(--el-border-color-light);
  transition: border-bottom 0.3s;
}
.study-deck-card:hover {
  border-bottom: 5px solid var(--el-color-primary);
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.title {
  font-weight: bold;
  font-size: 16px;
}
.description {
  color: #606266;
  font-size: 14px;
}
.study-deck-card__delete {
  font-size: 20px;
  color: var(--el-color-danger);
  cursor: pointer;
}
</style>
