using JavaScriptEngineSwitcher.ChakraCore;
using System;

namespace dotnet_ts_testing.Engines
{
    class ChakraJsEngine : JsEngine
    {
        private ChakraCoreJsEngine engine;

        protected override string Engine => "Chakra";

        protected override string Compile(string code) => engine.CallFunction("transform", code) as string;

        protected override void Prepare()
        {
            engine = new ChakraCoreJsEngine();
            engine.EmbedHostObject("log", new Action<object>(Console.WriteLine));
            engine.Execute("const global = this;");
            engine.Execute("const exports = {};");
            engine.Execute(Compiler);
            if (!engine.HasVariable("transform"))
                throw new InvalidOperationException("Missing compiler");
        }

        protected override void Dispose(bool disposing)
        {
            engine?.Dispose();
            base.Dispose(disposing);
        }
    }
}
