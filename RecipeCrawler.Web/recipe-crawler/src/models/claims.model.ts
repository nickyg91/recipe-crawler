export class Claim {
  subject: string;
  type: string;
  value: string;

  constructor(subject: string, type: string, value: string) {
    this.subject = subject;
    this.type = type;
    this.value = value;
  }
}
