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
  NPopover,
  NScrollbar,
} from "naive-ui";
import { Add, Chemistry, Save } from "@vicons/carbon";
import { reactive, ref } from "vue";
import { Step } from "../../../models/shared/step.model";
import { Recipe } from "../../../models/shared/recipe.model";
import RecipeStep from "./RecipeStep.vue";
import { useRecipeStore } from "../../../recipe-store";
import IngredientForm from "./IngredientForm.vue";
import { useRouter } from "vue-router";
const router = useRouter();
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
    new Step(0, "", "", currentlyEditedRecipe?.id ?? 0, [], 0)
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
      0,
      "",
      "",
      currentlyEditedRecipe?.id ?? 0,
      [],
      currentlyEditedRecipe.steps ? currentlyEditedRecipe.steps.length - 1 : 0
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

async function saveRecipe() {
  await store.saveRecipe(currentlyEditedRecipe);
  router.back();
}
</script>
<template>
  <section style="margin-top: 1em">
    <div style="margin-bottom: 0.75em; width: 77%">
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
    <div style="gap: 15px; display: flex">
      <n-space vertical>
        <n-button
          style="margin-bottom: 1.75em; margin-left: 0.25em"
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
        <n-scrollbar style="max-height: 350px" trigger="hover">
          <n-steps
            v-model:current="currentStep"
            class="steps-padding"
            vertical
            @update-current="stepClicked($event)"
          >
            <n-step v-for="step in currentlyEditedRecipe.steps" :key="step.id">
            </n-step>
          </n-steps>
        </n-scrollbar>
      </n-space>
      <div style="width: 75%">
        <RecipeStep
          v-if="currentlyEditedRecipe.steps!.length > 0"
          :key="currentStep"
          v-model:step="currentStepObject"
          v-model:index="currentStep"
          :ingredients="currentlyEditedRecipe.ingredients ?? []"
          @remove-clicked="onRemoveClicked"
        ></RecipeStep>
      </div>
    </div>
    <n-space class="floating-buttons" vertical align="end">
      <n-popover
        :key="currentlyEditedRecipe.id"
        placement="left-start"
        trigger="click"
      >
        <template #trigger>
          <n-button type="info" circle ghost size="large">
            <n-icon>
              <Chemistry />
            </n-icon>
          </n-button>
        </template>
        <div>
          <IngredientForm />
        </div>
      </n-popover>

      <n-button type="primary" circle ghost size="large" @click="saveRecipe()">
        <n-icon>
          <Save />
        </n-icon>
      </n-button>
    </n-space>
  </section>
</template>

<style scoped>
.floating-buttons {
  position: absolute;
  left: 96%;
  top: 15%;
}

.steps-padding {
  padding: 0.25em;
}
.steps-max-height {
  max-height: 300px;
}
</style>
