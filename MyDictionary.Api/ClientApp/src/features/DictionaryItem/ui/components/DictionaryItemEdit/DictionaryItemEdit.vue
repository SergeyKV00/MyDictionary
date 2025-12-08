<script setup lang="ts">
import ButtonBase from '@/components/ui/buttons/ButtonBase.vue';
import FormInput from '@/components/ui/form/FormInput.vue';
import DictionaryItemApi from '@/features/DictionaryItem/api/DictionaryItemApi';
import { useDictionaryItemStore } from '@/features/DictionaryItem/store/DictionaryItemStore';
import { DictionaryItemType } from '@/features/DictionaryItem/types/models/DictionaryItemType';
import type { DictionaryItemCreateRequests } from '@/features/DictionaryItem/types/requests/DictionaryItemCreateRequests';
import type { DictionaryItemRequest } from '@/features/DictionaryItem/types/requests/DictionaryItemRequest';
import { NotificationService } from '@/services/notification';
import { ref } from 'vue';
import { t } from '@/locales/i18';
import { useLayoutStore } from '@/stores/layoutStore';
import DictionaryItemExamplesEdit from './DictionaryItemExamplesEdit.vue';

const itemStore = useDictionaryItemStore();
const layoutStore = useLayoutStore();

const emit = defineEmits<{
  (e: "update") : void,
  (e: "close") : void
}>()

const exampleEditRef = ref();
const drawerVisible = ref(false);
const activeNames = ref("examples");
const item = ref<DictionaryItemType>(createEmptyItem());
const itemId = ref<string>();
const query = ref<DictionaryItemRequest>({
  id: "",
  isIncludeExample: true
})

function createEmptyItem() : DictionaryItemType {
  return {
    id: "",
    term: "",
    meaning: "",
    weight: 1,
    examples: []
  }
}

async function fetchItem() {
  if (itemId.value == null)
    return;

  query.value.id = itemId.value;
  const response = await DictionaryItemApi.get(query.value);
  if (response?.error != null) {
    console.log(response?.error);
    return;
  }

  if (response?.data)
    item.value = response.data
}

function onSave() {
  let saveEvent = "id" in item.value && item.value.id
    ? itemStore.update(item.value)
    : itemStore.create({...item.value, dictionaryId: itemStore.dictionary?.id! } as DictionaryItemCreateRequests)

  saveEvent
    .then((itemId) => {
      if (itemId != null) {
        item.value.id = itemId;
        return exampleEditRef.value.saveExamples(itemId, item.value.examples);
      }
        
    })
    .then(() => {
      close();
      emit('update');
      NotificationService.notifySuccess(t("Common.SaveSuccess"))
    })
    .catch(err => console.error(err))
}

function open(dictionaryItemId: string) {
  itemId.value = dictionaryItemId;
  fetchItem();
  drawerVisible.value = true;
  layoutStore.hideSidebar();
}
function close() {
  item.value = createEmptyItem();
  itemId.value = undefined;
  drawerVisible.value = false;
  emit('close');
  layoutStore.showSidebar();
}

// TEMP
const verbForms = ref([
  {
    infinitive: "take ",
    pastSimple: "took",
    pastParticiple: "taken"
  }
]);

defineExpose({ open });
</script>
<template>
  <el-drawer 
    v-model="drawerVisible"
    size="40%"
    :modal="false"
    :with-header="false" 
    modal-penetrable 
    @close="close"
  >
    <div class="dictionary-item-edit">
      <div class="content">
        <el-form 
          :model="item"
          label-width="auto"
          label-position="left"
        >
          <FormInput v-model="item.term" :label="$t('DictionaryItem.Term')"/>
          <FormInput v-model="item.meaning" :label="$t('DictionaryItem.Meaning')"/>
          <FormInput v-model="item.weight" :label="$t('DictionaryItem.Weight')"/>
        </el-form>

        <el-collapse v-model="activeNames" expand-icon-position="left">
          <el-collapse-item title="Формы глагола" name="wordForms">
              <el-table :data="verbForms" border style="width: 100%">
                <el-table-column prop="infinitive" label="Infinitive" />
                <el-table-column prop="pastSimple" label="Past Simple" />
                <el-table-column prop="pastParticiple" label="Past Participle" />
              </el-table>
          </el-collapse-item>
          <el-collapse-item title="Примеры" name="examples">
            <DictionaryItemExamplesEdit ref="exampleEditRef" :item="item" :examples="item.examples"/>
          </el-collapse-item>
        </el-collapse>
      </div>
      <div class="footer">
        <ButtonBase type="primary" @click="close">
          {{ $t('Common.Cancel') }}
        </ButtonBase>
        <ButtonBase type="success" @click="onSave">
          {{ $t('Common.Save') }}
        </ButtonBase>
      </div>
    </div>
  </el-drawer>
</template>
<style>
.dictionary-item-edit .el-collapse-item__header {
  background: var(--el-color-primary);
  color: white;
}
.dictionary-item-edit .el-collapse-item__arrow {
  margin-left: 5px;
}
.dictionary-item-edit .el-collapse-item__wrap {
  border: var(--el-color-primary) 1.5px dotted;
  border-top: 0;
}
.dictionary-item-edit .el-collapse-item__content {
  padding: 0px;
}
</style>
<style scoped>
.dictionary-item-edit {
  display: flex;
  flex-direction: column;
  height: 100%;
}
.footer {
  display: flex;
  justify-content: space-around;
  margin-top: auto;
  padding-top: 16px;
}
</style>