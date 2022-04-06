import { createApp } from "vue";
import App from "./App.vue";
import "vfonts/Lato.css";
import "vfonts/FiraCode.css";
import "./styles/main.scss";
import { routes } from "./router";
import { createRouter, createWebHistory } from "vue-router";
var router = createRouter({
  routes: routes,
  history: createWebHistory(),
});

createApp(App).use(router).mount("#app");
