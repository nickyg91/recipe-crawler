<script lang="ts" setup>
import { inject, ref } from "vue";
import { useRouter } from "vue-router";
import { AuthenticationService } from "../../services/authentication.service";
import { NSpace, NH1, NH2, NSpin } from "naive-ui";

const router = useRouter();
const timerValue = ref(5);
const guid: string = router.currentRoute.value.params["guid"].toString();
const isAccountVerifying = ref(true);
const isSuccessful = ref(false);
const errored = ref(false);
const accountService: AuthenticationService | null | undefined = inject(
  AuthenticationService.injectionKey
);

function countdownTimer(): void {
  if (timerValue.value > 0) {
    setTimeout(() => {
      timerValue.value--;
      countdownTimer();
    }, 1000);
  } else {
    router.push({
      name: "crawl",
    });
  }
}

try {
  await accountService?.verifyAccount(guid);
  isSuccessful.value = true;
  countdownTimer();
} catch (error) {
  errored.value = true;
} finally {
  isAccountVerifying.value = false;
}
</script>
<template>
  <n-space justify="center">
    <n-h1 v-if="!errored">
      {{ isAccountVerifying ? "Verifying Account" : "Account Verified!" }}
    </n-h1>
    <n-h1 v-if="errored">
      An error occurred verifying your account! Please try again.
    </n-h1>
    <n-spin v-if="isAccountVerifying"> </n-spin>
  </n-space>

  <n-space v-if="isSuccessful" justify="center">
    <n-h2>
      You will be automatically redirected to the home page in
      {{ timerValue }}
    </n-h2>
  </n-space>
</template>
