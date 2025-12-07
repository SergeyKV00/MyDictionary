<script lang="ts" setup>
import type { DictionaryItemExampleType } from '@/features/DictionaryItem/types/models/DictionaryItemExampleType';
import { Expand } from '@element-plus/icons-vue' 

const props = defineProps<{
 examples: DictionaryItemExampleType[] 
}>()
</script>
<template>
  <div class="examples">
    <div v-for="item in props.examples" class="example-item">
      <div class="example-item__content">
        <div>{{ item.example }}</div>
        <div>{{ item.translation }}</div>
      </div>
      <div class="example-item__action">
        <el-dropdown placement="bottom">
          <el-button type="primary" circle>
            <el-icon style="color: white"><Expand /></el-icon>
          </el-button>
          <template #dropdown>
            <el-dropdown-menu style="padding: 10px;">
            </el-dropdown-menu>
          </template>
        </el-dropdown>
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
</style>