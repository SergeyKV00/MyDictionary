<script lang="ts" setup>
import { ref, watch } from 'vue';
import { useStudyDeckStore } from '../../store/StudyDeckStore';
import { t } from '@/locales/i18';
import type { DictionaryDto } from '@/models/dto/dictionary/DictionaryDto';
import type { StudyDeckDictionaryResponse } from '../../types/responses/StudyDeckDetailResponse';
import { ElMessage, ElMessageBox } from 'element-plus';
import { Notebook, Delete, Plus, RemoveFilled } from '@element-plus/icons-vue';

const props = defineProps<{
  deckId: string;
  dictionaries: DictionaryDto[];
}>();

const store = useStudyDeckStore();
const boundDictionaries = ref<StudyDeckDictionaryResponse[]>([]);
const selectedBindingDictId = ref<string>('');
const isSync = ref(false);
const showAddForm = ref(false);

const fetchDeckDetails = async () => {
  if (!props.deckId) return;
  try {
    const details = await store.get(props.deckId);
    if (details) {
      boundDictionaries.value = details.dictionaries;
    }
  } catch (e) {
    console.error(e);
  }
}

const onBind = async () => {
  if (!selectedBindingDictId.value) return;
  try {
    await store.addDictionary(props.deckId, {
      dictionaryId: selectedBindingDictId.value,
      isSynchronized: isSync.value
    });
    ElMessage.success(t('Common.SaveSuccess'));
    selectedBindingDictId.value = '';
    isSync.value = false;
    showAddForm.value = false;
    fetchDeckDetails();
  } catch (e) {
    console.error(e);
  }
}

const onSyncToggle = async (item: StudyDeckDictionaryResponse) => {
  try {
    await store.updateDictionary(props.deckId, {
      dictionaryId: item.dictionaryId,
      isSynchronized: item.isSynchronized
    });
    ElMessage.success(t('Common.SaveSuccess'));
  } catch (e) {
    console.error(e);
    item.isSynchronized = !item.isSynchronized;
  }
}

const onRemoveDictionary = async (item: StudyDeckDictionaryResponse) => {
  try {
    await ElMessageBox.confirm(
      t('Common.DeleteConfirm'),
      t('Common.Delete'),
      {
        confirmButtonText: t('Common.Yes'),
        cancelButtonText: t('Common.Cancel'),
        type: 'warning',
      }
    );

    await store.removeDictionary(props.deckId, item.dictionaryId);
    ElMessage.success(t('Common.DeleteSuccess'));
    fetchDeckDetails();
  } catch (e) {
    if (e !== 'cancel') {
      console.error(e);
    }
  }
}

watch(() => props.deckId, (newId) => {
  if (newId) fetchDeckDetails();
}, { immediate: true });

defineExpose({
  fetchDeckDetails
});
</script>

<template>
  <div>
    <div style="margin-bottom: 20px;">
      <el-button 
        type="primary" 
        :icon="Plus" 
        @click="showAddForm = !showAddForm"
      >
        {{ t('Common.Add') }}
      </el-button>
    </div>

    <el-collapse-transition>
      <el-form v-show="showAddForm" label-position="top" style="margin-bottom: 20px;">
        <el-form-item :label="t('StudyDeck.SelectDictionary')">
          <el-select v-model="selectedBindingDictId" :placeholder="t('Common.Select')" style="width: 100%">
            <el-option
              v-for="item in dictionaries"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-checkbox v-model="isSync" :label="t('StudyDeck.Synchronize')" border />
        </el-form-item>
        <el-button type="primary" @click="onBind" :disabled="!selectedBindingDictId">
          {{ t('Common.Add') }}
        </el-button>
        <el-button @click="showAddForm = false">
          {{ t('Common.Cancel') }}
        </el-button>
      </el-form>
    </el-collapse-transition>

    <div class="deck-dictionaries">
      <el-card 
        v-for="item in boundDictionaries" 
        :key="item.dictionaryId" 
        class="dictionary-card" 
        shadow="hover"
      >
        <div class="dictionary-card__header">
            <el-checkbox 
              v-model="item.isSynchronized" 
              :label="t('StudyDeck.Synchronize')" 
              size="small"
              @change="onSyncToggle(item)"
            />
            <el-button 
							type="danger" 
							:icon="RemoveFilled" 
							circle 
							size="small"
							@click="onRemoveDictionary(item)"
            />
        </div>
        <div class="dictionary-card__footer">
          <el-icon :size="20"><Notebook /></el-icon>
          <a :href="`/dictionaryItems?dictionaryId=${item.dictionaryId}`" target="_blank" class="dictionary-link">
            {{ item.name }}
          </a>
        </div>
      </el-card>
    </div>
  </div>
</template>

<style>
.deck-dictionaries .el-card__body{
	padding: 0;
}
</style>
<style scoped>
.deck-dictionaries {
  display: flex;
  flex-wrap: wrap;
  gap: 15px;
  margin-bottom: 20px;
}

.dictionary-card {
  width: 250px;
}

.dictionary-card__header {
	display: flex;
	align-items: center;
	gap: 10px;
	justify-content: space-between;
	font-weight: bold;
	font-size: 16px;
	padding: 5px 10px;
}

.dictionary-link {
  text-decoration: none;
  color: var(--el-text-color-primary);
}

.dictionary-link:hover {
  color: var(--el-color-primary);
  text-decoration: underline;
}

.dictionary-card__footer {
  display: flex;
  justify-content: flex-start;
	align-items: center;
	font-size: 18px;
	padding: 10px;
}
</style>
