<script lang="ts" setup>
import { ref } from 'vue';
import { t } from '@/locales/i18';
import { useDictionaryItemStore } from '../../store/DictionaryItemStore';
import { NotificationService } from '@/services/notification';
import AsyncDialog from '@/components/AsyncDialog.vue';
import FormInput from '@/components/ui/form/FormInput.vue';
import { ButtonBase } from '@/components/ui/buttons';
import { DictionaryItemType } from '../../types/models/DictionaryItemType';
import { DictionaryItemCreateRequests } from '../../types/requests/DictionaryItemCreateRequests';

const itemStore = useDictionaryItemStore();
const localModel = ref<DictionaryItemType | DictionaryItemCreateRequests | null>(null);
const dialogRef = ref();
const title = ref();

async function openAsync(model: DictionaryItemType | null) {
  if (model) {
    title.value = t('Common.Editing');
    localModel.value = DictionaryItemType.clone(model);
  } else {
    title.value = t('Common.Creating');
    localModel.value = new DictionaryItemCreateRequests(itemStore.dictionary?.id!);
  }

  return await dialogRef.value.openAndWaitResult();
}

function onSave() {
  let saveEvent = localModel.value instanceof DictionaryItemType
    ? itemStore.update(localModel.value)
    : itemStore.create(localModel.value as DictionaryItemCreateRequests)

  saveEvent
    .then(() => {
      NotificationService.notifySuccess(t("Common.SaveSuccess"))
      dialogRef.value.onConfirm(localModel.value)
    })
    .catch(err => console.error(err))
}

function onCancel() {
  dialogRef.value.onCancel()
}

defineExpose({ openAsync });
</script>

<template>
  <AsyncDialog 
    ref="dialogRef"
    :title="title"
    :loading="itemStore.loading"
  >
    <el-form
      :model="localModel"
    >
      <FormInput v-model="localModel!.term" :placeholder="$t('DictionaryItem.Term')"/>
      <FormInput v-model="localModel!.meaning" :placeholder="$t('DictionaryItem.Meaning')"/>
      <FormInput v-model="localModel!.weight" :placeholder="$t('DictionaryItem.Weight')"/>
    </el-form>
    <div style="display: flex; justify-content: flex-end; gap: 10px;">
    <ButtonBase type="info" @click="onCancel">
      {{ $t('Common.Cancel') }}
    </ButtonBase>
    <ButtonBase type="success" @click="onSave">
      {{ $t('Common.Save') }}
    </ButtonBase>
  </div>
  </AsyncDialog>
</template> 