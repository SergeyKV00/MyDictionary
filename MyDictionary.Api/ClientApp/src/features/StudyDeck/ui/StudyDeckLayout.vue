<script lang="ts" setup>
import { onMounted, ref, computed, nextTick } from 'vue';
import { useStudyDeckStore } from '../store/StudyDeckStore';
import { CirclePlus } from '@element-plus/icons-vue';
import StudyDeckCard from './components/StudyDeckCard.vue';
import StudyDeckCreate from './components/StudyDeckCreate.vue';
import StudyDeckSettings from './components/StudyDeckSettings.vue';
import { ElMessageBox } from 'element-plus';
import type { StudyDeckType } from '../types/models/StudyDeckType';

const store = useStudyDeckStore();
const createRef = ref();
const settingsRef = ref();
const selectedDeckId = ref('');

const decks = computed(() => store.getStudyDecks);

onMounted(async () => {
  await store.fetchStudyDecks();
});

const onCreate = () => {
    createRef.value.open();
}

const onDeckClick = async (deck: StudyDeckType) => {
    selectedDeckId.value = deck.id;
    await nextTick();
    settingsRef.value.open();
}

const onDelete = async (id: string) => {
    try {
        await ElMessageBox.confirm(
            'Are you sure you want to delete this deck?',
            'Warning',
            {
                confirmButtonText: 'Yes',
                cancelButtonText: 'Cancel',
                type: 'warning',
            }
        );
        await store.delete(id);
    } catch {
        // Cancelled
    }
}
</script>

<template>
  <div class="study-deck-layout">
    <el-card class="box-card" v-loading="store.loading">
      <template #header>
        <div class="card-header">
          <span class="header-title">Study Decks</span>
          <el-button type="success" class="large-icon" :icon="CirclePlus" circle text @click="onCreate" />
        </div>
      </template>
      
    <div class="deck-list">
        <StudyDeckCard 
          v-for="deck in decks" 
          :key="deck.id" 
          :modelValue="deck"
          @delete="onDelete"
          @click="onDeckClick(deck)"
        />
      </div>
    </el-card>

    <StudyDeckCreate ref="createRef" />
    <StudyDeckSettings ref="settingsRef" :deckId="selectedDeckId" />
  </div>
</template>

<style scoped>
.study-deck-layout {
  padding: 20px;
}
.card-header {
  display: flex;
  align-items: center;
}
.header-title {
  font-weight: bold;
  font-size: 25px;
  color: var(--el-color-primary);
  margin-right: 15px;
}
.deck-list {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}
</style>
