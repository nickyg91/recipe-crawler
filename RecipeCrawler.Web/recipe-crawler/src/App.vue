<script setup lang="ts">
// This starter template is using Vue 3 <script setup> SFCs
// Check out https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup
import { Home, Moon } from "@vicons/carbon";
import { computed, ref } from "@vue/reactivity";
import {
  NConfigProvider,
  NNotificationProvider,
  NLayout,
  darkTheme,
  NLayoutContent,
  NLayoutSider,
  NMenu,
  MenuOption,
  NSwitch,
  NSpace,
  NIcon,
} from "naive-ui";
import { h } from "vue";
import { RouterLink, RouterView } from "vue-router";
import { useRecipeStore } from "./recipe-store";
let collapsed = ref(false);
const state = useRecipeStore();
const menuOptions: MenuOption[] = [
  {
    label: () =>
      h(
        RouterLink,
        {
          to: {
            name: "crawl",
          },
          activeClass: "n-menu-item-content--selected",
        },
        {
          default: () => "Home",
        }
      ),
    key: "home",
    icon: () => h(Home),
  },
];
const getTheme = computed(() => {
  return state.getTheme ? null : darkTheme;
});
</script>
<template>
  <n-config-provider :theme="getTheme">
    <n-notification-provider>
      <n-layout class="full-height" has-sider>
        <n-layout-sider
          collapse-mode="width"
          :collapsed-width="64"
          :width="240"
          show-trigger="bar"
          bordered
          @collapse="collapsed = true"
          @expand="collapsed = false"
        >
          <n-menu
            :collapsed="collapsed"
            :collapsed-width="64"
            :collapsed-icon-size="22"
            :options="menuOptions"
          />
        </n-layout-sider>
        <n-layout-content content-style="padding: 24px;">
          <n-space align="end">
            <n-icon><moon></moon></n-icon>
            <n-switch @update:value="state.setTheme($event)"></n-switch>
          </n-space>
          <router-view />
        </n-layout-content>
      </n-layout>
    </n-notification-provider>
  </n-config-provider>
</template>
