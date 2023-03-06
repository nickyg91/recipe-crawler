import { defineStore } from "pinia";
import { ParsedResponse } from "./pages/recipe/models/parsed-response.model";
import { IRecipeStore } from "./models/recipe-store.interface";
import { TokenResponse } from "./models/token-response.model";
import axiosInstance from "./services/axios-instance.model";
import { Cookbook } from "./models/shared/cookbook.model";
import { CookbookService } from "./pages/cook-books/services/cookbook.service";
import { ChefferWindow } from "./models/window.extension";
const cookbookService = new CookbookService();
export const useRecipeStore = defineStore("recipeStore", {
  state: () =>
    ({
      recipes: new Array<ParsedResponse>(),
      isLightMode: false,
      isMobile: false,
      userInfo: null,
      cookbooks: new Array<Cookbook>(),
      currentlySelectedCookbookToEdit: null,
      originalCookbook: null,
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
    getCurrentlyEditedCookbook(state): Cookbook | null {
      return state.currentlySelectedCookbookToEdit;
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
          instance.headers.Authorization = `Bearer ${response.token}`;
        }
        return instance;
      });
    },
    async setCookbooks() {
      if (this.cookbooks.length < 1) {
        try {
          this.cookbooks = await cookbookService.getCookbooksForChef();
        } catch (error) {
          (window as ChefferWindow).$message?.error(
            "An error occurred while retrieving your cook books!"
          );
        }
      }
    },
    addCookbook(cookbook: Cookbook) {
      this.cookbooks.push(cookbook);
    },
    async deleteCookbook(id: number) {
      try {
        const isDeleted = await cookbookService.deleteCookbook(id);
        if (isDeleted) {
          const cookbookToRemoveIndex = this.cookbooks.findIndex(
            (x) => x.id === id
          );
          this.cookbooks.splice(cookbookToRemoveIndex, 1);
          (window as ChefferWindow).$message?.success("Cookbook deleted!");
        }
      } catch (error) {
        (window as ChefferWindow).$message?.error(
          "An error occurred while deleting the cookbook!"
        );
      }
    },
    setCurrentlyEditingCookbook(id: number) {
      const index = this.cookbooks.findIndex((x) => x.id === id);
      this.currentlySelectedCookbookToEdit = this.cookbooks[index];
      this.originalCookbook = new Cookbook(
        this.currentlySelectedCookbookToEdit.id,
        this.currentlySelectedCookbookToEdit.name,
        this.currentlySelectedCookbookToEdit.coverImageBase64 ?? null
      );
    },
    removeEditedChanges() {
      const index = this.cookbooks.findIndex(
        (x) => x.id === this.originalCookbook?.id
      );
      if (this.originalCookbook && this.currentlySelectedCookbookToEdit) {
        this.cookbooks[index] = this.originalCookbook;
      }
      this.originalCookbook = null;
      this.currentlySelectedCookbookToEdit = null;
    },
  },
});
