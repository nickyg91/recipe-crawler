import { FormRules, FormItemRule } from "naive-ui";
import { Account } from "../models/account.model";
const emailRegex =
  /^(([^<>()[\].,;:\s@"]+(\.[^<>()[\].,;:\s@"]+)*)|(".+"))@(([^<>()[\].,;:\s@"]+\.)+[^<>()[\].,;:\s@"]{2,})$/i;
const passwordRegex =
  /^\S*(?=\S{6,})(?=\S*\d)(?=\S*[A-Z])(?=\S*[a-z])(?=\S*[!@#$%^&*? ])\S*$/;

export function accountSignupFormRules(formModel: Account): FormRules {
  return {
    email: [
      {
        required: true,
        message: "Email is required.",
        validator(rule: FormItemRule, value: string) {
          const result = emailRegex.test(value);
          if (result) {
            return result;
          }
          return new Error("Invalid Email.");
        },
        trigger: ["input", "blur"],
      },
    ],
    username: [
      {
        required: true,
        message: "Username is required.",
        min: 8,
        max: 256,
        trigger: ["input", "blur"],
      },
    ],
    password: [
      {
        required: true,
        message: "Password is required.",
        trigger: ["blur"],
        validator(rule: FormItemRule, value: string) {
          const isValidPassword = passwordRegex.test(value);
          if (isValidPassword) {
            return isValidPassword;
          }
          return new Error(
            "Password must be at least 6 characters and contain one letter, number, and special symbol."
          );
        },
      },
    ],
    confirmPassword: [
      {
        required: true,
        validator(rule: FormItemRule, value: string) {
          if (value === formModel.password) {
            return true;
          } else {
            return new Error("Password and Confirm Password must be the same.");
          }
        },
        message: "Confirm Password must be the same as Password.",
        trigger: ["blur", "password-input"],
      },
    ],
  };
}
