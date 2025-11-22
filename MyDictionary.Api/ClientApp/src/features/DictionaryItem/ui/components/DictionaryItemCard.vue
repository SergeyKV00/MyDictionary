<script lang="ts" setup>
import { defineEmits, computed } from 'vue';
import type { DictionaryItemType } from '../../types/models/DictionaryItemType';
import { Edit, Delete } from '@element-plus/icons-vue' 
import { ButtonBase } from '@/components/ui/buttons';

const emit = defineEmits<{
  (e: 'edit', flag: boolean, item: DictionaryItemType): void
  (e: 'delete', value: string): void
}>()

const props = defineProps({
  modelValue: { type: null as any}
})
const item = computed(() => props.modelValue);

const onEdit = (flag: boolean, item: DictionaryItemType) => {
  emit('edit', flag, item);
}
const onDelete = (value: string) => {
  emit('delete', value);
}

</script>

<template>
  <el-card class="dictionaryItem__card">
    <div style="display: flex;">
      <div class="dictionaryItem__item">
        <div class="dictionaryItem__item__value">
          <span class="text-mini" style="width: 70px; display: inline-block;">{{ $t("DictionaryItem.Term") }}: </span>
          {{ item.term }}
        </div>
        <div class="dictionaryItem__item__value">
          <span class="text-mini" style="width: 70px; display: inline-block;">{{ $t("DictionaryItem.Meaning") }}: </span>
          {{ item.meaning }}
        </div>
        <div class="dictionaryItem__item__value">
          <span class="text-mini" style="width: 70px; display: inline-block;">{{ $t("DictionaryItem.Weight") }}: </span>
          {{ item.weight }}
        </div>
      </div>
      <div class="dictionaryItem__card_btn">
        <ButtonBase type="success" @click="onEdit(true, item)">
          <el-icon><Edit /></el-icon>
        </ButtonBase>
        <ButtonBase type="danger" @click="onDelete(item.id)">
          <el-icon><Delete /></el-icon>
        </ButtonBase>
      </div>
    </div>
  </el-card>
</template> 

<style>
.dictionaryItem__card {
  width: 100%;
  position: relative;
}
.dictionaryItem__card_btn {
  position: absolute;
  top: -5px;
  right: -14px;
  transform: scale(0.7);
}
.dictionaryItem__item {
  width: 380px;
  display: flex;
  flex-direction: column;
  gap: 5px;
}
</style>