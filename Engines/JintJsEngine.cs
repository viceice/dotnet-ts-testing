using Jint;
using System;

namespace dotnet_ts_testing.Engines
{
    class JintJsEngine : JsEngine
    {
        protected override string Engine => "Jint";


        protected override string Compile(string code)
        {
            var engine = new Engine(c => c.DebugMode(false))
                            .SetValue("log", new Action<object>(Console.WriteLine));
            engine.Global.FastAddProperty("window", engine.Global, false, false, false);

            engine.Execute(Compiler);

            var compiler = engine.Global.Get(Type).AsObject();
            var res = compiler.Get("transform");
            return res.Invoke(code).AsString();
        }
    }
}
