import { RouteRecordRaw } from "vue-router";
import RecipeCrawler from "./components/RecipeCrawler.vue";
import SavedRecipe from "./components/SavedRecipe.vue";
import NotFound from "./components/NotFound.vue";
export const routes = [
  {
    path: "/",
    component: RecipeCrawler,
    name: "crawl",
  },
  {
    path: "/recipe/:url",
    component: SavedRecipe,
    props: true,
    name: "savedRecipe",
  },
  {
    path: "/:catchAll(.*)",
    component: NotFound,
    props: true,
  },
] as RouteRecordRaw[];
