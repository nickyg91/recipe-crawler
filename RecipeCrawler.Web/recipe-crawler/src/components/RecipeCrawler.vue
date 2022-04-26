<script setup lang="ts">
import { reactive, ref } from "vue";
import axios, { AxiosResponse } from "axios";
import { ParsedResponse } from "../models/parsed-response.model";
import {
  NButton,
  NGrid,
  NGi,
  NForm,
  NFormItem,
  NInput,
  NLayout,
  NLayoutContent,
  NSpace,
  NIcon,
  NCard,
  FormRules,
  FormInst,
  useNotification,
  NSpin,
  NDivider,
} from "naive-ui";
import { Checkmark, Close, Save } from "@vicons/carbon";
import { computed } from "@vue/reactivity";
import { useRouter } from "vue-router";
const notificationService = useNotification();
const router = useRouter();
let response = reactive(new ParsedResponse());
let loading = ref(false);
const removeIngredient = (index: number) => {
  response.ingredients.splice(index, 1);
};
const removeStep = (index: number) => {
  response.steps.splice(index, 1);
};
const formRef = ref<FormInst | null>(null);
const model = reactive({
  url: "",
});
const rules: FormRules = {
  url: {
    required: true,
    pattern: /(https?:\/\/)?([\da-z\.-]+)\.([a-z]{2,6})([\/\w\.-]*)*\/?/,
    message: "A valid URL must be supplied.",
    trigger: ["input", "blur"],
  },
};
const submitEnabled = computed(() => {
  return model.url.length > 1;
});

const saveEnabled = computed(() => {
  return response.ingredients?.length > 0 || response.steps?.length > 0;
});

const submit = () => {
  formRef.value?.validate((errors) => {
    if (!errors) {
      loading.value = true;
      axios
        .post("/api/features/crawler", {
          url: model.url,
        })
        .then((result: AxiosResponse<ParsedResponse>) => {
          loading.value = false;
          response.ingredients = result.data.ingredients;
          response.steps = result.data.steps;
          notificationService.success({
            title: "Success!",
            content: "The recipe has been scraped.",
            duration: 3000,
          });
        })
        .catch((error) => {
          loading.value = false;
          notificationService.error({
            content: "An error occurred attempting to scrape the recipe.",
            title: "Error",
            duration: 3000,
          });
        });
    } else {
      notificationService.error({
        content: "Invalid URL.",
        title: "Validation Error",
      });
    }
  });
};

const save = () => {
  loading.value = true;
  response.url = model.url;
  axios.post("/api/features/crawler/save", response).then(
    (result: AxiosResponse<string>) => {
      router.push({ name: "savedRecipe", params: { url: result.data } });
      loading.value = false;
    },
    (error) => {
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
    <n-layout>
      <n-layout-content>
        <n-space vertical size="large">
          <n-grid x-gap="12" :cols="3">
            <n-gi offset="1">
              <n-form
                ref="formRef"
                :model="model"
                :rules="rules"
                v-on:submit.prevent
              >
                <n-form-item path="url" label="Url">
                  <n-input placeholder="Url" v-model:value="model.url" />
                </n-form-item>
                <div>
                  <n-space size="large" justify="end">
                    <n-button
                      :disabled="!saveEnabled"
                      size="large"
                      ghost
                      @click="save"
                      type="primary"
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
                      ghost
                      @click="submit"
                      type="primary"
                    >
                      <template #icon>
                        <n-icon>
                          <Checkmark />
                        </n-icon>
                      </template>
                      Submit
                    </n-button>
                  </n-space>
                </div>
              </n-form>
            </n-gi>
          </n-grid>
        </n-space>
        <n-divider></n-divider>
        <n-space vertical justify="center">
          <n-card
            style="margin-bottom: 5px"
            v-for="(item, index) in response.ingredients"
          >
            <n-space justify="end">
              <n-button
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
          <n-card v-for="(item, index) in response.steps">
            <n-space justify="end">
              <n-button
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
      </n-layout-content>
    </n-layout>
  </n-spin>
</template>
