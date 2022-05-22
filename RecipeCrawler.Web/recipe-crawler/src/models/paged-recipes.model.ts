import { ParsedResponse } from "./parsed-response.model";

export class PagedRecipes {
  recipes!: ParsedResponse[];
  totalItems: number = 0;
}
