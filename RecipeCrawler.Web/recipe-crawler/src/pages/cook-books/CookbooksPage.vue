<script lang="ts" setup>
import CreateCookbook from "./components/CreateCookbook.vue";
import CookbookList from "./components/CookbookList.vue";
import { NDrawer, NDrawerContent, NH1 } from "naive-ui";
import { computed, ref, watch } from "vue";
import { useRecipeStore } from "../../recipe-store";
import { ChefferWindow } from "../../models/window.extension";
const store = useRecipeStore();
const cookbooks = computed(() => store.cookbooks);
try {
  await store.setCookbooks();
} catch (error) {
  (window as ChefferWindow).$message?.error(
    "Error getting your cookbooks! Try again."
  );
}

const showAddItemDrawer = ref(false);
watch(
  () => store.currentlySelectedCookbookToEdit,
  (editing) => {
    if (editing) {
      showAddItemDrawer.value = true;
    }
  }
);

function afterClose() {
  store.removeEditedChanges();
}
</script>
<template>
  <section>
    <n-h1> Cookbooks </n-h1>
    <!-- <NoCookbooks
      v-if="cookbooks?.length < 1"
      @create-clicked="showAddItemDrawer = true"
    /> -->
    <CookbookList
      :cookbooks="cookbooks"
      @on-add-clicked="showAddItemDrawer = true"
    />
    <n-drawer
      v-model:show="showAddItemDrawer"
      placement="left"
      :native-scrollbar="true"
      width="500"
      @after-leave="afterClose()"
    >
      <n-drawer-content title="Create Cookbook" closable>
        <CreateCookbook />
      </n-drawer-content>
    </n-drawer>
  </section>
</template>
