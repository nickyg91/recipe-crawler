<script setup lang="ts">
import {
  NCard,
  NInput,
  NForm,
  FormInst,
  NButton,
  NFormItem,
  NSpace,
} from "naive-ui";
import { Close } from "@vicons/carbon";
import { computed, PropType, ref } from "vue";
import { Step } from "../../../models/shared/step.model";
const emits = defineEmits(["update:stepUpdated", "removeClicked"]);
const props = defineProps({
  step: {
    default: () => null,
    required: true,
    type: Object as PropType<Step>,
  },
  index: {
    required: true,
    type: Number,
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
    return props.step;
  },
  set(value) {
    emits("update:stepUpdated", value);
  },
});

function removeClicked(): void {
  emits("removeClicked", props.index);
}
</script>
<template>
  <n-card>
    <n-space justify="end">
      <n-button circle primary @click="removeClicked()">
        <template #icon>
          <close></close>
        </template>
      </n-button>
    </n-space>
    <n-form ref="formRef" :model="computedModel" :rules="formRules">
      <n-form-item path="computedModel.name" label="Name">
        <n-input v-model:value="computedModel.name" placeholder="Name">
        </n-input>
      </n-form-item>
      <n-form-item path="computedModel.description" label="Description">
        <n-input
          v-model:value="computedModel.description"
          type="textarea"
          placeholder="Description"
        />
      </n-form-item>
    </n-form>
  </n-card>
</template>
