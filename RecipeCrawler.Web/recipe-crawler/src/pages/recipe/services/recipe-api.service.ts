import { Recipe } from "../../../models/shared/recipe.model";
import { axiosInstance } from "../../../services/axios-instance.model";

const baseUrl = "api/cookbook";
export class RecipeService {
  static injectionKey = "recipeService";

  public async getRecipesForCookbook(cookbookId: number): Promise<Recipe[]> {
    return (await axiosInstance.get(`${baseUrl}/${cookbookId}/recipes`)).data;
  }

  public async getRecipeById(
    cookbookId: number,
    recipeId: number
  ): Promise<Recipe> {
    return (
      await axiosInstance.get(`${baseUrl}/${cookbookId}/recipe/${recipeId}`)
    ).data;
  }

  public async saveRecipe(recipe: Recipe): Promise<Recipe> {
    if (recipe.id > 0) {
      return (
        await axiosInstance.put(
          `${baseUrl}/${recipe.cookbookId}/recipe/${recipe.id}`,
          recipe
        )
      ).data;
    } else {
      return (
        await axiosInstance.post(
          `${baseUrl}/${recipe.cookbookId}/recipe`,
          recipe
        )
      ).data;
    }
  }

  public async deleteRecipe(
    cookbookId: number,
    recipeId: number
  ): Promise<boolean> {
    return await axiosInstance.delete(
      `${baseUrl}/${cookbookId}/recipe/${recipeId}`
    );
  }
}
