<script setup lang="ts">
import { NCard } from "naive-ui";
import { computed } from "vue";
import { useRecipeStore } from "../recipe-store";
const store = useRecipeStore();
const userInfo = computed(() => store.getUserInfo);
</script>
<template>
  <n-card style="width: 750px" :title="userInfo ? 'Account' : 'Create Account'">
    content
  </n-card>
</template>
