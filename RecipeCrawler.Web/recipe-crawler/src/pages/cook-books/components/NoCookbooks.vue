<script setup lang="ts">
import { NSpace, NButton, NIcon, NH1 } from "naive-ui";
import { Add } from "@vicons/carbon";
</script>
<template>
  <n-space justify="center">
    <section>
      <n-h1>
        You don't seem to have any cookbooks yet. Would you like to create one?
      </n-h1>
    </section>
  </n-space>

  <n-space justify="center" class="my-5">
    <section>
      <n-button dashed size="large" type="default">
        <template #icon>
          <n-icon>
            <Add />
          </n-icon>
        </template>
        Create
      </n-button>
    </section>
  </n-space>
</template>
