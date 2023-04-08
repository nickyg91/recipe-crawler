<script setup lang="ts">
import { useLoadingBar } from "naive-ui";
import { RouterView } from "vue-router";
import { setupInstance } from "../services/axios-instance.model";

const loadingBar = useLoadingBar();
setupInstance(loadingBar);
</script>
<template>
  <router-view />
</template>
