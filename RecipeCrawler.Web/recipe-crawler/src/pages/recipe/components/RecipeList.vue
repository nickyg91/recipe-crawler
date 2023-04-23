<script setup lang="ts">
import { PropType, h } from "vue";
import { Recipe } from "../../../models/shared/recipe.model";
import { Add, Close, Edit } from "@vicons/carbon";
import {
  DataTableColumn,
  NDataTable,
  NSpace,
  NButton,
  NIcon,
  useDialog,
} from "naive-ui";
import { useRouter } from "vue-router";
import { useRecipeStore } from "../../../recipe-store";
import { ChefferWindow } from "../../../models/window.extension";
const dialog = useDialog();
const router = useRouter();
const store = useRecipeStore();
const props = defineProps({
  recipes: {
    default: () => [],
    type: Array as PropType<Recipe[]>,
    required: true,
  },
});
const columns = [
  {
    key: "name",
    title: "Name",
  },
  {
    align: "right",
    width: 65,
    render(rowData) {
      return h(
        NButton,
        {
          strong: true,
          tertiary: true,
          size: "small",
          renderIcon: () => h(Edit),
          onClick: () => editClicked(rowData.id as number),
        },
        { default: () => "Edit" }
      );
    },
  },
  {
    align: "right",
    width: 65,
    render(rowData) {
      return h(
        NButton,
        {
          strong: true,
          tertiary: false,
          size: "small",
          type: "error",
          renderIcon: () => h(Close),
          onClick: () => deleteClicked(rowData.id as number),
        },
        { default: () => "Delete" }
      );
    },
  },
] as DataTableColumn[];
function addRecipeClicked() {
  const recipeToCreate = new Recipe();
  const currentCookbook = store.getCurrentCookbook;
  if (currentCookbook) {
    recipeToCreate.cookbookId = currentCookbook.id;
    store.setCurrentRecipe(recipeToCreate);
    router.push({
      name: "recipeEditor",
      params: {
        recipeId: 0,
      },
    });
  } else {
    (window as ChefferWindow).$message?.error(
      "No active cookbook selected! Please select one from the list."
    );
  }
}
async function deleteClicked(id: number): Promise<void> {
  const currentCookbookId = Number(router.currentRoute.value.params.cookbookId);
  dialog.warning({
    content:
      "Are you sure you want to delete this? You wont be able to get it back.",
    title: "Delete?",
    positiveText: "Yes",
    negativeText: "No",
    onPositiveClick: async () => {
      await store.deleteRecipe(currentCookbookId, id);
    },
  });
}
async function editClicked(id: number): Promise<void> {
  const currentCookbookId = Number(router.currentRoute.value.params.cookbookId);
  await store.getFullRecipeDetails(id, currentCookbookId);
  router.push({
    name: "recipeEditor",
    params: {
      recipeId: id,
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
