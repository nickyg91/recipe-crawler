<script lang="ts" setup>
import NoCookbooks from "./components/NoCookbooks.vue";
import CreateCookbook from "./components/CreateCookbook.vue";
import { NDrawer, NDrawerContent } from "naive-ui";
import { computed, ref } from "vue";
import { useRecipeStore } from "../../recipe-store";
const store = useRecipeStore();
const cookbooks = computed(() => store.cookbooks);
const showAddItemDrawer = ref(false);
</script>
<template>
  <section>
    <NoCookbooks
      v-if="cookbooks?.length < 1"
      @create-clicked="showAddItemDrawer = true"
    />
    <n-drawer
      v-model:show="showAddItemDrawer"
      placement="left"
      :native-scrollbar="true"
      width="500"
    >
      <n-drawer-content title="Create Cookbook" closable>
        <CreateCookbook />
      </n-drawer-content>
    </n-drawer>
  </section>
</template>
