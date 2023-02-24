<script setup lang="ts">
import {
  FormInst,
  FormRules,
  NForm,
  NFormItem,
  NInput,
  NSpace,
  NRow,
  NCol,
  NButton,
  NUpload,
  NUploadDragger,
  NText,
  NP,
  NIcon,
} from "naive-ui";
import type { UploadInst } from "naive-ui";
import { Add, Upload } from "@vicons/carbon";
import { reactive, ref } from "vue";
import { Cookbook } from "../../../models/shared/cookbook.model";
const cookbook = reactive(new Cookbook());
const formRef = ref<FormInst | null>(null);
const uploadRef = ref<UploadInst | null>(null);
const rules = {
  cookbook: {
    name: {
      required: true,
      message: "Name is required",
      trigger: "blur",
    },
  },
} as FormRules;

function submit(e: MouseEvent): void {
  e.preventDefault();
  formRef.value?.validate((errors) => {
    if (!errors) {
      //yay
    } else {
      //not yay
    }
  });
}
</script>
<template>
  <section>
    <n-form ref="formRef" :model="cookbook" :rules="rules" size="large">
      <n-form-item label="Name" path="cookbook.name">
        <n-input v-model:value="cookbook.name" placeholder="Cookbook Name" />
      </n-form-item>
      <n-form-item label="Cover Photo" path="cookbook.coverImage">
        <n-upload ref="uploadRef" directory-dnd :max="1">
          <n-upload-dragger>
            <div style="margin-bottom: 12px; font-size: 32px">
              <n-icon>
                <Upload />
              </n-icon>
            </div>
            <n-text style="font-size: 16px">
              Click or drag a file to this area to upload a cover image.
            </n-text>
            <n-p depth="3" style="margin: 8px 0 0 0">
              Supported image file formats are .png, .jpg, or .gif.
            </n-p>
          </n-upload-dragger>
        </n-upload>
      </n-form-item>
      <n-row :gutter="[0, 24]">
        <n-col :span="24">
          <n-space justify="end">
            <n-button size="large" type="primary" @click="submit($event)">
              <template #icon>
                <Add />
              </template>
              Create
            </n-button>
          </n-space>
        </n-col>
      </n-row>
    </n-form>
  </section>
</template>
