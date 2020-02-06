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
            engine.Global.FastAddProperty("exports", engine.Global, false, false, false);

            engine.Execute(Compiler);

            var res = engine.Global.Get("transform").Invoke(code);
            if (res.IsString())
                return res.AsString();


            throw new InvalidOperationException("Wrong result: " + res.AsObject()?.GetType());
        }
    }
}
