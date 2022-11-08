<script setup lang="ts">
import { NCard, NSpace, NButton } from "naive-ui";
import { computed, ref } from "vue";
import { useRecipeStore } from "../recipe-store";
import { User, LogoGoogle, Close } from "@vicons/carbon";
const showSignupForm = ref(false);
const store = useRecipeStore();
const userInfo = computed(() => store.getUserInfo);
const emit = defineEmits(["closeClicked"]);
const onCreateAccountClicked = () => {
  showSignupForm.value = true;
};
const onCloseClicked = () => {
  emit("closeClicked", true);
};
</script>
<template>
  <n-card style="width: 750px" :title="userInfo ? 'Account' : 'Create Account'">
    <template #header-extra>
      <n-space justify="end">
        <n-button circle primary @click="onCloseClicked">
          <template #icon>
            <close></close>
          </template>
        </n-button>
      </n-space>
    </template>
    <n-space
      v-if="!userInfo"
      style="min-height: 450px"
      justify="center"
      vertical
      align="center"
    >
      <n-button
        style="width: 225px"
        type="primary"
        @click="onCreateAccountClicked"
      >
        <template #icon>
          <user></user>
        </template>
        Create Account
      </n-button>
      <n-button style="width: 225px" type="primary" disabled>
        <template #icon>
          <logo-google></logo-google>
        </template>
        Sign-in with Google
      </n-button>
    </n-space>
  </n-card>
</template>
