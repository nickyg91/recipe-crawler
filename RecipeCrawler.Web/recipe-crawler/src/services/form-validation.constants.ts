import { FormRules, FormItemRule } from "naive-ui";
const emailRegex =
  /^(([^<>()[\].,;:\s@"]+(\.[^<>()[\].,;:\s@"]+)*)|(".+"))@(([^<>()[\].,;:\s@"]+\.)+[^<>()[\].,;:\s@"]{2,})$/i;
const passwordRegex =
  /^\S*(?=\S{6,})(?=\S*\d)(?=\S*[A-Z])(?=\S*[a-z])(?=\S*[!@#$%^&*? ])\S*$/;

export const accountSignupFormRules: FormRules = {
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
      message: "Confirm Password is required.",
    },
  ],
};
