<script lang="ts" setup>
import WordFormApi from '@/features/DictionaryItem/api/WordFormApi';
import type { WordFormType } from '@/features/DictionaryItem/types/models/WordFormType'
import { computed, reactive, watch } from 'vue'

const props = defineProps<{
  wordForm?: WordFormType | null
}>()

const form = reactive<WordFormType>({
  id: '',
  infinitive: '',
  pastSimple: '',
  pastParticiple: ''
})

watch(
  () => props.wordForm,
  (val) => {
    if (val) {
      form.id = val.id
      form.infinitive = val.infinitive
      form.pastSimple = val.pastSimple
      form.pastParticiple = val.pastParticiple
    } else {
      form.id = '';
      form.infinitive = '';
      form.pastSimple = '';
      form.pastParticiple = '';
    }
  },
  { immediate: true }
)

const tableData = computed(() => [form])

async function saveWordForm(dictionaryItemId: string) {
  try {
    if (form.id != '') {
      await WordFormApi.update(form)
    } else {
      await WordFormApi.create({...form, dictionaryItemId: dictionaryItemId})
    }
  } catch (err) {
    console.error(err);
  }
}

defineExpose({ saveWordForm })
</script>

<template>
  <el-table :data="tableData" border style="width: 100%">
    
    <el-table-column label="Infinitive">
      <template #default="scope">
        <el-input v-model="scope.row.infinitive"/>
      </template>
    </el-table-column>

    <el-table-column label="Past Simple">
      <template #default="scope">
        <el-input v-model="scope.row.pastSimple"/>
      </template>
    </el-table-column>

    <el-table-column label="Past Participle">
      <template #default="scope">
        <el-input v-model="scope.row.pastParticiple"/>
      </template>
    </el-table-column>
  </el-table>
</template>