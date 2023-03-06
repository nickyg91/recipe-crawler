<script setup lang="ts">
import {
  NButton,
  NForm,
  NInput,
  NFormItem,
  FormInst,
  FormRules,
  FormItemRule,
  NRow,
  NCol,
} from "naive-ui";
import { inject, reactive, ref } from "vue";
import { Login } from "../../../models/login.model";
import { useRecipeStore } from "../../../recipe-store";
import { AuthenticationService } from "../../../services/authentication.service";
const authService: AuthenticationService | null = inject(
  AuthenticationService.injectionKey,
  new AuthenticationService()
);
const store = useRecipeStore();
const emits = defineEmits(["cancelClicked", "successfulSubmit"]);
const loginModel = reactive(new Login("", ""));
const formRef = ref<FormInst | null>(null);
const rules: FormRules = {
  username: {
    required: true,
    validator(rule: FormItemRule, value: string) {
      if (!value) {
        return new Error("Username is required.");
      }
    },
  },
  password: {
    required: true,
    validator(rule: FormItemRule, value: string) {
      if (!value) {
        return new Error("Password is required.");
      }
    },
  },
};
async function submitLogin() {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      const response = await authService?.login(loginModel);
      if (response) {
        store.setUserInfo(response);
        emits("successfulSubmit");
      }
    }
  });
}

function cancel() {
  emits("cancelClicked");
}
</script>
<template>
  <n-form ref="formRef" :model="loginModel" :rules="rules">
    <n-form-item path="username" label="Username">
      <n-input
        v-model:value="loginModel.username"
        :autofocus="true"
        placeholder="Username"
      />
    </n-form-item>
    <n-form-item path="password" label="Password">
      <n-input
        v-model:value="loginModel.password"
        type="password"
        :show-password-on="'click'"
        placeholder="Password"
      />
    </n-form-item>
    <n-row :gutter="[0, 24]">
      <n-col style="display: flex; justify-content: flex-end" :span="24">
        <n-button
          type="primary"
          style="margin-right: 5px"
          @click="submitLogin()"
        >
          Submit
        </n-button>
        <n-button type="warning" @click="cancel()"> Cancel </n-button>
      </n-col>
    </n-row>
  </n-form>
</template>
