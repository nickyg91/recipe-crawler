<script setup lang="ts">
// This starter template is using Vue 3 <script setup> SFCs
// Check out https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup
import {
  NConfigProvider,
  NNotificationProvider,
  NLayout,
  darkTheme,
} from "naive-ui";
import RecipeCrawler from "./components/RecipeCrawler.vue";
</script>
<template>
  <n-config-provider :theme="darkTheme">
    <n-notification-provider>
      <n-layout class="full-height">
        <RecipeCrawler />
      </n-layout>
    </n-notification-provider>
  </n-config-provider>
</template>
