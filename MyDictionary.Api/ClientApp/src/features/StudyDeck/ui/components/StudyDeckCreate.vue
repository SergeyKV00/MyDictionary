<script lang="ts" setup>
import { ref, reactive, computed } from 'vue';
import { useStudyDeckStore } from '../../store/StudyDeckStore';
import { t } from '@/locales/i18';

const emit = defineEmits<{
  (e: 'close'): void
}>();

const store = useStudyDeckStore();
const formRef = ref();
const form = reactive({
  name: '',
  description: ''
});

const rules = computed(() => ({
  name: [
    { required: true, message: t('StudyDeck.Validation.NameRequired'), trigger: 'blur' },
    { min: 1, max: 500, message: t('StudyDeck.Validation.NameLength'), trigger: 'blur' },
  ],
  description: [
    { max: 2000, message: t('StudyDeck.Validation.DescriptionLength'), trigger: 'blur' },
  ]
}));

const dialogVisible = ref(false);

const open = () => {
    form.name = '';
    form.description = '';
    dialogVisible.value = true;
}

const handleClose = () => {
    dialogVisible.value = false;
    emit('close');
}

const submit = async () => {
    if (!formRef.value) return;
    await formRef.value.validate(async (valid: boolean) => {
        if (valid) {
            await store.create({
                name: form.name,
                description: form.description
            });
            handleClose();
        }
    });
}

defineExpose({
    open
});
</script>

<template>
  <el-dialog
    v-model="dialogVisible"
    :title="t('StudyDeck.CreateTitle')"
    width="30%"
    :before-close="handleClose"
  >
    <el-form :model="form" :rules="rules" ref="formRef" label-width="120px">
      <el-form-item :label="t('StudyDeck.Name')" prop="name">
        <el-input v-model="form.name" />
      </el-form-item>
      <el-form-item :label="t('StudyDeck.Description')" prop="description">
        <el-input v-model="form.description" type="textarea" />
      </el-form-item>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="handleClose">{{ t('Common.Cancel') }}</el-button>
        <el-button type="primary" @click="submit" :loading="store.loading">
          {{ t('Common.Create') }}
        </el-button>
      </span>
    </template>
  </el-dialog>
</template>
