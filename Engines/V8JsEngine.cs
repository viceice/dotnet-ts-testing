using Microsoft.ClearScript.V8;
using System;

namespace dotnet_ts_testing.Engines
{
    class V8JsEngine : JsEngine
    {
        private V8ScriptEngine _engine;
        private dynamic _compiler;

        protected override string Engine => "V8";


        // protected override bool NonWindows => false;

        protected override string Compile(string code) => _compiler(code);

        protected override void Prepare()
        {
            _engine = new V8ScriptEngine();
            _engine.AddHostObject("log", new Action<object>(Console.WriteLine));
            _engine.Execute("const window = this;");
            _engine.Execute("const exports = {};");
            _engine.Execute(Compiler);
            _compiler = _engine.Script.transform ?? throw new InvalidOperationException("Missing compiler");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _engine?.Dispose();
        }
    }
}
