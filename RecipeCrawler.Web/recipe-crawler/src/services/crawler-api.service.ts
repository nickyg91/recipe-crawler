import axios, { AxiosResponse } from "axios";
import { ParsedResponse } from "../models/parsed-response.model";
export const injectionKey = "crawlerApi";
export class CrawlerApi {
  private readonly baseUrl = "/api/features/crawler";

  public crawlUrl(url: string): Promise<AxiosResponse<ParsedResponse>> {
    return axios.post<ParsedResponse>("/api/features/crawler", {
      url: url,
    });
  }

  public saveRecipe(recipe: ParsedResponse): Promise<AxiosResponse<string>> {
    return axios.post<string>("/api/features/crawler/save", recipe);
  }

  public getRecipe(url: string): Promise<AxiosResponse<ParsedResponse>> {
    return axios.get<ParsedResponse>(`/api/features/crawler/${url}`);
  }
}
