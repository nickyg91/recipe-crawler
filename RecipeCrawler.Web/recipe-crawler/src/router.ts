import { RouteRecordRaw } from "vue-router";
import RecipeCrawler from "./components/RecipeCrawler.vue";
import SavedRecipe from "./components/SavedRecipe.vue";
import NotFound from "./components/NotFound.vue";
export const routes = [
  {
    path: "/",
    component: RecipeCrawler,
  } as RouteRecordRaw,
  {
    path: "/recipe/:url",
    component: SavedRecipe,
    props: true,
  } as RouteRecordRaw,
  {
    path: "/:catchAll(.*)",
    component: NotFound,
    props: true,
  } as RouteRecordRaw,
] as RouteRecordRaw[];
