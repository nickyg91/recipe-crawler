<script setup lang="ts">
import { FormItemRule } from "naive-ui";
import { reactive } from "vue";
import { Account } from "../models/account.model";
import { accountSignupFormRules } from "../services/form-validation.constants";

const formModel = reactive<Account>({
  email: "",
  password: "",
  confirmPassword: "",
  username: "",
});
const signupFormRules = accountSignupFormRules;
(signupFormRules["confirmPassword"] as FormItemRule[]).push({
  validator(rule: FormItemRule, value: string) {
    if (value === formModel.password) {
      return true;
    } else {
      return new Error("Password and Confirm Password must be the same.");
    }
  },
  message: "Confirm Password must be the same as Password.",
  trigger: ["blur", "password-input"],
});
</script>
<template>
  <p>test</p>
</template>
