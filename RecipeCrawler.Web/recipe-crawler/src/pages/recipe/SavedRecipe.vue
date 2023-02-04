<script lang="ts" setup>
import { useRouter } from "vue-router";
import { ParsedResponse } from "./models/parsed-response.model";
import {
  NSpin,
  NSpace,
  NH1,
  NButton,
  NModal,
  NInput,
  NInputGroup,
  useNotification,
} from "naive-ui";
import RecipeDetails from "./RecipeDetails.vue";
import { computed, inject, reactive, ref } from "vue";
import { useRecipeStore } from "../../recipe-store";
import { Share, Copy } from "@vicons/carbon";
import { CrawlerApi, injectionKey } from "../../services/crawler-api.service";
const recipe = reactive(new ParsedResponse());
const store = useRecipeStore();
const loading = ref(true);
const router = useRouter();
const url: string = router.currentRoute.value.params.url as string;
const crawlerService: CrawlerApi | undefined = inject(injectionKey);
const notificationService = useNotification();
crawlerService?.getRecipe(url).then(
  (result) => {
    loading.value = false;
    Object.assign(recipe, result.data);
  },
  () => {
    notificationService.error({
      content: "Error loading recipe!",
      title: "Error",
    });
  }
);

const link = window.location.href;
const showShareModal = ref(false);

const copyUrl = () => {
  navigator.clipboard.writeText(link);
};

const isGhostButton = computed(() => {
  return !store.getIsLightMode;
});
</script>
<template>
  <n-spin :show="loading">
    <n-space vertical justify="center">
      <n-space justify="end">
        <n-button @click="showShareModal = true" :ghost="isGhostButton">
          <template #icon><Share /></template>
        </n-button>
      </n-space>
      <n-h1>
        {{ recipe.title }}
      </n-h1>
      <recipe-details :enable-removal="false" :recipe="recipe"></recipe-details>
    </n-space>
    <n-modal v-model:show="showShareModal" preset="dialog" title="Share">
      <div>
        <n-input-group>
          <n-input v-model:value="link" />
          <n-button @click="copyUrl" :ghost="isGhostButton">
            <template #icon>
              <Copy />
            </template>
          </n-button>
        </n-input-group>
      </div>
    </n-modal>
  </n-spin>
</template>
