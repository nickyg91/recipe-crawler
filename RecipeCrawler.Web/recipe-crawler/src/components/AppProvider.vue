<script lang="ts" setup>
import { NMessageProvider, NConfigProvider, darkTheme } from "naive-ui";
import App from "../App.vue";
import { useRecipeStore } from "../recipe-store";
import { computed } from "vue";

const state = useRecipeStore();

const getTheme = computed(() => {
  return state.getIsLightMode ? null : darkTheme;
});
</script>
<template>
  <n-config-provider :theme="getTheme">
    <n-message-provider>
      <app />
    </n-message-provider>
  </n-config-provider>
</template>
