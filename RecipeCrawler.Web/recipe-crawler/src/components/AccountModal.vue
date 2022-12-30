<script setup lang="ts">
import { NCard, NSpace, NButton, NNotificationProvider } from "naive-ui";
import { computed, ref } from "vue";
import { useRecipeStore } from "../recipe-store";
import { User, LogoGoogle, Close } from "@vicons/carbon";
import AccountSignupForm from "./AccountSignupForm.vue";
import LogInForm from "./LogInForm.vue";
const showSignupForm = ref(false);
const store = useRecipeStore();
const userInfo = computed(() => store.getUserInfo);
const emit = defineEmits(["closeClicked"]);
const showLoginForm = ref(false);
const onCreateAccountClicked = () => {
  showSignupForm.value = true;
};
const onCloseClicked = () => {
  emit("closeClicked", true);
};
const title = computed(() => {
  if (userInfo.value) {
    return "Account";
  }
  if (showSignupForm.value) {
    return "Create Account";
  }
  if (showLoginForm.value) {
    return "Log In";
  }
  return "Account";
});
</script>
<template>
  <n-card style="width: 750px" :title="title">
    <template #header-extra>
      <n-space justify="end">
        <n-button circle primary @click="onCloseClicked">
          <template #icon>
            <close></close>
          </template>
        </n-button>
      </n-space>
    </template>
    <n-notification-provider>
      <section>
        <n-space
          v-if="!userInfo && !showSignupForm && !showLoginForm"
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
          <n-button
            style="width: 225px"
            quaternary
            type="info"
            @click="showLoginForm = true"
          >
            Already have an account? Log in!
          </n-button>
        </n-space>
        <account-signup-form
          v-if="showSignupForm"
          @close-clicked="showSignupForm = false"
        ></account-signup-form>
        <log-in-form
          v-if="showLoginForm"
          @cancel-clicked="showLoginForm = false"
        ></log-in-form>
      </section>
    </n-notification-provider>
  </n-card>
</template>
<style scoped lang="scss"></style>
