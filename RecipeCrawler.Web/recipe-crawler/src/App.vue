<script setup lang="ts">
// This starter template is using Vue 3 <script setup> SFCs
// Check out https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup
import {
  NConfigProvider,
  NNotificationProvider,
  NLayout,
  darkTheme,
  NLayoutContent,
} from "naive-ui";
import { RouterView } from "vue-router";
</script>
<template>
  <n-config-provider :theme="darkTheme">
    <n-notification-provider>
      <n-layout class="full-height">
        <n-layout-content>
          <router-view />
        </n-layout-content>
      </n-layout>
    </n-notification-provider>
  </n-config-provider>
</template>
