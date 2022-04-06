<script setup lang="ts">
import { NSpace, NH1, NLayout, NLayoutContent } from "naive-ui";
</script>
<template>
  <n-layout content-style="padding: 1.5em;">
    <n-layout-content>
      <n-space size="large" justify="center" vertical>
        <n-h1 type="success" :align-text="true">404 - Page not found.</n-h1>
      </n-space>
    </n-layout-content>
  </n-layout>
</template>
