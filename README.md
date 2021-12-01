# dotnet-ts-testing

This project compares the typescript compilation on different dotnet javascript engines

* [Jint](https://github.com/sebastienros/jint)
* [Jurassic](https://github.com/paulbartrum/jurassic)
* [ClearScript V8](https://github.com/Microsoft/ClearScript)
* [Chakra](https://github.com/chakra-core/ChakraCore) via [JavaScriptEngineSwitcher](https://github.com/Taritsyn/JavaScriptEngineSwitcher)
* [NiL.JS](https://github.com/nilproject/NiL.JS)


Usage:

```sh
yarn install
yarn test
```

Run minimized js code:

```sh
yarn install
yarn test -m
```
