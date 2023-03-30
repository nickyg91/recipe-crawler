import { Claim } from "./claims.model";

export class TokenResponse {
  public token: string;
  public claims: Claim[];
  public refreshToken: string;

  get chefId(): number | null {
    return Number(this.claims?.find((x) => x.type === "chefId")?.value);
  }

  get username(): string | null {
    return this.claims?.find((x) => x.type === "username")?.value ?? null;
  }

  constructor(token: string, claims: Claim[], refreshToken: string) {
    this.token = token;
    this.claims = claims;
    this.refreshToken = refreshToken;
  }
}
