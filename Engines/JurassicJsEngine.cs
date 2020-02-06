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
            engine.Execute("var exports = {};");

            engine.Execute(Compiler);

            var compiler = engine.Global;

            object res = compiler.CallMemberFunction("transform", code);
            if (res is string s)
                return s;
            else if(res is ConcatenatedString cs)
                    return cs.ToString();
            throw new InvalidOperationException("Wrong result: " + res?.GetType());
        }
    }
}
