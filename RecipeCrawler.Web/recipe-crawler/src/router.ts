import { RouteRecordRaw } from "vue-router";
import RecipeCrawler from "./pages/recipe/RecipeCrawler.vue";
import SavedRecipe from "./pages/recipe/SavedRecipe.vue";
import NotFound from "./components/NotFound.vue";
import ReportedUrls from "./components/ReportedUrls.vue";
import EmailVerification from "./pages/account/EmailVerification.vue";
import CookBooks from "./pages/cook-books/CookbooksPage.vue";

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
    path: "/account/verify/:guid",
    component: EmailVerification,
    name: "verifyAccount",
    props: true,
  },
  {
    path: "/reported-urls",
    component: ReportedUrls,
    name: "reportedUrls",
  },
  {
    path: "/cook-books",
    component: CookBooks,
    name: "cookBooks",
  },
  {
    path: "/:catchAll(.*)",
    component: NotFound,
    props: true,
  },
] as RouteRecordRaw[];
