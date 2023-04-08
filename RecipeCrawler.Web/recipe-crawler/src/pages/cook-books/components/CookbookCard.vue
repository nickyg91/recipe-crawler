<script setup lang="ts">
import { NCard, NIcon, NSpace, useDialog, NButton } from "naive-ui";
import { DocumentHorizontal, Close, Edit } from "@vicons/carbon";
import { Cookbook } from "../../../models/shared/cookbook.model";
import { type PropType } from "vue";
import { useRecipeStore } from "../../../recipe-store";
import { useRouter } from "vue-router";
const router = useRouter();
const dialog = useDialog();
const props = defineProps({
  cookbook: {
    type: Object as PropType<Cookbook>,
    required: true,
  },
});

const store = useRecipeStore();

function cardClicked(): void {
  router.push({
    name: "recipes",
    params: {
      cookbookId: props.cookbook.id,
    },
  });
}

async function editClicked(): Promise<void> {
  store.setCurrentlyEditingCookbook(props.cookbook.id);
  return;
}

async function deleteClicked(id: number): Promise<void> {
  dialog.warning({
    content:
      "Are you sure you want to delete this? You wont be able to get it back.",
    title: "Delete?",
    positiveText: "Yes",
    negativeText: "No",
    onPositiveClick: async () => {
      await store.deleteCookbook(id);
    },
  });
}
</script>
<template>
  <n-card hoverable size="large" :title="cookbook?.name" @click="cardClicked()">
    <template #cover>
      <img
        v-if="cookbook!.coverImageBase64 && cookbook.coverImageBase64.length > 0"
        :src="'data:image/png;base64, ' + cookbook.coverImageBase64"
      />
      <div v-if="!cookbook!.coverImageBase64" style="text-align: center">
        <n-icon size="128">
          <DocumentHorizontal />
        </n-icon>
      </div>
    </template>
    <template #action>
      <n-space justify="center" align="center">
        <n-button type="info" circle strong secondary @click="editClicked()">
          <template #icon>
            <n-icon>
              <Edit />
            </n-icon>
          </template>
        </n-button>
        <n-button
          type="warning"
          circle
          strong
          secondary
          @click="deleteClicked(cookbook.id)"
        >
          <template #icon>
            <n-icon>
              <Close />
            </n-icon>
          </template>
        </n-button>
      </n-space>
    </template>
  </n-card>
</template>
<style scoped>
.n-icon {
  padding: 4%;
}
.n-card {
  max-height: 300px;
  height: 300px;
}
</style>
