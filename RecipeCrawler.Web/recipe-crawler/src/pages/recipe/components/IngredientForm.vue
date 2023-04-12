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
  useDialog,
} from "naive-ui";
import { Add, Close, ThumbsUp } from "@vicons/carbon";
import { useRecipeStore } from "../../../recipe-store";
import { Ingredient } from "../../../models/shared/ingredient.model";
import { h, reactive, ref } from "vue";
import { computed } from "vue";
import { Measurement } from "../../../models/shared/measurement.enum";
import useMeasurementFunctions from "../../../shared/measurement-functions";
const store = useRecipeStore();
const measurementHelpers = useMeasurementFunctions();
const dialog = useDialog();
const emits = defineEmits(["ingredientDeleted"]);
const currentlyEditedRecipe = store.getCurrentRecipe;
const formRef = ref<FormInst | null>(null);
const ingredientFormState = reactive({
  formModel: new Ingredient(),
});

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
    currentlyEditedRecipe.ingredients?.push(...stepIngredients);
  }
}

function addClicked() {
  formRef.value?.validate((errors) => {
    if (!errors) {
      store.$patch((state) => {
        state.currentRecipe?.ingredients?.push({
          ...ingredientFormState.formModel,
        });
      });
      Object.assign(ingredientFormState.formModel, {
        id: 0,
        name: "",
        amount: 0,
        measurement: Measurement.Cup,
      } as Ingredient);
    }
  });
}

function deleteIngredient(index: number): void {
  dialog.warning({
    title: "Confirm",
    content:
      "Are you sure you want to delete this ingredient? It will remove it from all steps that use it.",
    positiveText: "Yes",
    positiveButtonProps: {
      renderIcon: () => h(ThumbsUp),
      size: "large",
    },
    negativeButtonProps: {
      renderIcon: () => h(Close),
      size: "large",
    },
    negativeText: "No",
    onPositiveClick: () => {
      const ingredientToRemove = currentlyEditedRecipe?.ingredients?.splice(
        index,
        1
      );
      if (ingredientToRemove) {
        emits("ingredientDeleted", ingredientToRemove[0]);
      }
    },
  });
}
</script>
<template>
  <n-space vertical>
    <n-scrollbar style="max-height: 350px" trigger="hover">
      <n-list
        v-if="currentlyEditedRecipe && currentlyEditedRecipe!.ingredients!.length > 0"
        bordered
      >
        <n-list-item
          v-for="(ingredient, index) in currentlyEditedRecipe!.ingredients!"
          :key="index"
        >
          <n-space align="center" justify="space-evenly">
            <span>{{ ingredient.name }}</span>
            <span>{{ ingredient.amount }}</span>
            <span>{{
              measurementHelpers.translateEnum(ingredient.measurement!)
            }}</span>
            <span>
              <n-button type="error" circle @click="deleteIngredient(index)">
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
      :key="currentlyEditedRecipe!.ingredients?.find(x => x == ingredientFormState.formModel)"
      :model="ingredientFormState.formModel"
      :rules="ingredientFormRules"
      size="large"
    >
      <n-form-item label="Name" path="name">
        <n-input
          v-model:value="ingredientFormState.formModel.name"
          placeholder="Ingredient Name"
        />
      </n-form-item>
      <n-form-item
        :show-require-mark="true"
        label="Ingredient Amount"
        path="amount"
      >
        <n-input-number
          v-model:value="ingredientFormState.formModel.amount"
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
          v-model:value="ingredientFormState.formModel.measurement"
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
