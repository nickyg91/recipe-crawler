import { Ingredient } from "./ingredient.model";

export class Step {
  constructor(
    id: number,
    description: string,
    name: string,
    recipeId: number,
    ingredients: Ingredient[] | null
  ) {
    this.id = id;
    this.description = description;
    this.name = name;
    this.recipeId = recipeId;
    this.ingredients = ingredients;
  }

  id!: number;
  description!: string;
  name!: string;
  recipeId!: number;
  ingredients?: Ingredient[] | null;
}
