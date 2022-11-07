export class Account {
  public email: string;
  public username: string;
  public password: string;
  public confirmPassword: string;

  constructor(
    email: string,
    username: string,
    password: string,
    confirmPassword: string
  ) {
    this.email = email;
    this.username = username;
    this.password = password;
    this.confirmPassword = confirmPassword;
  }
}
