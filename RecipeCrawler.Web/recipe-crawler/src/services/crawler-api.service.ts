import axios, { AxiosResponse } from "axios";
import { PagedRecipes } from "../pages/recipe/models/paged-recipes.model";
import { ParsedResponse } from "../pages/recipe/models/parsed-response.model";
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

  public reportUrl(recipe: ParsedResponse): Promise<AxiosResponse<boolean>> {
    return axios.post<boolean>("/api/features/crawler/report", recipe);
  }

  public getReportedUrls(
    page: number,
    pageSize: number
  ): Promise<AxiosResponse<PagedRecipes>> {
    return axios.get<PagedRecipes>(
      `/api/features/crawler/recipes/reported/${page}/page/${pageSize}`
    );
  }
}
