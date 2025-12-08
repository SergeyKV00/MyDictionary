<script lang="ts" setup>
import { reactive, ref, computed } from 'vue';
import type { DictionaryItemExampleType } from '@/features/DictionaryItem/types/models/DictionaryItemExampleType';
import { Expand, Check, CircleClose, SuccessFilled } from '@element-plus/icons-vue' 
import type { DictionaryItemType } from '@/features/DictionaryItem/types/models/DictionaryItemType';
import DictionaryItemExampleApi from '@/features/DictionaryItem/api/DictionaryItemExampleApi';
import type { DictionaryItemExampleListRequest } from '@/features/DictionaryItem/types/requests/DictionaryItemExampleListRequest';

const props = defineProps<{
  item: DictionaryItemType,
  examples: DictionaryItemExampleType[] 
}>()


const editingKey = ref<string | null>(null);
const editingCopy = reactive<Partial<DictionaryItemExampleType>>({});

function startEdit(key: string, item?: DictionaryItemExampleType) {
  editingKey.value = key;
  if (item) Object.assign(editingCopy, item);
  else {
    editingCopy.example = '';
    editingCopy.translation = '';
  }
}

function cancelEdit() {
  editingKey.value = null;
  editingCopy.example = '';
  editingCopy.translation = '';
}

function saveEdit(index?: number) {
  if (!editingCopy.example) return;

  if (index !== undefined && props.examples[index]) {
    props.examples[index].example = editingCopy.example;
    props.examples[index].translation = editingCopy.translation;
  } else {
    props.examples.push({
      example: editingCopy.example!,
      translation: editingCopy.translation!,
    } as DictionaryItemExampleType);
  }

  cancelEdit();
}

function deleteExample(index: number) {
  props.examples.splice(index, 1);
}

const displayedExamples = computed(() => {
  if (editingKey.value === 'new') return [...props.examples, editingCopy as DictionaryItemExampleType];
  return props.examples;
});

const isEdit = (index: number) => {
  return editingKey.value === index.toString() || (editingKey.value ==='new' && index === props.examples.length)
}

async function saveExamples(dictionaryItemId: string, examples: DictionaryItemExampleType[]) {
  const response = await DictionaryItemExampleApi.list({dictionaryItemId: props.item.id} as DictionaryItemExampleListRequest)
  if (response?.error != null) {
    throw new Error(response?.error.message);
  }
  const originalExamples = response?.data?.data ?? [];

  const toCreate = examples.filter(ex => !ex.id).map(ex => ({
    ...ex,
    dictionaryItemId
  }));

  const toUpdate = examples.filter(ex => ex.id);

  const toDelete = originalExamples.filter(
    oldEx => !examples.find(ex => ex.id === oldEx.id)
  );

  await Promise.all(toCreate.map(ex => DictionaryItemExampleApi.create(ex)));

  await Promise.all(toUpdate.map(ex => DictionaryItemExampleApi.update(ex)));

  await Promise.all(toDelete.map(ex => DictionaryItemExampleApi.delete(ex.id!)));
}

defineExpose({ saveExamples });
</script>
<template>
  <div class="examples">
    <!-- Кнопка добавления нового примера -->
    <el-button type="info" @click="startEdit('new')">{{ $t('Example.Add') }}</el-button>

    <div v-for="(item, index) in displayedExamples" :key="index" class="example-item">
      <div class="example-item__content" :class="{'edit': isEdit(index)}">
        <template v-if="isEdit(index)">
          <el-input v-model="editingCopy.example" placeholder="Example" size="small" style="margin-bottom: 5px;" />
          <el-input v-model="editingCopy.translation" placeholder="Translation" size="small" />
        </template>
        <template v-else>
          <div>{{ item.example }}</div>
          <div>{{ item.translation }}</div>
        </template>
      </div>
      <div class="example-item__action">
        <template v-if="isEdit(index)">
          <el-button 
          type="danger"
          circle
          :icon="CircleClose" 
          @click="cancelEdit">
        </el-button>
        <el-button 
            type="success" 
            :icon="SuccessFilled"
            circle
            :disabled="!editingCopy.example" 
            @click="saveEdit(index)">
          </el-button>
        </template>
        <template v-else>
          <el-dropdown placement="bottom">
            <el-button type="primary" circle>
              <el-icon style="color: white"><Expand /></el-icon>
            </el-button>
            <template #dropdown>
              <el-dropdown-menu>
                <el-dropdown-item @click="startEdit(index.toString(), item)">
                  {{ $t('Common.Edit') }}
                </el-dropdown-item>
                <el-dropdown-item @click="deleteExample(index)">
                  {{ $t('Common.Delete') }}
                </el-dropdown-item>
              </el-dropdown-menu>
            </template>
          </el-dropdown>
        </template>
      </div>
    </div>
  </div>
</template>
<style scoped>
.examples {
  display: flex;
  flex-direction: column;
  gap: 5px;
  padding: 5px;
  background: #ececec;
}
.example-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border: var(--el-border-color-light) 1px solid;
  border-radius: 5px;
  padding: 0px 10px;
  background: white;
}
.edit  {
  padding: 5px 0px;
}
.dropdown__btn {
  margin-left: 0px;
  width: 100%;
  max-height: 30px;
  transform: scale(0.8);
}
</style>