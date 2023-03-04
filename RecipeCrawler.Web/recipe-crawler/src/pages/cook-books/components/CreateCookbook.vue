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
import { Add, Upload } from "@vicons/carbon";
import { inject, reactive, ref } from "vue";
import { Cookbook } from "../../../models/shared/cookbook.model";
import { CookbookService } from "../services/cookbook.service";
import { ChefferWindow } from "../../../models/window.extension";
import { useRecipeStore } from "../../../recipe-store";
const store = useRecipeStore();
const cookbookService: CookbookService =
  inject(CookbookService.injectionKey) ?? new CookbookService();
const allowedFiles = ["image/png", "image/jpg", "image/gif"];
const cookbook = reactive<Cookbook>({
  name: "",
  id: 0
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

function onUploadFileAdded(options: {
  file: UploadFileInfo;
  fileList: Array<UploadFileInfo>;
  event?: Event;
}): void {
  const file = options.file;
  const maxBytes = 5_000_000;
  const isOverMaxSize = (file.file?.size ?? 0) > maxBytes;
  if (allowedFiles.indexOf(file?.type ?? "") < 0 || isOverMaxSize) {
    uploadRef.value?.clear();
    cookbook.coverImage = null;
    if (isOverMaxSize) {
      showTooBigOfFileError.value = true;
    } else {
      showTooBigOfFileError.value = false;
    }
  } else {
    file?.file?.arrayBuffer().then((data) => {
      cookbook.coverImage = new Uint8Array(data);
    });
  }
}

async function submit(e: MouseEvent): Promise<void> {
  e.preventDefault();
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        const createdCookbook = await cookbookService.saveCookbook(cookbook);
        store.addCookbook(createdCookbook);
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
              Max file size is 5MB.
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
