<script lang="ts" setup>
import { reactive, ref } from "@vue/reactivity";
import axios from "axios";
import { useRouter } from "vue-router";
import { ParsedResponse } from "../models/parsed-response.model";
import { NSpin } from "naive-ui";
let recipe = reactive(new ParsedResponse());
const loading = ref(true);
const router = useRouter();
const url = router.currentRoute.value.params.url;
axios.get(`/api/features/crawler/${url}`).then((result) => {
  loading.value = false;
  recipe = Object.assign(result.data);
  console.log(result);
});
</script>
<template>
  <n-spin :show="loading"> loaded! </n-spin>
</template>
