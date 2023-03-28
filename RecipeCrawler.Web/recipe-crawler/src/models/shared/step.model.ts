import { Ingredient } from "./ingredient.model";

export class Step {
  id!: number;
  description!: string;
  name!: string;
  recipeId!: number;
  ingredients?: Ingredient[] | null;
}
