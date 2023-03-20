<script setup lang="ts">
import { NGi, NGrid, NSpace, NSteps, NStep } from "naive-ui";
import { ref } from "vue";
import { Step } from "../../../models/shared/step.model";
const steps = ref(new Array<Step>());
const currentStep = ref(0);
</script>
<template>
  <n-grid :x-gap="12" :cols="2">
    <n-gi>
      <n-space vertical>
        <n-steps :current="currentStep" vertical>
          <n-step v-for="step in steps" :key="step.id" :title="step.name">
          </n-step>
        </n-steps>
      </n-space>
    </n-gi>
    <n-gi> </n-gi>
  </n-grid>
</template>
