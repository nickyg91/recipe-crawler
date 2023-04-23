<script setup lang="ts">
import {
  NCard,
  NInput,
  NForm,
  FormInst,
  NButton,
  NFormItem,
  NSpace,
  NSelect,
} from "naive-ui";
import { Close } from "@vicons/carbon";
import { computed, PropType, ref } from "vue";
import { Step } from "../../../models/shared/step.model";
import { Ingredient } from "../../../models/shared/ingredient.model";
import { SelectMixedOption } from "naive-ui/es/select/src/interface";
import useMeasurementFunctions from "../../../shared/measurement-functions";
const measurementHelpers = useMeasurementFunctions();
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
  ingredients: {
    default: () => [],
    required: true,
    type: Array as PropType<Array<Ingredient>>,
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

const computedMultiselectIngredientOptions = computed(() =>
  props.ingredients.map((x) => {
    return {
      key: !x.id ? x.keyId : x.id,
      value: !x.id ? x.keyId : x.id,
      label: `${x.name} - ${x.amount} - ${measurementHelpers.translateEnum(
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        x.measurement!
      )}`,
    } as SelectMixedOption;
  })
);

const computedModel = computed({
  get() {
    return props.step;
  },
  set(value) {
    emits("update:stepUpdated", value);
  },
});

const ingredientValues = ref(
  computedModel.value.ingredients.map((x) => (!x.id ? x.keyId : x.id))
);

function removeClicked(): void {
  emits("removeClicked", props.index);
}

function onValueUpdated(values: number[]) {
  const ingredients = props.ingredients.filter(
    (x) => values.indexOf(!x.id ? x.keyId : x.id) > -1
  );
  ingredientValues.value = values;
  computedModel.value.ingredients = ingredients;
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
      <n-form-item
        v-if="computedMultiselectIngredientOptions.length > 0"
        path="computedModel.ingredients"
        label="Ingredients"
      >
        <n-select
          :value="ingredientValues"
          multiple
          :options="computedMultiselectIngredientOptions"
          @update:value="onValueUpdated($event)"
        >
        </n-select>
      </n-form-item>
    </n-form>
  </n-card>
</template>
