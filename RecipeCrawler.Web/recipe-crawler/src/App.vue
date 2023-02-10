<script setup lang="ts">
// This starter template is using Vue 3 <script setup> SFCs
// Check out https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup
import { Home, Moon, Sun, Warning, User, Catalog } from "@vicons/carbon";
import { computed, reactive, ref, watch } from "vue";
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
  NButton,
  NModal,
} from "naive-ui";
import { h } from "vue";
import { RouterLink, RouterView, useRoute } from "vue-router";
import { useRecipeStore } from "./recipe-store";
import AccountModal from "./components/AccountModal.vue";
const route = useRoute();
const selectedKeyRef = ref();
watch(
  () => route.name,
  (val) => {
    selectedKeyRef.value = val?.valueOf();
  }
);

const state = useRecipeStore();

const collapsed = ref(false);
const isMobileSize = window.innerWidth <= 760;
state.setIsMobile(isMobileSize);
window.addEventListener("resize", () => {
  const isMobile = window.innerWidth <= 760;
  state.setIsMobile(isMobile);
});
const menuOptions: MenuOption[] = reactive([
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
          default: () => "Home",
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
          default: () => "Reported Urls",
        }
      ),
    key: "reportedUrls",
    icon: () => h(Warning),
  },
]);

state.$onAction(
  ({
    name,
    // after  
  }) => {
    if (name === "setUserInfo") {
      menuOptions.push({
        label: () => h(
          RouterLink,
          {
            to: {
              name: "recipeBooks"
            }
          },
          {
            default: () => "Recipe Books"
          }
        ),
        key: "recipeBooks",
        icon: () => h(Catalog)
      })
    }
  }
);

const getTheme = computed(() => {
  return state.getIsLightMode ? null : darkTheme;
});
const isMobile = computed(() => {
  return state.isMobile;
});

const showAccountModal = ref(false);
const closeModal = () => {
  showAccountModal.value = false;
};
const openAccountModal = () => {
  showAccountModal.value = true;
};
</script>
<template>
  <n-config-provider :theme="getTheme">
    <n-notification-provider>
      <n-layout class="full-height" has-sider>
        <n-layout-sider
          v-if="!isMobile"
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
            :value="selectedKeyRef"
          />
        </n-layout-sider>
        <n-layout-content content-style="padding: 24px;">
          <n-space justify="end" align="center">
            <n-switch @update:value="state.setTheme($event)">
              <template #icon>
                <n-icon v-if="!getTheme"><moon></moon></n-icon>
                <n-icon v-if="getTheme"><sun></sun></n-icon>
              </template>
            </n-switch>
            <n-button
              strong
              primary
              circle
              type="primary"
              @click="openAccountModal"
            >
              <template #icon>
                <n-icon>
                  <User />
                </n-icon>
              </template>
            </n-button>
          </n-space>
          <router-view />
        </n-layout-content>
      </n-layout>
      <n-layout-footer
        v-if="isMobile"
        style="padding-top: 5px; padding-bottom: 5px"
        position="absolute"
      >
        <n-menu
          v-model:value="selectedKeyRef"
          :class="{ 'mobile-menu': isMobile }"
          :options="menuOptions"
          mode="horizontal"
        />
      </n-layout-footer>
      <n-modal :show="showAccountModal">
        <AccountModal @close-clicked="closeModal" />
      </n-modal>
    </n-notification-provider>
  </n-config-provider>
</template>
<style>
.mobile-menu.n-menu .n-menu-item-content {
  display: block;
  text-align: center;
  line-height: 0.95;
}
.mobile-menu.n-menu .n-menu-item-content .n-menu-item-content__icon {
  margin-right: 0 !important;
}
</style>
