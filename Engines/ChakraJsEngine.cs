using JavaScriptEngineSwitcher.ChakraCore;
using System;

namespace dotnet_ts_testing.Engines
{
    class ChakraJsEngine : JsEngine
    {
        protected override string Engine => "Chakra";

        protected override string Compile(string code)
        {
            using (var engine = new ChakraCoreJsEngine())
            {
                engine.EmbedHostObject("log", new Action<object>(Console.WriteLine));
                engine.Execute("const window = this;");

                engine.Execute(Compiler);

                engine.Evaluate($"function transform(code){{ return {Type}.transform(code); }}");

                return engine.CallFunction<string>("transform", code);
            }
        }
    }
}
