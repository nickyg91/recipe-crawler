import { defineStore } from "pinia";
import { ParsedResponse } from "./pages/recipe/models/parsed-response.model";
import { IRecipeStore } from "./models/recipe-store.interface";
import { TokenResponse } from "./models/token-response.model";
import axiosInstance from "./services/axios-instance.model";
import { Cookbook } from "./models/shared/cookbook.model";
export const useRecipeStore = defineStore("recipeStore", {
  state: () =>
    ({
      recipes: new Array<ParsedResponse>(),
      isLightMode: false,
      isMobile: false,
      userInfo: null,
      cookbooks: new Array<Cookbook>(),
    } as IRecipeStore),
  getters: {
    getRecipes(state) {
      return state.recipes;
    },
    getIsLightMode(state) {
      return state.isLightMode;
    },
    getIsMobile(state) {
      return state.isMobile;
    },
    getUserInfo(state): TokenResponse | null {
      return state.userInfo;
    },
    getChefCookbooks(state): Array<Cookbook> {
      return state.cookbooks;
    },
  },
  actions: {
    addRecipe(recipe: ParsedResponse) {
      this.recipes.push(recipe);
    },
    setTheme(isLightMode: boolean) {
      this.isLightMode = isLightMode;
    },
    setIsMobile(isMobile: boolean) {
      this.isMobile = isMobile;
    },
    setUserInfo(response: TokenResponse) {
      this.userInfo = response;
      axiosInstance?.interceptors.request.use((instance) => {
        if (instance) {
          // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
          instance.headers!.Authorization = `Authorization: ${response.token}`;
        }
        return instance;
      });
    },
    // async setCookbooks() {

    // }
  },
});
