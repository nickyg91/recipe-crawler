<script setup lang="ts">
import { NSpace, NH1 } from "naive-ui";
import RecipeList from "./components/RecipeList.vue";
import { useRecipeStore } from "../../recipe-store";
import { computed } from "vue";
const store = useRecipeStore();
const currentCookbook = store.getCurrentCookbook;
const recipes = computed(() => store.getRecipesForCookbook);
</script>
<template>
  <n-space justify="start">
    <n-h1>
      {{ currentCookbook?.name }}
    </n-h1>
    <!-- <n-breadcrumb>
      <n-breadcrumb-item href="/cook-books"> Cookbooks </n-breadcrumb-item>
      <n-breadcrumb-item>
        {{ currentCookbook?.name }}
      </n-breadcrumb-item>
    </n-breadcrumb> -->
  </n-space>
  <RecipeList :recipes="recipes ?? []" />
</template>
