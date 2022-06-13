import { defineStore } from "pinia";
import { ParsedResponse } from "./models/parsed-response.model";

export const useRecipeStore = defineStore("recipeStore", {
  state: () => ({
    recipes: new Array<ParsedResponse>(),
    isLightMode: false,
    isMobile: false,
  }),
  getters: {
    getRecipes(state) {
      return state.recipes;
    },
    getTheme(state) {
      return state.isLightMode;
    },
    getIsMobile(state) {
      return state.isMobile;
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
  },
});
