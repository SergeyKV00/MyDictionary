<script lang="ts" setup>
import { onMounted, computed, ref } from 'vue';
import { useDictionaryItemStore } from '../store/DictionaryItemStore';
import { CirclePlus, Delete, Edit } from '@element-plus/icons-vue' 
import { FormInput } from '@/components/ui/form';
import { ButtonBase, ButtonLink } from '@/components/ui/buttons';
import type { DictionaryItemCreateRequests } from '../types/requests/DictionaryItemCreateRequests';
import type { DictionaryItemType } from '../types/models/DictionaryItemType';
import { NotificationService } from '@/services/notification';
import { t } from '@/locales/i18';
import { ElMessageBox } from 'element-plus';
import DictionaryItemCard from './components/DictionaryItemCard.vue'

const itemStore = useDictionaryItemStore();

const dictionary = computed(() => itemStore.getDictionary );
const items = computed(() => itemStore.dictionaryItems ?? []);

const itemForm = ref<DictionaryItemCreateRequests | DictionaryItemType>({
  dictionaryId: dictionary.value?.id!,
  term: "",
  meaning: "",
  weight: 0
})
const isEdit = ref(false);

onMounted(async() => {
  if (dictionary.value?.id != null) {
    await itemStore.fetchDictionaryItems({ dictionaryId: dictionary.value?.id });
  }
})

const onEdit = (val: boolean, item?: DictionaryItemType) => {
  isEdit.value = val;
  itemForm.value = item ? item : {
    dictionaryId: dictionary.value?.id!,
    term: "",
    meaning: "",
    weight: 0
  }
}

const onSave = async () => {
  try {
    var item = itemForm.value as DictionaryItemType;
    if (item.id) {
      await itemStore.update(item);
    } else {
      await itemStore.create(itemForm.value as DictionaryItemCreateRequests);
    }
    NotificationService.notifySuccess(t("Common.SaveSuccess"))
    onEdit(false);
  } catch (err) {
    console.error(err);
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
          @click="onEdit(true)"
        />
      </div>
    </template>

    <transition name="slide-down">
      <el-card v-if="isEdit" style="width: 100%;">
        <el-form
          :model="itemForm"
        >
          <FormInput v-model="itemForm.term" :placeholder="$t('DictionaryItem.Term')"/>
          <FormInput v-model="itemForm.meaning" :placeholder="$t('DictionaryItem.Meaning')"/>
          <FormInput v-model="itemForm.weight" :placeholder="$t('DictionaryItem.Weight')"/>
        </el-form>
        <div style="display: flex; justify-content: flex-end; gap: 10px;">
          <ButtonBase type="info" @click="onEdit(false)">
            {{ $t('Common.Cancel') }}
          </ButtonBase>
          <ButtonBase type="success" @click="onSave">
            {{ $t('Common.Save') }}
          </ButtonBase>
        </div>
      </el-card>
    </transition>

    <div class="dictionaryItem__body">
      <div v-for="(item, index) in items" :key="item.id">
        <DictionaryItemCard v-model="items[index]" @edit="onEdit" @delete="onDelete" />
      </div>
    </div>
  </el-card>
</template> 

<style scoped>
/* Эффект выезда сверху вниз */
.slide-down-enter-active,
.slide-down-leave-active {
  transition: all 0.5s ease;
}

.slide-down-enter-from,
.slide-down-leave-to {
  opacity: 0;
  transform: translateY(-20px);
}

.slide-down-enter-to,
.slide-down-leave-from {
  opacity: 1;
  transform: translateY(0);
}
</style>

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
}
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
.dictionaryItem__body {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 5px 15px;
}
.dictionaryItem__item {
  width: 380px;
  display: flex;
  flex-direction: column;
  gap: 5px;
}
</style>