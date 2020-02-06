import typescript from "@rollup/plugin-typescript";
import commonjs from "@rollup/plugin-commonjs";
import resolve from "@rollup/plugin-node-resolve";

export default {
  input: "scripts/tsc.ts",
  output: [
    {
      file: "scripts/esm/tsc.js",
      format: "esm",
    },
    {
      file: "scripts/tsc.js",
      format: "cjs",
      exports: 'named'
    },
  ],
  plugins: [
    resolve({ browser: true }),
    typescript({
      tsconfig: "tsconfig.scripts.json",
    }),
    commonjs(),
  ],
};
