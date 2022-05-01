import { defineStore } from "pinia";
import { ParsedResponse } from "./models/parsed-response.model";

export const useRecipeStore = defineStore("recipeStore", {
  state: () => ({
    recipes: new Array<ParsedResponse>(),
    isLightMode: false,
  }),
  getters: {
    getRecipes(state) {
      return state.recipes;
    },
    getTheme(state) {
      return state.isLightMode;
    },
  },
  actions: {
    addRecipe(recipe: ParsedResponse) {
      this.recipes.push(recipe);
    },
    setTheme(isLightMode: boolean) {
      this.isLightMode = isLightMode;
    },
  },
});
