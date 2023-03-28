import { Step } from "./step.model";

export class Recipe {
  id!: number;
  name!: string;
  crawledHtml?: string | null;
  cookbookId!: number;
  steps?: Step[] | null;
}
