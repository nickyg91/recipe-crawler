<script setup lang="ts">
import { PropType } from "vue";
import { Recipe } from "../../../models/shared/recipe.model";
import { Add } from "@vicons/carbon";
import { DataTableColumn, NDataTable, NSpace, NButton, NIcon } from "naive-ui";
import { useRouter } from "vue-router";
const router = useRouter();
const props = defineProps({
  recipes: {
    default: () => [],
    type: Array as PropType<Recipe[]>,
    required: true,
  },
});
const columns = [
  {
    key: "id",
    title: "Name",
  },
  {},
  {},
] as DataTableColumn[];
function addRecipeClicked() {
  router.push({
    name: "recipeEditor",
    params: {
      recipeId: 0,
    },
  });
}
</script>
<template>
  <n-space style="margin-bottom: 15px" justify="start">
    <n-button size="large" @click="addRecipeClicked()">
      <template #icon>
        <n-icon>
          <Add />
        </n-icon>
      </template>
      Add Recipe
    </n-button>
  </n-space>
  <n-data-table :columns="columns" :data="props.recipes"> </n-data-table>
</template>
