<script lang="ts" setup>
import { onMounted, computed, ref, watch } from 'vue';
import { useDictionaryItemStore } from '../store/DictionaryItemStore';
import { CirclePlus, Delete, Edit } from '@element-plus/icons-vue' 
import DictionaryItemEdit from './components/DictionaryItemEdit.vue';
import type { DictionaryItemType } from '../types/models/DictionaryItemType';
import { NotificationService } from '@/services/notification';
import { t } from '@/locales/i18';
import { ElMessageBox } from 'element-plus';
import DictionaryItemCardNew from './components/DictionaryItemCardNew.vue';
import Pagination from '@/components/Pagination.vue';
import type { DictionaryItemListRequest } from '../types/requests/DictionaryItemListRequest';
import type { AsyncDialogResult } from '@/components/AsyncDialog.vue';

const itemStore = useDictionaryItemStore();

const dictionary = computed(() => itemStore.getDictionary );
const items = computed(() => {
  const _items = itemStore.dictionaryItems ?? [];
  return _items.sort((a, b) => b.weight - a.weight);
});
const minWeight = computed(() => itemStore.minWeight);
const maxWeight = computed(() => itemStore.maxWeight);
const pageSize = computed(() => itemStore.query.limit);

const currentPage = ref(1);
const itemEditRef = ref();

const filters = ref<DictionaryItemListRequest>({
  dictionaryId: dictionary.value?.id!
})

onMounted(async() => {
  itemStore.query.limit = 10;
  await fetchItems();
})

watch([currentPage, pageSize], () => {
  itemStore.query.offset = (currentPage.value - 1) * pageSize.value;
  fetchItems();
});

async function fetchItems() {
  await itemStore.fetchDictionaryItems(filters.value);
}

async function onCreateOrEdit(item?: DictionaryItemType) {
  const result = await itemEditRef.value.openAsync(item) as AsyncDialogResult
  if (result.isSuccess) {
    item = result.value;
  }
}

const onDelete = async (id: string) => {
  try {
    await ElMessageBox.confirm( // TODO_U
      `Вы уверены, что хотите удалить?`,
      'Подтверждение удаления',
      {
        confirmButtonText: 'Да',
        cancelButtonText: 'Отмена',
        type: 'warning',
      }
    );

    await itemStore.delete(id);
    NotificationService.notifySuccess(t("Common.DeleteSuccess"))
  } catch (err) {
    console.error(err);
  }
}

</script>

<template>
  <el-card v-loading="itemStore.loading" class="dictionaryItem">
    <template #header>
      <div class="flex" style="align-items: center;">
        <span class="dictionaryItem__header">{{ dictionary?.name }}</span>
        <el-button type="success" class="large-icon" :icon="CirclePlus" circle text style="margin-left: 5px;"
          @click="onCreateOrEdit()"
        />
      </div>
    </template>

    <DictionaryItemEdit ref="itemEditRef"/>

    <div class="dictionaryItem__body">
      <div class="dictionaryItem__cards-wrapper">
        <DictionaryItemCardNew 
          v-for="(item, index) in items" 
          :key="item.id"
          v-model="items[index]" 
          :minWeight="minWeight"
          :maxWeight="maxWeight"
          @edit="onCreateOrEdit" 
          @delete="onDelete" 
        />
      </div>
    </div>

    <Pagination class="pagination"
      :total="itemStore.dictionaryItemTotal"
      v-model:currentPage="currentPage"
      v-model:pageSize="pageSize"
    />
  </el-card>
</template> 
<style>
.dictionaryItem__header { 
  font-weight: bold;
  font-size: 25px;
  color: var(--el-color-primary);
}
.dictionaryItem .el-card__body {
  display: flex;
  flex-direction: column;
  gap: 15px;
  padding: 5px 10px;
}
.dictionaryItem__body {
  height: calc(100vh - 265px);
  display: flex;
  flex-direction: column;
}
.dictionaryItem__cards-wrapper {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 5px;
  overflow-y: auto;
  padding: 10px;
}
</style>