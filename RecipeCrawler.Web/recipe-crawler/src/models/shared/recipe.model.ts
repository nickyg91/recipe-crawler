import { Step } from "./step.model";

export class Recipe {
  constructor() {
    this.steps = new Array<Step>();
  }
  id!: number;
  name!: string;
  crawledHtml?: string | null;
  cookbookId!: number;
  steps!: Step[] | null;
}
