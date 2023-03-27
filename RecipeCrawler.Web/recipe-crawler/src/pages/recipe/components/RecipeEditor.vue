<script setup lang="ts">
import { NSpace, NSteps, NStep, NButton, NIcon } from "naive-ui";
import { Add } from "@vicons/carbon";
import { reactive, ref } from "vue";
import { Step } from "../../../models/shared/step.model";
import RecipeStep from "./RecipeStep.vue";
const steps = reactive(new Array<Step>());
const currentStep = ref(1);
function addStep(): void {
  steps.push(new Step());
}
</script>
<template>
  <n-space>
    <section style="margin-bottom: 1.25rem">
      <n-button size="small" circle primary @click="addStep()">
        <template #icon>
          <n-icon>
            <Add />
          </n-icon>
        </template>
      </n-button>
    </section>
    <br v-if="steps.length === 0" />
    <n-steps v-model:current="currentStep" vertical>
      <n-step v-for="(step, index) in steps" :key="step.id" :title="step.name">
        <template #icon>
          <section class="text-center">
            {{ index + 1 }}
          </section>
        </template>
      </n-step>
    </n-steps>
    <RecipeStep :step="steps[currentStep - 1]"></RecipeStep>
  </n-space>
</template>
