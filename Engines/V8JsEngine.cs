using Microsoft.ClearScript.V8;
using System;

namespace dotnet_ts_testing.Engines
{
    class V8JsEngine : JsEngine
    {
        protected override string Engine => "V8";

        protected override string Compile(string code)
        {
            using (var engine = new V8ScriptEngine())
            {
                engine.AddHostObject("log", new Action<object>(Console.WriteLine));
                engine.Execute("const window = this;");
                engine.Execute("const exports = {};");

                engine.Execute(Compiler);

                return engine.Script.transform(code);
            }
        }
    }
}
