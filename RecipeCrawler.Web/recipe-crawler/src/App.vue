<script setup lang="ts">
// This starter template is using Vue 3 <script setup> SFCs
// Check out https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup
import { NConfigProvider, NSpace, darkTheme } from 'naive-ui';
import RecipeCrawler from './components/RecipeCrawler.vue';
</script>
<template>
  <n-config-provider> 
  <n-space content-style="padding: 24px;" justify="center">
    <RecipeCrawler />
  </n-space>
  </n-config-provider>
</template>