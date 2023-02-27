import { Cookbook } from "../../../models/shared/cookbook.model";
import axiosInstance from "../../../services/axios-instance.model";

const baseUrl = "api/chef/cookbook";
export class CookbookService {
  static injectionKey = "cookbookService";

  public getCookbooksForChef(): Promise<Cookbook[]> {
    return axiosInstance.get(`${baseUrl}`);
  }

  public saveCookbook(cookbook: Cookbook): Promise<Cookbook> {
    return axiosInstance.post(`${baseUrl}`, cookbook);
  }
}
