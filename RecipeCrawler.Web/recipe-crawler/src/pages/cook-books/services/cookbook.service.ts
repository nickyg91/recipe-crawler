import { Cookbook } from "../../../models/shared/cookbook.model";
import axiosInstance from "../../../services/axios-instance.model";

const baseUrl = "api/chef/cookbook";
export class CookbookService {
  static injectionKey = "cookbookService";

  public async getCookbooksForChef(): Promise<Cookbook[]> {
    return (await axiosInstance.get(`${baseUrl}`)).data;
  }

  public async saveCookbook(cookbook: Cookbook): Promise<Cookbook> {
    if (cookbook.id > 0) {
      await axiosInstance.put(`${baseUrl}/${cookbook.id}`, cookbook);
      return cookbook;
    } else {
      return (await axiosInstance.post(`${baseUrl}`, cookbook)).data;
    }
  }

  public async deleteCookbook(id: number): Promise<boolean> {
    return (await axiosInstance.delete(`${baseUrl}/${id}`)).data;
  }
}
