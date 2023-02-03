import { RouteRecordRaw } from "vue-router";
import RecipeCrawler from "./components/RecipeCrawler.vue";
import SavedRecipe from "./components/SavedRecipe.vue";
import NotFound from "./components/NotFound.vue";
import ReportedUrls from "./components/ReportedUrls.vue";
import RecipeBooks from "./pages/recipe-books/RecipeBooks.vue";
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
    path: "/reported-urls",
    component: ReportedUrls,
    name: "reportedUrls",
  },
  {
    path: "/recipe-books",
    component: RecipeBooks,
    name: "recipeBooks"
  },
  {
    path: "/:catchAll(.*)",
    component: NotFound,
    props: true,
  },
] as RouteRecordRaw[];
