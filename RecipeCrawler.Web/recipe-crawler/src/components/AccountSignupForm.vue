<script setup lang="ts">
import {
  NForm,
  NInput,
  NFormItem,
  FormInst,
  NSpace,
  NButton,
  FormItemInst,
} from "naive-ui";
import { reactive, ref } from "vue";
import { Account } from "../models/account.model";
import { accountSignupFormRules } from "../services/form-validation.constants";
import { Save, Close } from "@vicons/carbon";
const emit = defineEmits(["closeClicked"]);
const formRef = ref<FormInst | null>(null);
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
    console.log(passwordFormItemRef.value?.validate);
    // passwordFormItemRef.value?.validate("password-input");
  }
};

const submit = () => {
  formRef.value?.validate();
};

const cancel = () => {
  emit("closeClicked", true);
};
</script>
<template>
  <section>
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
      <n-space align="end" justify="end">
        <n-button type="primary" @click="submit">
          <template #icon><save /></template>Confirm
        </n-button>
        <n-button type="warning" @click="cancel">
          <template #icon><close /></template>Cancel
        </n-button>
      </n-space>
    </n-form>
  </section>
</template>
