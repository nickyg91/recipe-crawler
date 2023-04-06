import { Ingredient } from "./ingredient.model";
import { Step } from "./step.model";

export class Recipe {
  constructor() {
    this.ingredients = new Array<Ingredient>();
    this.steps = new Array<Step>();
  }
  id!: number;
  name!: string;
  crawledHtml?: string | null;
  cookbookId!: number;
  steps!: Step[] | null;
  ingredients!: Ingredient[] | null;
}
