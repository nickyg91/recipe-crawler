import { ParsedResponse } from "./parsed-response.model";

export class PagedRecipes {
  recipes!: ParsedResponse[];
  totalItems = 0;
}
