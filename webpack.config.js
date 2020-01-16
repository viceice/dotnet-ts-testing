const path = require("path");

module.exports = [
  {
    entry: {
      // babel: "./scripts/babel.ts",
      tsc: "./scripts/tsc.ts",
    },
    output: {
      filename: "[name].js",
      path: path.resolve(__dirname, "scripts/"),
      library: "[name]",
      libraryTarget: "umd",
    },
    mode: "development",
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
              presets: [["@babel/preset-env", {}], "@babel/preset-typescript"],
            },
          },
        },
      ],
    },
  },
  // {
  // 	entry: {
  // 		babel: './Resources/babel.ts',
  // 	},
  // 	output: {
  // 		filename: '[name].min.js',
  // 		path: path.resolve(__dirname, 'Resources/'),
  // 		libraryTarget: 'commonjs',
  // 	},
  // 	mode: 'production',
  // 	node: {
  // 		fs: 'empty',
  // 	},
  // 	performance: {
  // 		hints: false,
  //     },
  //     module: {
  //         rules: [
  //           {
  //             test: /\.tsx?$/,
  //             loader: 'babel-loader',
  //           },
  //         ],
  //       },
  // },
];
