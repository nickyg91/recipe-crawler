import { defineStore } from "pinia";
import { ParsedResponse } from "./models/parsed-response.model";
import { IRecipeStore } from "./models/recipe-store.interface";
import { TokenResponse } from "./models/token-response.model";
import { axiosInstance } from "./main";
export const useRecipeStore = defineStore("recipeStore", {
  state: () =>
    ({
      recipes: new Array<ParsedResponse>(),
      isLightMode: false,
      isMobile: false,
      userInfo: null,
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
  },
});
