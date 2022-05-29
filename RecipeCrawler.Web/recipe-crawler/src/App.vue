<script setup lang="ts">
// This starter template is using Vue 3 <script setup> SFCs
// Check out https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup
import { Home, Moon, Sun, Warning } from "@vicons/carbon";
import { computed, ref } from "vue";
import {
  NConfigProvider,
  NNotificationProvider,
  NLayout,
  darkTheme,
  NLayoutContent,
  NLayoutSider,
  NLayoutFooter,
  NMenu,
  MenuOption,
  NSwitch,
  NSpace,
  NIcon,
} from "naive-ui";
import { h } from "vue";
import { RouterLink, RouterView, useRoute } from "vue-router";
import { useRecipeStore } from "./recipe-store";
const route = useRoute();
const state = useRecipeStore();
let collapsed = ref(false);

const isMobile = (): boolean => {
  const isMobile = window.innerWidth <= 760;
  return isMobile;
};
const selectedKeyRef = ref(route.name?.toString());
const menuOptions: MenuOption[] = [
  {
    label: () =>
      h(
        RouterLink,
        {
          to: {
            name: "crawl",
          },
        },
        {
          default: () => (isMobile() ? "" : "Home"),
        }
      ),
    key: "crawl",
    icon: () => h(Home),
  },
  {
    label: () =>
      h(
        RouterLink,
        {
          to: {
            name: "reportedUrls",
          },
        },
        {
          default: () => (isMobile() ? "" : "Reported Urls"),
        }
      ),
    key: "reportedUrls",
    icon: () => h(Warning),
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
          v-if="!isMobile()"
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
            v-model:value="selectedKeyRef"
          />
        </n-layout-sider>
        <n-layout-content content-style="padding: 24px;">
          <n-space align="end">
            <n-switch @update:value="state.setTheme($event)">
              <template #icon v-if="!getTheme">
                <n-icon><moon></moon></n-icon>
              </template>
              <template #icon v-if="getTheme">
                <n-icon><sun></sun></n-icon>
              </template>
            </n-switch>
          </n-space>
          <router-view />
        </n-layout-content>
      </n-layout>
      <n-layout-footer position="absolute" v-if="isMobile()">
        <n-menu
          :options="menuOptions"
          mode="horizontal"
          v-model:value="selectedKeyRef"
        />
      </n-layout-footer>
    </n-notification-provider>
  </n-config-provider>
</template>
