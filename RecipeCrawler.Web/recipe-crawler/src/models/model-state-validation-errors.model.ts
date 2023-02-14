export class ModelStateErrors {
  title!: string;
  errors!: {
    [key: string]: Array<string>;
  };
}
