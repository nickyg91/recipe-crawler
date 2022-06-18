<script setup lang="ts">
import { ParsedResponse } from "../models/parsed-response.model";
import { NCard, NSpace, NButton, NH1, useNotification, NSpin } from "naive-ui";
import { Close, WarningHex } from "@vicons/carbon";
import { useRecipeStore } from "../recipe-store";
import { computed, ref } from "@vue/reactivity";
import { CrawlerApi, injectionKey } from "../services/crawler-api.service";
import { inject } from "vue";
const store = useRecipeStore();
const crawlerApi: CrawlerApi | undefined = inject(injectionKey);
const props = defineProps({
  recipe: ParsedResponse,
  enableRemoval: Boolean,
});
const removeIngredient = (index: number) => {
  props.recipe?.ingredients.splice(index, 1);
};
const removeStep = (index: number) => {
  props.recipe?.steps.splice(index, 1);
};
const isReportButtonDisabled = ref(false);
const loading = ref(false);
const isGhostButton = computed(() => {
  return !store.getIsLightMode;
});

const notificationService = useNotification();
const report = () => {
  if (props.recipe) {
    loading.value = true;
    crawlerApi
      ?.reportUrl(props.recipe)
      .then(() => {
        isReportButtonDisabled.value = true;
        notificationService.success({
          title: "Success!",
          content:
            "The URL has been reported. We will look into what went wrong..",
        });
      })
      .catch(() => {
        notificationService.error({
          title: "Error reporting Url",
          content: "Unable to report URL. Please try again.",
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
        style="margin-bottom: 5px"
        v-for="(item, index) in recipe?.ingredients"
      >
        <n-space justify="end">
          <n-button
            v-if="enableRemoval"
            @click="removeIngredient(index)"
            type="primary"
            strong
            circle
            secondary
          >
            <template #icon>
              <Close />
            </template>
          </n-button>
        </n-space>
        <div v-html="item"></div>
      </n-card>
      <n-card v-for="(item, index) in recipe?.steps">
        <n-space justify="end">
          <n-button
            v-if="enableRemoval"
            @click="removeStep(index)"
            type="primary"
            strong
            circle
            secondary
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
      align="center"
      vertical
      v-if="recipe?.steps?.length == 0 && recipe?.ingredients?.length == 0"
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
