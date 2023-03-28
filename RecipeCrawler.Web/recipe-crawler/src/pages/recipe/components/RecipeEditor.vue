<script setup lang="ts">
import { NSpace, NSteps, NStep, NButton, NIcon } from "naive-ui";
import { Add } from "@vicons/carbon";
import { computed, reactive, ref } from "vue";
import { Step } from "../../../models/shared/step.model";
import RecipeStep from "./RecipeStep.vue";
//import { useRecipeStore } from "../../../recipe-store";
const steps = reactive(new Array<Step>());
const currentStep = ref(1);
function addStep(): void {
  steps.push(new Step());
}
function onRemoveClicked(index: number): void {
  steps.splice(index, 1);
  if (computedStepIndex.value === index + 1) {
    currentStep.value--;
  }
}
const computedStepIndex = computed(() => currentStep.value - 1);
</script>
<template>
  <!-- <section style="display: flex; margin-top: 0.75em">
    <div>
      <n-space vertical>
        <n-button
          style="margin-bottom: 1.75em"
          size="small"
          circle
          primary
          @click="addStep()"
        >
          <template #icon>
            <n-icon>
              <Add />
            </n-icon>
          </template>
        </n-button>
        <n-steps v-model:current="currentStep" vertical>
          <n-step v-for="step in steps" :key="step.id" :title="step.name">
          </n-step>
        </n-steps>
      </n-space>
    </div>
    <div style="flex-grow: 3">
      <RecipeStep
        v-if="steps.length > 0"
        :step="steps[computedStepIndex]"
        :index="computedStepIndex"
        @remove-clicked="onRemoveClicked"
      ></RecipeStep>
    </div>
  </section> -->
  <n-space vertical :size="1">
    <n-button
      style="margin-bottom: 1.75em"
      size="small"
      circle
      primary
      @click="addStep()"
    >
      <template #icon>
        <n-icon>
          <Add />
        </n-icon>
      </template>
    </n-button>
    <n-steps v-model:current="currentStep" vertical>
      <n-step v-for="step in steps" :key="step.id" :title="step.name"> </n-step>
    </n-steps>
  </n-space>
  <n-space :size="12">
    <RecipeStep
      v-if="steps.length > 0"
      :step="steps[computedStepIndex]"
      :index="computedStepIndex"
      @remove-clicked="onRemoveClicked"
    ></RecipeStep>
  </n-space>
</template>
