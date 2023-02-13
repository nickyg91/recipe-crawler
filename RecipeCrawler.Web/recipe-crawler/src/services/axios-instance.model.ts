import axios, { AxiosError } from "axios";
import { ChefferWindow } from "../models/window.extension";

const axiosInstance = axios.create({
  baseURL: "/",
});

function determineErrorMessage(trace: string): string {
  if (trace.length < 1) {
    return "An error occurred";
  }
  return trace.split("\n")[0].split(":")[1];
}

axiosInstance.interceptors.response.use(
  (response) => {
    return response;
  },
  (error: AxiosError) => {
    (window as ChefferWindow).$message?.error(
      determineErrorMessage(error?.response?.data?.toString() ?? "")
    );
    return Promise.reject(error);
  }
);

export default axiosInstance;
