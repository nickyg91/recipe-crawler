<script setup lang="ts">
import {
  NForm,
  NInput,
  NSelect,
  NList,
  NListItem,
  FormInst,
  NInputNumber,
  NFormItem,
  NSpace,
  NButton,
  NIcon,
  NScrollbar,
  FormItemRule,
} from "naive-ui";
import { Add, Close } from "@vicons/carbon";
import { useRecipeStore } from "../../../recipe-store";
import { Ingredient } from "../../../models/shared/ingredient.model";
import { reactive, ref } from "vue";
import { computed } from "vue";
import { Measurement } from "../../../models/shared/measurement.enum";
import useMeasurementFunctions from "../../../shared/measurement-functions";
const store = useRecipeStore();
const ingredients = reactive(new Array<Ingredient>());
const currentlyEditedRecipe = store.getCurrentRecipe;
const formRef = ref<FormInst | null>(null);
const formModel = reactive(new Ingredient());
const measurementHelpers = useMeasurementFunctions();
const measurements = computed(() => {
  const values = [];
  const enumValues = Object.keys(Measurement).filter((x) => !isNaN(+x));
  for (const item in enumValues) {
    const measurement = Number(item) as Measurement;
    values.push({
      value: Number(item) as Measurement,
      label: measurementHelpers.translateEnum(measurement),
    });
  }
  return values;
});
const ingredientFormRules = {
  name: {
    required: true,
    trigger: ["blur", "change"],
    max: 256,
    message: "Name is required!",
  } as FormItemRule,
  amount: {
    trigger: ["blur", "change"],
    required: true,
    message: "Ingredient amount is required!",
    type: "number",
  } as FormItemRule,
  measurement: {
    trigger: ["blur", "change"],
    required: true,
    message: "Ingredient measurement type is required!",
    type: "enum",
    enum: Object.values(Measurement).filter((x) => !isNaN(+x)),
  } as FormItemRule,
};

if (currentlyEditedRecipe?.steps) {
  const stepIngredients = currentlyEditedRecipe.steps.flatMap((x) =>
    x.ingredients ? x.ingredients : []
  );
  if (stepIngredients) {
    ingredients.concat(stepIngredients);
  }
}

function addClicked() {
  formRef.value?.validate((errors) => {
    if (!errors) {
      ingredients.push({ ...formModel });
      Object.assign(formModel, new Ingredient());
    }
  });
}
</script>
<template>
  <n-space vertical>
    <n-scrollbar style="max-height: 350px" trigger="hover">
      <n-list v-if="ingredients.length > 0" bordered>
        <n-list-item v-for="ingredient in ingredients" :key="ingredient.id">
          <n-space align="center" justify="space-evenly">
            <span>{{ ingredient.name }}</span>
            <span>{{ ingredient.amount }}</span>
            <span>{{
              measurementHelpers.translateEnum(ingredient.measurement!)
            }}</span>
            <span>
              <n-button type="error" circle>
                <template #icon>
                  <n-icon>
                    <Close />
                  </n-icon>
                </template>
              </n-button>
            </span>
          </n-space>
        </n-list-item>
      </n-list>
    </n-scrollbar>
    <n-form
      ref="formRef"
      :model="formModel"
      :rules="ingredientFormRules"
      size="large"
    >
      <n-form-item label="Name" path="name">
        <n-input v-model:value="formModel.name" placeholder="Ingredient Name" />
      </n-form-item>
      <n-form-item
        :show-require-mark="true"
        label="Ingredient Amount"
        path="amount"
      >
        <n-input-number
          v-model:value="formModel.amount"
          :show-button="false"
          placeholder="Amount"
        />
      </n-form-item>
      <n-form-item
        :show-require-mark="true"
        label="Measurement"
        path="measurement"
      >
        <n-select
          v-model:value="formModel.measurement"
          :options="measurements"
        />
      </n-form-item>
      <n-form-item>
        <n-button
          style="width: 100%"
          primary
          size="large"
          @click="addClicked()"
        >
          <template #icon>
            <n-icon size="large">
              <Add />
            </n-icon>
          </template>

          Add
        </n-button>
      </n-form-item>
    </n-form>
  </n-space>
</template>
