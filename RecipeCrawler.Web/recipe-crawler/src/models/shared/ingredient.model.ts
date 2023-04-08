import { Measurement } from "./measurement.enum";

export class Ingredient {
  id!: number;
  name!: string;
  measurement!: Measurement;
  amount!: number;
  stepId!: number;
  recipeId!: number;
}
