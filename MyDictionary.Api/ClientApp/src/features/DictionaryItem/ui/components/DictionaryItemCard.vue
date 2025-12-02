<script lang="ts" setup>
import { defineEmits, computed, ref } from 'vue';
import type { DictionaryItemType } from '../../types/models/DictionaryItemType';
import { Edit, Delete, ArrowRightBold } from '@element-plus/icons-vue' 
import { ButtonBase } from '@/components/ui/buttons';
import getWeightClassFunc from '../../helpers/getWeightClass';

const emit = defineEmits<{
  (e: 'edit', item: DictionaryItemType): void
  (e: 'delete', value: string): void
}>()

const props = defineProps<{
  modelValue: any,
  minWeight: number,
  maxWeight: number
}>()

const openIndex = ref();
const isOpen = ref<boolean>(false);

const item = computed(() => props.modelValue);

const onEdit = (item: DictionaryItemType) => {
  emit('edit', item);
}
const onDelete = (value: string) => {
  emit('delete', value);
}

function toggle() {
  isOpen.value = !isOpen.value
}

</script>

<template>
  <el-card style="width: 100%;" class="item-card">
    <div class="item__body">
      <div class="collapse-arrow"@click="toggle()">
        <el-icon><ArrowRightBold class="arrow" :class="{ 'arrow-open': isOpen}"/></el-icon>
        <!-- <i class="arrow" :class="{ 'arrow-open': isOpen }"></i> -->
      </div>
      <div :class="`info ${getWeightClassFunc(item.weight, minWeight, maxWeight)}`">
          <span class="info-weight__value">{{ item.weight }}</span>
          <span class="info-weight__label">{{ $t("DictionaryItem.Weight")}}</span>
      </div>
      <div class="words">
        <div>
          <span class="words__label text-mini">{{ $t("DictionaryItem.Term") }}:</span>
          <span>{{ item.term }}</span>
        </div>
        <div>
          <span class="words__label text-mini">{{ $t("DictionaryItem.Meaning") }}:</span>
          <span>{{ item.meaning }}</span>
        </div>
      </div>
      <div class="buttons">
        <ButtonBase type="primary" @click="onEdit(item)">
          <span>{{ $t('Common.Edit_2') }}</span>
          <el-icon style="margin-left: 5px;"><Edit /></el-icon>
        </ButtonBase>
        <ButtonBase type="danger" @click="onDelete(item.id)">
          <span>{{ $t('Common.Delete') }}</span>
          <el-icon style="margin-left: 5px;"><Delete /></el-icon>
        </ButtonBase>
      </div>      
    </div>

    <transition name="collapse">
      <div 
        v-show="isOpen" 
        class="collapse-content"
      >
        <slot name="content" :item="item">
          .collapse-enter-active,
.collapse-leave-active {
  transition: all .25s ease;
  overflow: hidden;
}

.collapse-enter,
.collapse-leave-to {
  max-height: 0;
  opacity: 0;
}

.collapse-content {
  padding: 10px 0;
}
        </slot>
      </div>
    </transition>
  </el-card>
</template> 
<style>
.item-card .el-card__body {
    display: flex;
    flex-direction: column;
    align-items: center;
}
</style>
<style scoped>
.item__body {
  display: flex;
  align-items: center;
  gap: 15px;
  width: 100%;
}
.info {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 6px 10px;
  border-radius: 8px;
  color: #fff;
}
.info-weight__value {
  font-size: 20px;
}
.info-weight__label {
  font-size: 10px;
}
.weight-green {
  background: linear-gradient(320deg,rgba(25, 150, 14, 1) 29%, rgba(143, 194, 16, 1) 100%);
}
.weight-yellow {
  background: linear-gradient(320deg,rgba(253, 201, 29, 1) 30%, rgba(252, 228, 69, 1) 100%);
  color: #000;
}
.weight-red {
  background: linear-gradient(320deg,rgba(253, 29, 29, 1) 30%, rgba(252, 176, 69, 1) 100%);
}
.words {
  flex: 3;
}
.words__label {
  display: inline-block;
  width: 70px;
}
.buttons {
  display: flex;
  justify-content: flex-end;
  flex: 1;
  gap: 10px;
}
</style>
<style>
.collapse-header {
  display: flex;
  align-items: center;
}

/* Клик-зона стрелки */
.collapse-arrow {
  width: 24px;
  height: 24px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}
.arrow {
  transition: all .25s ease;
}
.arrow-open {
  transform: rotate(90deg);
}

.collapse-enter-active,
.collapse-leave-active {
  transition: all .25s ease;
  overflow: hidden;
}
.collapse-enter,
.collapse-leave-to {
  max-height: 0;
  opacity: 0;
}

.collapse-content {
  padding: 10px 0 10px 32px;
}
</style>