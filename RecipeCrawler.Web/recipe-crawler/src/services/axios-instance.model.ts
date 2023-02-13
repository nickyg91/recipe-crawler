import axios, { AxiosError } from "axios";
import { ModelStateErrors } from "../models/model-state-validation-errors.model";
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

function determine400ErrorMessage(data: ModelStateErrors): string {
  let invalidFields = "";
  for (const [key] of Object.entries(data.errors)) {
    let errorStr = `${key}: `;
    debugger;
    const errorMessages = data.errors[key];
    errorMessages?.forEach((val) => {
      errorStr += `${val}\n`;
    });
    invalidFields += errorStr;
  }
  return invalidFields;
}

axiosInstance.interceptors.response.use(
  (response) => {
    return response;
  },
  (error: AxiosError) => {
    if (error.response?.status === 500) {
      (window as ChefferWindow).$message?.error(
        determineErrorMessage(error?.response?.data?.toString() ?? "")
      );
    }
    if (error.response?.status === 400) {
      const modelData = error.response?.data as ModelStateErrors;
      const errorMessage = determine400ErrorMessage(modelData);
      (window as ChefferWindow)?.$message?.error(errorMessage, {
        closable: true,
        keepAliveOnHover: true,
      });
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;
