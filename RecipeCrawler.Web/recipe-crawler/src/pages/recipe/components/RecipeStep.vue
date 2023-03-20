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
const formRules = {
  name: {
    required: true,
    message: "Name is required.",
  },
  description: {
    required: true,
    message: "Description is required.",
  },
};
const computedModel = computed({
  get() {
    return {
      ...props.step,
    };
  },
  set() {
    emits("stepUpdated", computedModel.value);
  },
});
</script>
<template>
  <section>
    <n-form ref="formRef" :rules="formRules">
      <n-card size="huge">
        <template #header>
          <n-input v-model:value="computedModel.name" placeholder="Name">
          </n-input>
        </template>
        <template #content>
          <n-input
            v-model:value="computedModel.description"
            type="textarea"
            placeholder="Description"
          >
          </n-input>
        </template>
      </n-card>
    </n-form>
  </section>
</template>
