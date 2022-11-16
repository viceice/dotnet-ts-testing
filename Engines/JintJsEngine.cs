using Jint;
using Jint.Native;
using Jint.Runtime.Descriptors;
using System;

namespace dotnet_ts_testing.Engines
{
    class JintJsEngine : JsEngine
    {
        private Engine _engine;
        private JsValue _compiler;

        protected override string Engine => "Jint";

        protected override string Compile(string code) => _engine.Invoke(_compiler, code).AsString();

        protected override void Prepare()
        {
            _engine = new Engine(SetOptions)
                            .SetValue("log", new Action<object>(Console.WriteLine));
            _engine.Execute("const global = this;");
            _engine.Execute("const exports = {};");

            _engine.Execute(Compiler);

            _compiler = _engine.Evaluate("exports.transform") ?? throw new InvalidOperationException("Missing compiler");
        }

        private void SetOptions(Options opt)
        {
            opt.DebugMode(false).TimeoutInterval(TimeSpan.FromMinutes(5));
        }
    }
}
