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
                engine.Execute("const exports = {};");

                engine.Execute(Compiler);

                var res = engine.CallFunction("transform", code);
                if (res is string s)
                    return s;
                throw new InvalidOperationException("Wrong result: " + res?.GetType());
            }
        }
    }
}
