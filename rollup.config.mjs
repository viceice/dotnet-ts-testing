import typescript from '@rollup/plugin-typescript';
import commonjs from '@rollup/plugin-commonjs';
import { nodeResolve } from '@rollup/plugin-node-resolve';
import { terser } from 'rollup-plugin-terser';
import nodePolyfills from 'rollup-plugin-polyfill-node';

const base = {
  input: 'scripts/tsc.ts',
  plugins: [
    nodeResolve({ browser: true, preferBuiltins: false }),
    commonjs(),
    nodePolyfills({ include: ['perf-hooks'] }),
    typescript({
      tsconfig: 'tsconfig.cjs.json',
    }),
  ],
};

const esm = {
  file: 'scripts/tsc.esm.js',
  format: 'esm',
};
const cjs = {
  file: 'scripts/tsc.js',
  format: 'cjs',
  exports: 'named',
};

export default [
  {
    ...base,
    output: [
      cjs,
      {
        ...cjs,
        file: 'scripts/tsc.min.js',
        plugins: [terser()],
      },
      esm,
      {
        ...esm,
        file: 'scripts/tsc.esm.min.js',
        plugins: [terser()],
      },
    ],
  },
];
