import { Ingredient } from "./ingredient.model";
import { Step } from "./step.model";

export class Recipe {
  name!: string;
  crawledHtml?: string | null;
  cookbookId!: number;
  steps?: Step[] | null;
  ingredients?: Ingredient[] | null;
}
