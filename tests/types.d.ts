declare module "console" {
  class Console {
    static log(arg: any): void;
  }
  export = Console;
}

declare function log(arg: any): void;
