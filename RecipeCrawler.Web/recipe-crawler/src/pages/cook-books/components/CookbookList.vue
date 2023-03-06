<script setup lang="ts">
import { NGrid, NGi, NCard, NIcon, NSpace } from "naive-ui";
import { Add } from "@vicons/carbon";
import { Cookbook } from "../../../models/shared/cookbook.model";
import CookbookCard from "./CookbookCard.vue";
import { type PropType } from "vue";
defineProps({
  cookbooks: {
    default: () => [],
    type: Array as PropType<Cookbook[]>,
    required: true,
  },
});
const emits = defineEmits(["onAddClicked"]);
function onAddClicked(): void {
  emits("onAddClicked");
}
</script>
<template>
  <n-grid
    cols="xs:1 s:2 m:3 l:4 xl:5 2xl:6"
    responsive="screen"
    :x-gap="15"
    :y-gap="15"
  >
    <n-gi>
      <n-card
        hoverable
        embedded
        :bordered="true"
        class="add-cookbook-card"
        size="large"
        @click="onAddClicked()"
      >
        <n-space style="height: 100%" justify="center" align="center">
          <n-icon size="128">
            <Add />
          </n-icon>
        </n-space>
      </n-card>
    </n-gi>
    <n-gi v-for="cookbook in cookbooks" :key="cookbook.id">
      <CookbookCard :cookbook="cookbook" />
    </n-gi>
  </n-grid>
</template>
<style scoped>
.add-cookbook-card {
  border-style: dashed;
  height: 300px;
  max-height: 300px;
}

.n-card:hover {
  cursor: pointer;
}
</style>
