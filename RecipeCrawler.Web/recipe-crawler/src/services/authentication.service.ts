import { AxiosResponse } from "axios";
import { Account } from "../models/account.model";
import { Login } from "../models/login.model";
import { TokenResponse } from "../models/token-response.model";
import axiosInstance from "./axios-instance.model";
const baseUrl = "api/account";
export class AuthenticationService {
  static injectionKey = "authenticationService";

  public async login(loginModel: Login): Promise<TokenResponse> {
    return (
      await axiosInstance.post<TokenResponse, AxiosResponse<TokenResponse>>(
        `${baseUrl}/login`,
        loginModel
      )
    ).data;
  }

  public async createAccount(model: Account): Promise<void> {
    return await axiosInstance.post(`${baseUrl}/create`, model);
  }

  public async verifyAccount(guid: string): Promise<void> {
    return await axiosInstance.get(`${baseUrl}/${guid}/verify`);
  }

  public async resendVerificationEmail(guid: string): Promise<void> {
    return await axiosInstance.get(`${baseUrl}/${guid}/resend/verify`);
  }
}
