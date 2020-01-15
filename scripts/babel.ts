import { transform as babelTransform, registerPreset } from "@babel/standalone";
// import * as presetTs from "@babel/preset-typescript";


// registerPreset(presetTs);

export function transform(
  input: string,
  babelConfig: object,
  filename: string
) {
  babelConfig = {
    ...babelConfig,
    ast: false,
    filename,
    // presets: ["@babel/preset-typescript"]
  };
  try {
    return babelTransform(input, babelConfig);
    // return input;
  } catch (ex) {
    // Parsing stack is extremely long and not very useful, so just rethrow the message.
    throw new Error(ex.message);
  }
}
