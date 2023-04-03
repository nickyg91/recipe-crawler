import { Measurement } from "./measurement.enum";

export class Ingredient {
  id!: number | undefined;
  name!: string | undefined;
  measurement!: Measurement | undefined;
  amount!: number | undefined;
  stepId!: number | undefined;
}
