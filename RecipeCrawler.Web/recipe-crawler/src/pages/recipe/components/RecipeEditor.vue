<script setup lang="ts">
import { NSpace, NSteps, NStep, NButton, NIcon } from "naive-ui";
import { Add } from "@vicons/carbon";
import { reactive, ref } from "vue";
import { Step } from "../../../models/shared/step.model";
import RecipeStep from "./RecipeStep.vue";
import { useRouter } from "vue-router";
const router = useRouter();
const recipeId = router.currentRoute.value.params[
  "recipeId"
] as unknown as number;
const steps = reactive(new Array<Step>());
const currentStep = ref(1);
let currentStepObject = reactive(new Step(0, "", "", recipeId, null));
steps.push(currentStepObject);
function addStep(): void {
  const stepToAdd = reactive(
    new Step(currentStepObject.id - 1, "", "", recipeId, null)
  );
  stepToAdd.id = steps.push(stepToAdd);
  currentStepObject = stepToAdd;
  currentStep.value++;
}

function onRemoveClicked(index: number): void {
  steps.splice(index, 1);
  if (currentStep.value === index + 1) {
    currentStep.value--;
  }
}

function stepClicked(currentStep: number): void {
  currentStepObject = steps[currentStep - 1];
}
</script>
<template>
  <section style="display: flex; margin-top: 0.75em">
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
        <n-steps
          v-model:current="currentStep"
          vertical
          @update-current="stepClicked($event)"
        >
          <n-step v-for="step in steps" :key="step.id"> </n-step>
        </n-steps>
      </n-space>
    </div>
    <div style="flex-grow: 3">
      <RecipeStep
        v-if="steps.length > 0"
        :key="currentStepObject.id"
        v-model:step="currentStepObject"
        v-model:index="currentStep"
        @remove-clicked="onRemoveClicked"
      ></RecipeStep>
    </div>
  </section>
</template>
