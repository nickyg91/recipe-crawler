import { createApp } from "vue";
import App from "./App.vue";
import "vfonts/Lato.css";
import "vfonts/FiraCode.css";
import "./styles/main.scss";
import { routes } from "./router";
import { createRouter, createWebHistory } from "vue-router";
import { createPinia } from "pinia";
import { CrawlerApi } from "./services/crawler-api.service";
import axios from "axios";
import { AuthenticationService } from "./services/authentication.service";

export const axiosInstance = axios.create();
const router = createRouter({
  routes: routes,
  history: createWebHistory(),
});


const app = createApp(App).use(router).use(createPinia());
app.provide("crawlerApi", new CrawlerApi());
app.provide("authenticationService", new AuthenticationService());
app.provide("axiosInstance", axiosInstance);
app.mount("#app");
