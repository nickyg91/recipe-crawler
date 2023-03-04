import { Cookbook } from "../../../models/shared/cookbook.model";
import axiosInstance from "../../../services/axios-instance.model";

const baseUrl = "api/chef/cookbook";
export class CookbookService {
  static injectionKey = "cookbookService";

  public async getCookbooksForChef(): Promise<Cookbook[]> {
    return (await (axiosInstance.get(`${baseUrl}`))).data;
  }

  public async saveCookbook(cookbook: Cookbook): Promise<Cookbook> {
    return (await axiosInstance.post(`${baseUrl}`, cookbook)).data;
  }
}
