<script setup lang="ts">
import { NCard, NInput, NForm, FormInst } from "naive-ui";
import { computed, PropType, ref } from "vue";
import { Step } from "../../../models/shared/step.model";
const emits = defineEmits(["stepUpdated"]);
const props = defineProps({
  step: {
    default: () => null,
    required: true,
    type: Step as PropType<Step>,
  },
});
const formRef = ref<FormInst | null>(null);
const computedModel = computed({
  get() {
    return props.step;
  },
  set(step: Step) {
    emits("stepUpdated", step);
  },
});
</script>
<template>
  <n-form ref="formRef">
    <n-card size="huge">
      <template #header>
        <n-input v-model:value="props.step.name"> </n-input>
      </template>
    </n-card>
  </n-form>
</template>
