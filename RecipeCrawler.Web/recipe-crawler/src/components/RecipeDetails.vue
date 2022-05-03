<script setup lang="ts">
import { ParsedResponse } from "../models/parsed-response.model";
import { NCard, NSpace, NButton, NH1 } from "naive-ui";
import { Close, WarningHex } from "@vicons/carbon";
import { useRecipeStore } from "../recipe-store";
import { computed } from "@vue/reactivity";
const store = useRecipeStore();

const props = defineProps({
  recipe: ParsedResponse,
  enableRemoval: Boolean,
});
const removeIngredient = (index: number) => {
  props.recipe?.ingredients.splice(index, 1);
};
const removeStep = (index: number) => {
  props.recipe?.steps.splice(index, 1);
};

const isGhostButton = computed(() => {
  return !store.getTheme;
});

const report = () => {
  console.log(props.recipe?.url);
};
</script>
<template>
  <n-space vertical justify="center">
    <n-card
      style="margin-bottom: 5px"
      v-for="(item, index) in recipe?.ingredients"
    >
      <n-space justify="end">
        <n-button
          v-if="enableRemoval"
          @click="removeIngredient(index)"
          type="primary"
          strong
          circle
          secondary
        >
          <template #icon>
            <Close />
          </template>
        </n-button>
      </n-space>
      <div v-html="item"></div>
    </n-card>
    <n-card v-for="(item, index) in recipe?.steps">
      <n-space justify="end">
        <n-button
          v-if="enableRemoval"
          @click="removeStep(index)"
          type="primary"
          strong
          circle
          secondary
        >
          <template #icon>
            <Close />
          </template>
        </n-button>
      </n-space>
      <div v-html="item"></div>
    </n-card>
  </n-space>
  <n-space
    align="center"
    vertical
    v-if="recipe?.steps?.length == 0 && recipe?.ingredients?.length == 0"
  >
    <n-h1>
      Unable to find recipe steps or ingredients. You can report this address in
      an effort to improve this tool.
    </n-h1>
    <n-button
      size="large"
      type="warning"
      :ghost="isGhostButton"
      @click="report()"
    >
      <template #icon>
        <WarningHex />
      </template>
      Report
    </n-button>
  </n-space>
</template>
