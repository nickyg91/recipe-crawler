import { Measurement } from "./measurement.enum";

export class Ingredient {
  id!: number;
  name!: string;
  measurement!: Measurement;
  recipeId!: number;
}
