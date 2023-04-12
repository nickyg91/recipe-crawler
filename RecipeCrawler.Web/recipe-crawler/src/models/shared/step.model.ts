import { Ingredient } from "./ingredient.model";

export class Step {
  constructor(
    id: number,
    description: string,
    name: string,
    recipeId: number,
    ingredients: Ingredient[],
    order: number
  ) {
    this.id = id;
    this.description = description;
    this.name = name;
    this.recipeId = recipeId;
    this.ingredients = ingredients;
    this.order = order;
  }

  id!: number;
  description!: string;
  name!: string;
  recipeId!: number;
  ingredients: Ingredient[];
  order!: number;
}
