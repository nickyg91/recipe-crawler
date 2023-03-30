<script setup lang="ts">
import {
  NSpace,
  NSteps,
  NStep,
  NButton,
  NIcon,
  NInput,
  NFormItem,
  FormItemRule,
} from "naive-ui";
import { Add, Chemistry, Save } from "@vicons/carbon";
import { reactive, ref } from "vue";
import { Step } from "../../../models/shared/step.model";
import { Recipe } from "../../../models/shared/recipe.model";
import RecipeStep from "./RecipeStep.vue";
import { useRecipeStore } from "../../../recipe-store";
const store = useRecipeStore();
const currentlyEditedRecipe =
  store.getCurrentRecipe === null
    ? reactive(new Recipe())
    : store.getCurrentRecipe;
const currentStep = ref(1);

const recipeNameValidationRule = {
  trigger: ["blur"],
  validator: () => {
    if (
      !currentlyEditedRecipe.name ||
      currentlyEditedRecipe.name?.length === 0
    ) {
      return Error("Recipe name is required!");
    }
  },
} as FormItemRule;

let currentStepObject: Step;
if (!currentlyEditedRecipe.steps || currentlyEditedRecipe.steps?.length === 0) {
  currentStepObject = reactive(
    new Step(0, "", "", currentlyEditedRecipe?.id ?? 0, null)
  );
  currentlyEditedRecipe.steps = [currentStepObject];
} else if (
  currentlyEditedRecipe.steps &&
  currentlyEditedRecipe.steps.length > 0
) {
  currentStepObject = currentlyEditedRecipe.steps[0];
}

function addStep(): void {
  const stepToAdd = reactive(
    new Step(
      currentStepObject.id - 1,
      "",
      "",
      currentlyEditedRecipe?.id ?? 0,
      null
    )
  );
  currentlyEditedRecipe.steps?.push(stepToAdd);
  currentStepObject = stepToAdd;
  currentStep.value++;
}

function onRemoveClicked(index: number): void {
  currentlyEditedRecipe.steps?.splice(index, 1);
  if (currentStep.value === index + 1) {
    currentStep.value--;
  }
}

function stepClicked(currentStep: number): void {
  // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
  currentStepObject = currentlyEditedRecipe.steps![currentStep - 1];
}
</script>
<template>
  <section style="margin-top: 1em">
    <div style="margin-bottom: 0.75em">
      <n-form-item
        label="Recipe Name"
        size="large"
        :required="true"
        :rule="recipeNameValidationRule"
      >
        <n-input
          v-model:value="currentlyEditedRecipe.name"
          placeholder="Name of Recipe"
        />
      </n-form-item>
    </div>
    <div style="display: flex">
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
          <n-step v-for="step in currentlyEditedRecipe.steps" :key="step.id">
          </n-step>
        </n-steps>
      </n-space>
      <div style="flex-grow: 3">
        <RecipeStep
          v-if="currentlyEditedRecipe.steps!.length > 0"
          :key="currentStepObject.id"
          v-model:step="currentStepObject"
          v-model:index="currentStep"
          @remove-clicked="onRemoveClicked"
        ></RecipeStep>
      </div>
    </div>
    <n-space vertical align="end">
      <n-button type="info" circle ghost size="large">
        <n-icon>
          <Chemistry />
        </n-icon>
      </n-button>
      <n-button tupe="primary" circle ghost size="large">
        <n-icon>
          <Save />
        </n-icon>
      </n-button>
    </n-space>
  </section>
</template>
