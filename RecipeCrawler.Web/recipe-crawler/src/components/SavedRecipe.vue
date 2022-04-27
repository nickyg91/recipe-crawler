<script lang="ts" setup>
import { reactive, ref } from "@vue/reactivity";
import axios from "axios";
import { useRouter } from "vue-router";
import { ParsedResponse } from "../models/parsed-response.model";
import { NSpin, NSpace, NH1 } from "naive-ui";
import RecipeDetails from "./RecipeDetails.vue";
let state = reactive({
  recipe: new ParsedResponse(),
});
const loading = ref(true);
const router = useRouter();
const url = router.currentRoute.value.params.url;
axios.get(`/api/features/crawler/${url}`).then((result) => {
  loading.value = false;
  state.recipe = result.data;
});
</script>
<template>
  <n-spin :show="loading">
    <n-space vertical justify="center">
      <n-h1>
        {{ state.recipe.title }}
      </n-h1>
      <recipe-details
        :enable-removal="false"
        :recipe="state.recipe"
      ></recipe-details>
    </n-space>
  </n-spin>
</template>
