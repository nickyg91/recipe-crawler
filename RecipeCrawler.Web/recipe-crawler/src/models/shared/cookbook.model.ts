export class Cookbook {
  constructor(id: number, name: string, coverImageBase64: string | null) {
    this.id = id;
    this.name = name;
    this.coverImageBase64 = coverImageBase64;
  }
  id!: number;
  name!: string;
  coverImageBase64?: string | null;
}
