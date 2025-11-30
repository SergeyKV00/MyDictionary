<template>
  <div class="dictionary flex-center">
    <el-card style="width: 90%;">
      <template #header >
        <div class="flex" style="align-items: center;">
          <span class="dictionary__header">Мои словари</span>
          <el-button type="success" class="large-icon" :icon="CirclePlus" circle text style="margin-left: 5px;"
            @click="openCreateDialog"
          />
        </div>
      </template>
      <div>
        <el-input v-model="filters.term" :placeholder="$t('DictionaryItem.Term')"/>
      </div>
      <div class="dictionary__body" v-loading="loading">
        <el-card 
          v-for="dict in dictionaries" 
          :key="dict.id" 
          class="dictionary-card" 
          hover
          @click="routeToDictionaryItem(dict)"
        >
          <div class="dictionary-card__body flex-column">
            <div class="flex-space" style="align-items: center;">
              <span>{{ dict.name }}</span>
              <el-button type="dunger" class="dictionary__delete" :icon="Delete" circle text
                @click="onDelete(dict)"
              />
            </div>
            <div class="flex-space">
              <span class="wh-100"></span>
              <div class="text-muted flex" style="column-gap: 5px; margin-top: 5px; align-items: center;">
                <span><el-icon><Files /></el-icon></span>
                <span>{{ dict.itemCount }}</span>
                <span>карточек</span>
              </div>
            </div>
            <div style="display: flex; flex-direction: column; align-items: center;">
              <DictionaryItemCardLight 
                v-for="(item, index) in getMatches(dict.id)" 
                :key="item.id"
                :item="item" 
                :minWeight="item.minWeight"
                :maxWeight="item.maxWeight"
              />
            </div>
          </div>
        </el-card>
      </div>
    </el-card>
  </div>
  <el-dialog
    title="Создать словарь"
    v-model="isCreateDialogVisible"
    width="400px"
  >
    <el-form ref="createForm" :model="newDictionary">
      <el-form-item label="Название" prop="name">
        <el-input v-model="newDictionary.name" placeholder="Введите название словаря" />
      </el-form-item>
    </el-form>

    <template #footer>
      <el-button @click="isCreateDialogVisible = false">Отмена</el-button>
      <el-button type="primary" @click="createDictionary">Создать</el-button>
    </template>
  </el-dialog>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch, computed } from 'vue';
import { useRouter } from 'vue-router';
import { ElMessageBox, ElMessage } from 'element-plus';
import { useDictionaryItemStore } from '@/features/DictionaryItem/store/DictionaryItemStore';
import DictionaryItemCardLight from '@/features/DictionaryItem/ui/components/DictionaryItemCardLight.vue';

import DictionaryService from '@/services/dictionary';
import DictionaryItemService from '@/services/dictionaryItem';
import type { DictionaryDto } from '@/models/dto/dictionary/DictionaryDto';
import type { DictionaryListRequest } from '@/models/dto/dictionary/DictionaryListRequest';
import type { CreateDictionaryRequest } from '@/models/dto/dictionary/CreateDictionaryRequest';
import { Search, CirclePlus, Delete, Files } from '@element-plus/icons-vue'
import type { DictionaryItemType } from '@/features/DictionaryItem/types/models/DictionaryItemType';

interface UserDictionarySearchAcross {
  dictionaryId: string,
  minWeight: number,
  maxWeight: number,
  matches: DictionaryItemType[]
}

const router = useRouter();
const dictionaryItemStore = useDictionaryItemStore();

const dictionaries = ref<DictionaryDto[]>([]);
const total = ref(0);
const loading = ref(false);

const filters = ref({
  term: ""
})
const searchAcrossResults = ref<UserDictionarySearchAcross[]>([])

const dictionaryRequest = ref<DictionaryListRequest>({
  isIncludeItems: true
})

watch(
  filters,
  () => searchAcross(),
  {deep: true}
)

function getMatches(dictionaryId: string) {
  const item = searchAcrossResults.value.find(r => 
    r.dictionaryId == dictionaryId &&
    r.matches.length > 0)

  return item?.matches.map(m => {
    return {...m, minWeight: item.minWeight, maxWeight: item.maxWeight}
  }) ?? []; 
}

async function searchAcross() {
  const response = await DictionaryService.searchAcross(filters.value.term);
  if (response?.error != null) return;

  searchAcrossResults.value = (response?.data ?? []) as UserDictionarySearchAcross[];
}

const loadDictionaryList = async () => {
  loading.value = true;

  const response = await DictionaryService.list(dictionaryRequest.value);
  if (response?.error != null) 
    return;

  const result = response?.data; 
  dictionaries.value = result?.data ?? [];
  total.value = result?.total ?? 0;

  dictionaries.value.forEach(async d => {
    d.itemCount = (await getCount(d.id)) ?? 0;
  });

  loading.value = false;
}
const getCount = async (dictionaryId: string) => {
  const response = await DictionaryItemService.count(dictionaryId);
  if (response?.error != null)
    return 0;

  return response?.data;
}

const routeToDictionaryItem = (dict: DictionaryDto) => {
  // dictionaryItemStore.dictionaryName = dict.name; // TODO_U Delete change on dictionaryStore
  // router.push({name: "DictionaryItems", params: { dictionaryId: dict.id }});
  dictionaryItemStore.setDictionary(dict);
  router.push({name: "DictionaryItems" });
}

const isCreateDialogVisible = ref(false);
const newDictionary = ref<CreateDictionaryRequest>({ name: '' });

const openCreateDialog = () => {
  console.log('openCreateDialog')
  newDictionary.value.name = '';
  isCreateDialogVisible.value = true;
}

const createDictionary = async () => {
  if (!newDictionary.value.name) return;

  try {
    await DictionaryService.create(newDictionary.value);
    isCreateDialogVisible.value = false;
    await loadDictionaryList();
  } catch (err) {
    console.error("Ошибка создания словаря", err);
  }
}

const onDelete = async (dint: DictionaryDto) => {
 await ElMessageBox.confirm(
      `Вы уверены, что хотите удалить словарь "${dint.name}"?`,
      'Подтверждение удаления',
      {
        confirmButtonText: 'Да',
        cancelButtonText: 'Отмена',
        type: 'warning',
      }
    );
  
  await DictionaryService.delete(dint.id);
  await loadDictionaryList();
}

onMounted(async () => {
  await loadDictionaryList();
})

</script>
<style>
.dictionary__header { 
  font-weight: bold;
  font-size: 25px;
  color: var(--el-color-primary);
}
.dictionary__body {
  position: relative;
  display:  flex;
  flex-wrap: wrap;
  gap: 15px;
  margin-top: 15px;
  align-items: flex-start;
  justify-content: center;
}
.dictionary-card {
  cursor: pointer;
  border-bottom: 5px solid transparent;
}
.dictionary-card__body { 
  width: 500px;
}
.dictionary-card .el-card__body {
  padding-bottom: 5px;
}
.dictionary__delete {
  font-size: 20px;
  color: var(--el-color-danger);
  cursor: pointer;
}
.dictionary__delete:hover {
  color: var(--el-color-danger-dark-2);
}
.dictionary-card:hover {
  border-bottom: 5px solid var(--el-color-primary);
}
</style>