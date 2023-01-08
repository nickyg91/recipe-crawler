import { Claim } from "./claims.model";

export class TokenResponse {
  public token: string;
  public claims: Claim[];
  public refreshToken: string;

  get chefId(): number | undefined {
    return Number(this.claims?.find((x) => x.type === "chefId")?.value);
  }

  get username(): string | undefined {
    return this.claims?.find((x) => x.type === "username")?.value;
  }

  constructor(token: string, claims: Claim[], refreshToken: string) {
    this.token = token;
    this.claims = claims;
    this.refreshToken = refreshToken;
  }
}
