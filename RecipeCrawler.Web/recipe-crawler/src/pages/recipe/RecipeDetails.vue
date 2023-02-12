<script setup lang="ts">
import { ParsedResponse } from "./models/parsed-response.model";
import { NCard, NSpace, NButton, NH1, NSpin, useMessage } from "naive-ui";
import { Close, WarningHex } from "@vicons/carbon";
import { useRecipeStore } from "../../recipe-store";
import { computed, ref } from "vue";
import { CrawlerApi, injectionKey } from "../../services/crawler-api.service";
import { inject } from "vue";
const store = useRecipeStore();
const crawlerApi: CrawlerApi | undefined = inject(injectionKey);
const props = defineProps({
  recipe: ParsedResponse,
  enableRemoval: Boolean,
});

const computedRecipe = computed(() => props.recipe);

const removeIngredient = (index: number) => {
  computedRecipe.value?.ingredients.splice(index, 1);
};
const removeStep = (index: number) => {
  computedRecipe.value?.steps.splice(index, 1);
};
const isReportButtonDisabled = ref(false);
const loading = ref(false);
const isGhostButton = computed(() => {
  return !store.getIsLightMode;
});

const messageService = useMessage();
const report = () => {
  if (props.recipe) {
    loading.value = true;
    crawlerApi
      ?.reportUrl(props.recipe)
      .then(() => {
        isReportButtonDisabled.value = true;
        messageService.success(
          "The URL has been reported. We will look into what went wrong.",
          {
            closable: true,
          }
        );
      })
      .catch(() => {
        messageService.error("Unable to report URL. Please try again.", {
          closable: true,
        });
      })
      .finally(() => {
        loading.value = false;
      });
  }
};
</script>
<template>
  <n-spin :show="loading">
    <n-space vertical justify="center">
      <n-card
        v-for="(item, index) in recipe?.ingredients"
        :key="item"
        style="margin-bottom: 5px"
      >
        <n-space justify="end">
          <n-button
            v-if="enableRemoval"
            type="primary"
            strong
            circle
            secondary
            @click="removeIngredient(index)"
          >
            <template #icon>
              <Close />
            </template>
          </n-button>
        </n-space>
        <div v-html="item"></div>
      </n-card>
      <n-card v-for="(item, index) in recipe?.steps" :key="item">
        <n-space justify="end">
          <n-button
            v-if="enableRemoval"
            type="primary"
            strong
            circle
            secondary
            @click="removeStep(index)"
          >
            <template #icon>
              <Close />
            </template>
          </n-button>
        </n-space>
        <div v-html="item"></div>
      </n-card>
    </n-space>
    <n-space
      v-if="recipe?.steps?.length == 0 && recipe?.ingredients?.length == 0"
      align="center"
      vertical
    >
      <n-h1>
        Unable to find recipe steps or ingredients. You can report this address
        in an effort to improve this tool.
      </n-h1>
      <n-button
        size="large"
        type="warning"
        :disabled="isReportButtonDisabled"
        :ghost="isGhostButton"
        @click="report()"
      >
        <template #icon>
          <WarningHex />
        </template>
        Report
      </n-button>
    </n-space>
  </n-spin>
</template>
