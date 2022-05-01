<script setup lang="ts">
import { ParsedResponse } from "../models/parsed-response.model";
import { NCard, NSpace, NButton } from "naive-ui";
import { Close } from "@vicons/carbon";
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
</template>
