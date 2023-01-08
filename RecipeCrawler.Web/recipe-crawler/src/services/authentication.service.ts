import axios, { AxiosResponse } from "axios";
import { Account } from "../models/account.model";
import { Login } from "../models/login.model";
import { TokenResponse } from "../models/token-response.model";
const baseUrl = "api/account";
export class AuthenticationService {
  static injectionKey = "authenticationService";
  public login(loginModel: Login): Promise<AxiosResponse<TokenResponse>> {
    return axios.post(`${baseUrl}/login`, loginModel);
  }

  public async createAccount(model: Account): Promise<void> {
    return await axios.post(`${baseUrl}/create`, model);
  }
}
