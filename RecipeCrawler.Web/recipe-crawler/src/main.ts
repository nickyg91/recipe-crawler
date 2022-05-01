import { createApp } from "vue";
import App from "./App.vue";
import "vfonts/Lato.css";
import "vfonts/FiraCode.css";
import "./styles/main.scss";
import { routes } from "./router";
import { createRouter, createWebHistory } from "vue-router";
import { createPinia } from "pinia";
var router = createRouter({
  routes: routes,
  history: createWebHistory(),
});

const app = createApp(App).use(router).use(createPinia());

app.mount("#app");
