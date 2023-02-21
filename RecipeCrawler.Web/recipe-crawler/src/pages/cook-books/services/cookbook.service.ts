import { Cookbook } from "../../../models/shared/cookbook.model";
import axiosInstance from "../../../services/axios-instance.model";

const baseUrl = "api/chef/cookbook";
export class CookbookService {
  static injectionKey = "cookbookService";

  public async getCookbooksForChef(): Promise<Cookbook> {
    return axiosInstance.get(`${baseUrl}`);
  }
}
