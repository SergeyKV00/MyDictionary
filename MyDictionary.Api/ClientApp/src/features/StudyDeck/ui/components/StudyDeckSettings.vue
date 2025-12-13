<script lang="ts" setup>
import { ref } from 'vue';
import { t } from '@/locales/i18';
import DictionaryService from '@/services/dictionary';
import type { DictionaryDto } from '@/models/dto/dictionary/DictionaryDto';
import type { DictionaryListRequest } from '@/models/dto/dictionary/DictionaryListRequest';
import StudyDeckDictionaries from './StudyDeckDictionaries.vue';
import StudyDeckWords from './StudyDeckWords.vue';

const props = defineProps<{
  deckId: string;
}>();

const emit = defineEmits<{
  (e: 'close'): void
}>();

const dialogVisible = ref(false);
const activeTab = ref('binding');
const dictionaries = ref<DictionaryDto[]>([]);

const open = () => {
    dialogVisible.value = true;
    activeTab.value = 'binding';
    fetchDictionaries();
}

const handleClose = () => {
    dialogVisible.value = false;
    emit('close');
}

const fetchDictionaries = async () => {
    const query: DictionaryListRequest = { isIncludeItems: false }

    const response = await DictionaryService.list(query);
    if (response?.data?.data) {
        dictionaries.value = response.data.data;
    }
}

defineExpose({
    open
});
</script>

<template>
  <el-dialog
    v-model="dialogVisible"
    :title="t('StudyDeck.Settings')"
    width="60%"
    :before-close="handleClose"
  >
    <el-tabs v-model="activeTab">
      <el-tab-pane :label="t('StudyDeck.Binding')" name="binding">
            <StudyDeckDictionaries 
                :deck-id="deckId" 
                :dictionaries="dictionaries" 
            />
      </el-tab-pane>
      
      <el-tab-pane :label="t('StudyDeck.AddWords')" name="words">
            <StudyDeckWords 
                :deck-id="deckId" 
                :dictionaries="dictionaries"
                :is-active="activeTab === 'words'"
            />
      </el-tab-pane>
    </el-tabs>
  </el-dialog>
</template>
