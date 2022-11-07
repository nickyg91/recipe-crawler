import { Claim } from "./claims.model";

export class TokenResponse {
  public token: string;
  public claims: Claim[];
  public refreshToken: string;

  constructor(token: string, claims: Claim[], refreshToken: string) {
    this.token = token;
    this.claims = claims;
    this.refreshToken = refreshToken;
  }
}
