import { Measurement } from "./measurement.enum";

export class Ingredient {
  name!: string;
  measurement!: Measurement;
  recipeId!: number;
}
