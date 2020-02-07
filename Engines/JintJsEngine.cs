using Jint;
using Jint.Native;
using System;

namespace dotnet_ts_testing.Engines
{
    class JintJsEngine : JsEngine
    {
        private Engine _engine;
        private JsValue _compiler;

        protected override string Engine => "Jint";


        protected override string Compile(string code) => _compiler.Invoke(code).AsString();

        protected override void Prepare()
        {
            _engine = new Engine(c => c.DebugMode(false))
                            .SetValue("log", new Action<object>(Console.WriteLine));
            _engine.Global.FastAddProperty("window", _engine.Global, false, false, false);
            _engine.Global.FastAddProperty("exports", _engine.Global, false, false, false);

            _engine.Execute(Compiler);

            _compiler = _engine.Global.Get("transform") ?? throw new InvalidOperationException("Missing compiler");
        }
    }
}
