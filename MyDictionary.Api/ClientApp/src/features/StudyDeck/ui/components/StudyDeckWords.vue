<script lang="ts" setup>
import { ref, watch } from 'vue';
import { useStudyDeckStore } from '../../store/StudyDeckStore';
import { t } from '@/locales/i18';
import DictionaryItemApi from '@/features/DictionaryItem/api/DictionaryItemApi';
import type { DictionaryDto } from '@/models/dto/dictionary/DictionaryDto';
import type { DictionaryItemListRequest } from '@/features/DictionaryItem/types/requests/DictionaryItemListRequest';
import type { DictionaryItemType } from '@/features/DictionaryItem/types/models/DictionaryItemType';
import type { StudyDeckWordResponse } from '../../types/responses/StudyDeckWordResponse';
import { ElMessage, ElMessageBox } from 'element-plus';
import { Plus, Delete } from '@element-plus/icons-vue';

const props = defineProps<{
  deckId: string;
  dictionaries: DictionaryDto[];
  isActive: boolean;
}>();

const store = useStudyDeckStore();

// Words State
const deckWords = ref<StudyDeckWordResponse[]>([]);
const deckWordsTotal = ref(0);
const deckWordsPage = ref(1);
const deckWordsPageSize = ref(10);
const deckWordsLoading = ref(false);
const selectedWords = ref<string[]>([]);

// Add Words State
const selectedWordDictId = ref<string>('');
const dictWords = ref<DictionaryItemType[]>([]);
const selectedWordIds = ref<string[]>([]);
const wordsLoading = ref(false);
const showAddForm = ref(false);

const fetchDeckWords = async () => {
  if (!props.deckId) return;
  deckWordsLoading.value = true;
  try {
    const response = await store.getWords({
      studyDeckId: props.deckId,
      page: deckWordsPage.value,
      pageSize: deckWordsPageSize.value
    });
    if (response) {
      deckWords.value = response.data;
      deckWordsTotal.value = response.total;
    }
  } catch (e) {
    console.error(e);
  } finally {
    deckWordsLoading.value = false;
  }
}

const onWordDictChange = async () => {
  if (!selectedWordDictId.value) {
    dictWords.value = [];
    return;
  }
  wordsLoading.value = true;
  try {
    const request: DictionaryItemListRequest = {
      dictionaryId: selectedWordDictId.value,
      page: 1,
      pageSize: 1000 
    };
    const response = await DictionaryItemApi.list(request);
    if (response?.data?.data) {
      dictWords.value = response.data.data;
    }
  } finally {
    wordsLoading.value = false;
  }
}

const onAddWords = async () => {
  if (selectedWordIds.value.length === 0) return;
  try {
    await store.addWords(props.deckId, {
      wordIds: selectedWordIds.value
    });
    ElMessage.success(t('Common.SaveSuccess'));
    selectedWordIds.value = [];
    selectedWordDictId.value = '';
    dictWords.value = [];
    showAddForm.value = false;
    fetchDeckWords();
  } catch (e) {
    console.error(e);
  }
}

const onRemoveWords = async () => {
  if (selectedWords.value.length === 0) return;
  
  try {
    await ElMessageBox.confirm(
      `${t('Common.DeleteConfirm')} (${selectedWords.value.length})`,
      t('Common.Delete'),
      {
        confirmButtonText: t('Common.Yes'),
        cancelButtonText: t('Common.Cancel'),
        type: 'warning',
      }
    );

    await store.removeWords(props.deckId, selectedWords.value);
    ElMessage.success(t('Common.DeleteSuccess'));
    selectedWords.value = [];
    fetchDeckWords();
  } catch (e) {
    if (e !== 'cancel') {
      console.error(e);
    }
  }
}

const handlePageChange = (page: number) => {
  deckWordsPage.value = page;
  fetchDeckWords();
}

const handleSelectionChange = (selection: StudyDeckWordResponse[]) => {
  selectedWords.value = selection.map(item => item.id);
}

watch(() => props.isActive, (val) => {
  if (val) {
    fetchDeckWords();
  }
});

watch(() => props.deckId, () => {
  if (props.isActive) {
    fetchDeckWords();
  }
});
</script>

<template>
  <div>
    <div style="margin-bottom: 20px; display: flex; gap: 10px;">
      <el-button 
        type="primary" 
        :icon="Plus" 
        @click="showAddForm = !showAddForm"
      >
        {{ t('Common.Add') }}
      </el-button>
      <el-button 
        type="danger" 
        :icon="Delete" 
        :disabled="selectedWords.length === 0"
        @click="onRemoveWords"
      >
        {{ t('Common.Delete') }} ({{ selectedWords.length }})
      </el-button>
    </div>

    <el-collapse-transition>
      <el-form v-show="showAddForm" label-position="top" style="margin-bottom: 20px;">
        <el-form-item :label="t('StudyDeck.SelectDictionary')">
          <el-select v-model="selectedWordDictId" :placeholder="t('Common.Select')" style="width: 100%" @change="onWordDictChange">
            <el-option
              v-for="item in dictionaries"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            />
          </el-select>
        </el-form-item>
        <el-form-item :label="t('StudyDeck.SelectWords')">
          <el-select 
            v-model="selectedWordIds" 
            multiple 
            :placeholder="t('StudyDeck.SelectWords')" 
            style="width: 100%"
            :loading="wordsLoading"
            filterable
          >
            <el-option
              v-for="word in dictWords"
              :key="word.id"
              :label="word.term"
              :value="word.id"
            />
          </el-select>
        </el-form-item>
        <el-button type="primary" @click="onAddWords" :disabled="selectedWordIds.length === 0">
          {{ t('Common.Add') }}
        </el-button>
        <el-button @click="showAddForm = false">
          {{ t('Common.Cancel') }}
        </el-button>
      </el-form>
    </el-collapse-transition>

    <el-table 
      :data="deckWords" 
      style="width: 100%; margin-bottom: 20px;" 
      v-loading="deckWordsLoading"
      @selection-change="handleSelectionChange"
    >
      <el-table-column type="selection" width="55" />
      <el-table-column prop="term" :label="t('StudyDeck.Term')" />
      <el-table-column prop="meaning" :label="t('StudyDeck.Definition')" />
    </el-table>
    <el-pagination
      v-if="deckWordsTotal > 0"
      layout="prev, pager, next"
      :total="deckWordsTotal"
      :page-size="deckWordsPageSize"
      :current-page="deckWordsPage"
      @current-change="handlePageChange"
      style="margin-bottom: 20px; justify-content: center;"
    />
  </div>
</template>
