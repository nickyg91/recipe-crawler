import { Measurement } from "./measurement.enum";

export class Ingredient {
  keyId!: number;
  id!: number;
  name!: string;
  measurement!: Measurement;
  amount!: number;
  stepId!: number;
  recipeId!: number;
}
