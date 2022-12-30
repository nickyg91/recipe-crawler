import axios from "axios";
import { Account } from "../models/account.model";
import { Login } from "../models/login.model";
import { TokenResponse } from "../models/token-response.model";
const baseUrl = "api/account";
export class AuthenticationService {
  static injectionKey = "authenticationService";
  public async login(loginModel: Login): Promise<TokenResponse> {
    return axios.post(`${baseUrl}/login`, loginModel);
  }

  public async createAccount(model: Account): Promise<void> {
    return axios.post(`${baseUrl}/create`, model);
  }
}
