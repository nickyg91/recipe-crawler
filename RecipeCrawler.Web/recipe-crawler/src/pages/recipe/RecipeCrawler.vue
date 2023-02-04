<script setup lang="ts">
import { inject, reactive, ref } from "vue";
import { AxiosResponse } from "axios";
import {
  NButton,
  NGrid,
  NGi,
  NForm,
  NFormItem,
  NInput,
  NSpace,
  NIcon,
  FormRules,
  FormInst,
  useNotification,
  NSpin,
  NDivider,
} from "naive-ui";
import { Checkmark, Save } from "@vicons/carbon";
import { computed } from "vue";
import { useRouter } from "vue-router";
import RecipeDetails from "./RecipeDetails.vue";
import { useRecipeStore } from "../../recipe-store";
import { injectionKey, CrawlerApi } from "../../services/crawler-api.service";
import { ParsedResponse } from "./models/parsed-response.model";

const crawlerApi: CrawlerApi | undefined = inject(injectionKey);
const store = useRecipeStore();
const notificationService = useNotification();
const router = useRouter();
const response = reactive(new ParsedResponse());
const loading = ref(false);
const isGhostButton = computed(() => {
  return !store.isLightMode;
});
const formRef = ref<FormInst | null>(null);
const model = reactive({
  url: "",
  title: "",
});

const rules: FormRules = {
  url: {
    required: true,
    pattern: /(https?:\/\/)?([\da-z.-]+)\.([a-z]{2,6})([\w.-]*)*\/?/,
    message: "A valid URL must be supplied.",
    trigger: ["input", "blur"],
  },
  title: {
    required: true,
    message: "A title is required.",
    trigger: ["input", "blur"],
  },
};

const submitEnabled = computed(() => {
  return model.url.length > 0 && model.title.length > 0;
});

const saveEnabled = computed(() => {
  return response.ingredients?.length > 0 || response.steps?.length > 0;
});

const submit = () => {
  formRef.value?.validate((errors) => {
    if (!errors) {
      loading.value = true;
      crawlerApi
        ?.crawlUrl(model.url)
        .then((result) => {
          response.url = model.url;
          response.title = model.title;
          loading.value = false;
          response.ingredients = result.data.ingredients;
          response.steps = result.data.steps;
          notificationService.success({
            title: "Success!",
            content: "The recipe has been scraped.",
            duration: 3000,
          });
        })
        .catch(() => {
          loading.value = false;
          notificationService.error({
            content: "An error occurred attempting to scrape the recipe.",
            title: "Error",
            duration: 3000,
          });
        });
    } else {
      notificationService.error({
        content: "Invalid Form. Please correct the errors and resubmit.",
        title: "Validation Error",
      });
    }
  });
};

const save = () => {
  loading.value = true;
  response.url = model.url;
  response.title = model.title;
  crawlerApi?.saveRecipe(response).then(
    (result: AxiosResponse<string>) => {
      router.push({ name: "savedRecipe", params: { url: result.data } });
      loading.value = false;
    },
    () => {
      loading.value = false;
      notificationService.error({
        content: "An error occurred attempting to save the recipe.",
        title: "Error",
        duration: 3000,
      });
    }
  );
};
</script>
<template>
  <n-spin :show="loading">
    <n-space vertical style="margin-top: 15px" justify="center">
      <n-grid :cols="!store.getIsMobile ? 3 : 1">
        <n-gi :offset="!store.getIsMobile ? 1 : 0">
          <n-form ref="formRef" :model="model" :rules="rules" @submit.prevent>
            <n-form-item path="title" label="Title">
              <n-input v-model:value="model.title" placeholder="Title" />
            </n-form-item>
            <n-form-item path="url" label="Url">
              <n-input v-model:value="model.url" placeholder="Url" />
            </n-form-item>
            <n-space
              size="large"
              :justify="store.getIsMobile ? 'center' : 'end'"
            >
              <n-button
                :disabled="!saveEnabled"
                size="large"
                :ghost="isGhostButton"
                type="primary"
                @click="save"
              >
                <template #icon>
                  <n-icon>
                    <Save />
                  </n-icon>
                </template>
                Save
              </n-button>
              <n-button
                :disabled="!submitEnabled"
                size="large"
                :ghost="isGhostButton"
                type="primary"
                @click="submit"
              >
                <template #icon>
                  <n-icon>
                    <Checkmark />
                  </n-icon>
                </template>
                Submit
              </n-button>
            </n-space>
          </n-form>
        </n-gi>
      </n-grid>
    </n-space>
    <n-divider></n-divider>
    <recipe-details :enable-removal="true" :recipe="response"></recipe-details>
  </n-spin>
</template>
