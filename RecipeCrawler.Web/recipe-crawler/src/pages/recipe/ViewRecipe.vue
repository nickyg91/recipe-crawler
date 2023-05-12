<script setup lang="ts">
import { useRecipeStore } from "../../recipe-store";
import { reactive } from "vue";
import {
  NScrollbar,
  NStep,
  NSteps,
  NSpace,
  NCard,
  NList,
  NListItem,
} from "naive-ui";
import useMeasurementFunctions from "../../shared/measurement-functions";
import { computed } from "vue";
const measurementComposables = useMeasurementFunctions();
const store = useRecipeStore();
const viewRecipeState = reactive({
  currentStep: 1,
  currentRecipe: store.getCurrentRecipe,
});

const currentStepComputed = computed(
  // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
  () => viewRecipeState.currentRecipe!.steps![viewRecipeState.currentStep - 1]
);
const stepIngredients = computed(() =>
  // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
  currentStepComputed.value.ingredients.map((ingredient) => {
    const measurement = measurementComposables.translateEnum(
      ingredient.measurement
    );
    return {
      display: `${ingredient.name} | ${ingredient.amount} | ${measurement}`,
      key: ingredient.id,
    };
  })
);

function stepClicked(step: number): void {
  viewRecipeState.currentStep = step;
}
</script>
<template>
  <div style="gap: 15px; display: flex">
    <n-space vertical>
      <n-scrollbar style="max-height: 350px" trigger="hover">
        <n-steps
          v-model:current="viewRecipeState.currentStep"
          class="steps-padding"
          vertical
          @update-current="stepClicked($event)"
        >
          <n-step
            v-for="step in viewRecipeState.currentRecipe!.steps"
            :key="step.id"
          >
          </n-step>
        </n-steps>
      </n-scrollbar>
    </n-space>
    <div style="width: 75%">
      <n-card>
        <section>
          <h2>{{ currentStepComputed.name }}</h2>
          <section>
            {{ currentStepComputed.description }}
          </section>
        </section>
        <section>
          <h2>
            Ingredients for this
            {{ currentStepComputed.name }}
          </h2>
          <n-list>
            <n-list-item
              v-for="ingredient in stepIngredients"
              :key="ingredient.key"
            >
              {{ ingredient.display }}
            </n-list-item>
          </n-list>
        </section>
      </n-card>
    </div>
  </div>
</template>
<style scoped>
.steps-padding {
  padding: 0.25em;
}
.steps-max-height {
  max-height: 300px;
}
</style>
