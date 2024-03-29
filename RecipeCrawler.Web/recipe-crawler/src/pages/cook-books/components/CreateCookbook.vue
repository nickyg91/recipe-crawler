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
  UploadFileInfo,
} from "naive-ui";
import type { UploadInst } from "naive-ui";
import { Add, Upload, Save } from "@vicons/carbon";
import { inject, reactive, ref } from "vue";
import { Cookbook } from "../../../models/shared/cookbook.model";
import { CookbookService } from "../services/cookbook.service";
import { ChefferWindow } from "../../../models/window.extension";
import { useRecipeStore } from "../../../recipe-store";
const emits = defineEmits(["onSave"]);
const store = useRecipeStore();
const cookbookService: CookbookService =
  inject(CookbookService.injectionKey) ?? new CookbookService();
const allowedFiles = ["image/png", "image/jpg", "image/gif"];
const editableCookbook = store.getCurrentlyEditedCookbook;
const cookbook = editableCookbook
  ? editableCookbook
  : reactive<Cookbook>({
      name: "",
      id: 0,
    });
const formRef = ref<FormInst | null>(null);
const uploadRef = ref<UploadInst | null>(null);

const showTooBigOfFileError = ref(false);
const rules = {
  cookbook: {
    name: [
      {
        required: true,
        message: "Name is required",
        max: 256,
        trigger: ["input", "blur"],
      },
    ],
  },
} as FormRules;

async function base64Arraybuffer(data: ArrayBuffer): Promise<string | null> {
  const base64url: string = await new Promise((r) => {
    const reader = new FileReader();
    reader.onload = () => r(reader.result?.toString() ?? "");
    reader.readAsDataURL(new Blob([data]));
  });
  return base64url.length > 0 ? base64url.split(",", 2)[1] : null;
}

async function onUploadFileAdded(options: {
  file: UploadFileInfo;
  fileList: Array<UploadFileInfo>;
  event?: Event;
}): Promise<void> {
  const file = options.file;
  const maxBytes = 1_000_000;
  const isOverMaxSize = (file.file?.size ?? 0) > maxBytes;
  if (allowedFiles.indexOf(file?.type ?? "") < 0 || isOverMaxSize) {
    uploadRef.value?.clear();
    cookbook.coverImageBase64 = null;
    if (isOverMaxSize) {
      showTooBigOfFileError.value = true;
    } else {
      showTooBigOfFileError.value = false;
    }
  } else {
    const data = await file?.file?.arrayBuffer();
    if (data) {
      cookbook.coverImageBase64 = await base64Arraybuffer(new Uint8Array(data));
    }
  }
}

async function submit(e: MouseEvent): Promise<void> {
  e.preventDefault();
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        const createdCookbook = await cookbookService.saveCookbook(cookbook);
        if (cookbook.id == 0) {
          store.addCookbook(createdCookbook);
        }
        emits("onSave");
      } catch (error) {
        (window as ChefferWindow)?.$message?.error(
          "An error occurred while saving your cookbook!"
        );
      }
    }
  });
}
</script>
<template>
  <section>
    <n-form ref="formRef" :model="cookbook" :rules="rules" size="large">
      <n-form-item label="Name" path="name">
        <n-input v-model:value="cookbook.name" placeholder="Cookbook Name" />
      </n-form-item>
      <n-form-item label="Cover Photo">
        <n-upload
          ref="uploadRef"
          directory-dnd
          :max="1"
          @change="onUploadFileAdded($event)"
        >
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
            <n-p depth="3" style="margin: 8px 0 0 0">
              Max file size is 1MB.
            </n-p>
          </n-upload-dragger>
        </n-upload>
      </n-form-item>
      <n-text v-if="showTooBigOfFileError" type="error">
        File size is too big!
      </n-text>
      <n-row :gutter="[0, 24]">
        <n-col :span="24">
          <n-space justify="end">
            <n-button
              v-if="cookbook.id < 1"
              size="large"
              type="primary"
              @click="submit($event)"
            >
              <template #icon>
                <Add />
              </template>
              Create
            </n-button>
            <n-button
              v-if="cookbook.id > 0"
              size="large"
              type="primary"
              @click="submit($event)"
            >
              <template #icon>
                <Save />
              </template>
              Update
            </n-button>
          </n-space>
        </n-col>
      </n-row>
    </n-form>
  </section>
</template>
