<script setup lang="ts">
import { computed } from 'vue';
import getWeightClassFunc from '../../helpers/getWeightClass';

const props = defineProps<{
  item: any,
  minWeight: number,
  maxWeight: number
}>()

const item = computed(() => props.item);

</script>
<template>
  <el-card class="card-light" style="width: 100%;">
    <div class="item__body">
      <div :class="`info ${getWeightClassFunc(item.weight, minWeight, maxWeight)}`">
          <span class="info-weight__value">{{ item.weight }}</span>
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
    </div>
  </el-card>
</template>
<style>
.card-light .el-card__body {
  padding: 5px 10px;
}
.card-light  {
  margin: 2px 0px;
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
</style>