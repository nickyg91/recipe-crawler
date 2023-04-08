import { ParsedResponse } from "../pages/recipe/models/parsed-response.model";
import { Cookbook } from "./shared/cookbook.model";
import { Recipe } from "./shared/recipe.model";
import { TokenResponse } from "./token-response.model";

export interface IRecipeStore {
  recipes: Array<ParsedResponse>;
  isLightMode: boolean;
  isMobile: boolean;
  userInfo: TokenResponse | null;
  cookbooks: Array<Cookbook>;
  currentlySelectedCookbookToEdit: Cookbook | null;
  originalCookbook: Cookbook | null;
  currentCookbook: Cookbook | null;
  currentCookbookRecipes: Recipe[] | null;
  currentRecipe: Recipe | null;
}
