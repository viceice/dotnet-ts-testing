using Jurassic;
using Jurassic.Library;
using System;

namespace dotnet_ts_testing.Engines
{
    class JurassicJsEngine : JsEngine
    {
        private ScriptEngine _engine;
        private FunctionInstance _compiler;

        protected override string Engine => "Jurassic";

        protected override string Compile(string code) => _compiler.Call(_engine.Global, code)?.ToString();

        protected override void Prepare()
        {
            _engine = new ScriptEngine
            {
                ForceStrictMode = true,
            };

            _engine.SetGlobalFunction("log", new Action<object>(Console.WriteLine));
            _engine.Global["window"] = _engine.Global;
            _engine.Execute("var exports = {};");

            _engine.Execute(Compiler);

            _compiler = _engine.Global["transform"] as FunctionInstance ?? throw new InvalidOperationException("Missing compiler");
        }
    }
}
