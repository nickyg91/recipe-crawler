<script setup lang="ts">
import {
  NForm,
  NInput,
  NFormItem,
  FormInst,
  NSpace,
  NButton,
  FormItemInst,
  NAlert,
  useNotification,
  NSpin,
} from "naive-ui";
import { inject, reactive, ref } from "vue";
import { Account } from "../models/account.model";
import { accountSignupFormRules } from "../services/form-validation.constants";
import { Save, Close } from "@vicons/carbon";
import { AuthenticationService } from "../services/authentication.service";
const notificationService = useNotification();
const accountService: AuthenticationService | undefined = inject(
  AuthenticationService.injectionKey
);
const emit = defineEmits(["closeClicked"]);
const formRef = ref<FormInst | null>(null);
const loading = ref<boolean>(false);
const showEmailAlert = ref<boolean>(false);
const passwordFormItemRef = ref<FormItemInst | null>(null);
const formModel = reactive<Account>({
  email: "",
  password: "",
  confirmPassword: "",
  username: "",
});
const signupFormRules = accountSignupFormRules(formModel);

const handlePasswordInput = () => {
  if (formModel.password) {
    passwordFormItemRef.value?.validate("password-input");
  }
};

async function submit() {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      loading.value = true;
      try {
        await accountService?.createAccount(formModel);
        notificationService.success({
          content: "Your account has been created successfully!",
          title: "Success!",
        });
        showEmailAlert.value = true;
      } catch (error) {
        notificationService.error({
          content: "An error occurred attempting to create your account.",
          title: "Error Creating Account",
        });
      } finally {
        loading.value = false;
      }
    }
  });
}

function cancel() {
  emit("closeClicked", true);
}
</script>
<template>
  <section>
    <n-spin :show="loading">
      <n-form ref="formRef" :model="formModel" :rules="signupFormRules">
        <n-form-item path="email" label="Email">
          <n-input
            v-model:value="formModel.email"
            placeholder="email@domain.com"
          />
        </n-form-item>
        <n-form-item path="username" label="Username">
          <n-input v-model:value="formModel.username" placeholder="username" />
        </n-form-item>
        <n-form-item path="password" label="Password">
          <n-input
            ref="passwordFormItemRef"
            v-model:value="formModel.password"
            type="password"
            placeholder="password"
            @input="handlePasswordInput"
          />
        </n-form-item>
        <n-form-item path="confirmPassword" label="Confirm Password">
          <n-input
            v-model:value="formModel.confirmPassword"
            type="password"
            placeholder="confirm password"
          />
        </n-form-item>
        <n-space v-if="showEmailAlert" style="margin-bottom: 5px" :size="12">
          <n-alert type="success" title="Success!">
            Your account has been created successfully! A confirmation email
            will be sent to the email address provided. Please use this email to
            verify your account.
          </n-alert>
        </n-space>
        <n-space stlyle="top: 5px" align="end" justify="end">
          <n-button v-if="!showEmailAlert" type="primary" @click="submit">
            <template #icon><save /></template>Confirm
          </n-button>
          <n-button type="warning" @click="cancel">
            <template #icon><close /></template>

            {{ showEmailAlert ? 'Close' : 'Cancel' }}
          </n-button>
        </n-space>
      </n-form>
    </n-spin>
  </section>
</template>
