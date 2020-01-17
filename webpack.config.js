const path = require("path");
const merge = require("webpack-merge");

function base(args) {
  return merge(
    {
      entry: {
        // babel: "./scripts/babel.ts",
        tsc: "./scripts/tsc.ts",
      },
      output: {
        filename: "[name].js",
        path: path.resolve(__dirname, "scripts/"),
        library: "[name]",
        libraryTarget: "var",
      },
      devtool: "none",
      node: {
        fs: "empty",
      },
      performance: {
        hints: false,
      },
      module: {
        rules: [
          {
            test: /\.tsx?$/,
            use: {
              loader: "babel-loader",
              options: {
                presets: [
                  ["@babel/preset-env", {}],
                  "@babel/preset-typescript",
                ],
              },
            },
          },
        ],
      },
    },
    args
  );
}

module.exports = [
  base({
    mode: "development",
  }),
  base({
    mode: "production",
    output: {
      filename: "[name].min.js",
    },
  }),
];
