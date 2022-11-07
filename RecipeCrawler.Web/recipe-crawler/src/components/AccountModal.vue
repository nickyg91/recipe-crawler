<script setup lang="ts">
import { NCard, NSpace, NButton } from "naive-ui";
import { computed } from "vue";
import { useRecipeStore } from "../recipe-store";
import { User } from "@vicons/carbon";
const store = useRecipeStore();
const userInfo = computed(() => store.getUserInfo);
</script>
<template>
  <n-card style="width: 750px" :title="userInfo ? 'Account' : 'Create Account'">
    <n-space v-if="!userInfo" justify="center" align="center">
      <n-button type="primary">
        <template #icon>
          <user></user>
        </template>
        Create Account
      </n-button>
    </n-space>
  </n-card>
</template>
