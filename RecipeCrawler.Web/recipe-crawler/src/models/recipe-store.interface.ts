import { ParsedResponse } from "./parsed-response.model";
import { TokenResponse } from "./token-response.model";

export interface IRecipeStore {
  recipes: Array<ParsedResponse>;
  isLightMode: boolean;
  isMobile: boolean;
  userInfo: TokenResponse | null;
}
