using Jurassic;
using Jurassic.Library;
using System;

namespace dotnet_ts_testing.Engines
{
    class JurassicJsEngine : JsEngine
    {
        protected override string Engine => "Jurassic";


        protected override string Compile(string code)
        {
            var engine = new ScriptEngine();
            
            engine.SetGlobalFunction("log", new Action<object>(Console.WriteLine));
            engine.Global["window"] = engine.Global;

            engine.Execute(Compiler);

            var compiler = engine.Global[Type] as ObjectInstance;

            return compiler.CallMemberFunction("transform", code) as string;
        }
    }
}
