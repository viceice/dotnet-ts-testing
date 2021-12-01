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

        protected override string Compile(string code) => _engine.Invoke(_compiler, code).AsString();

        protected override void Prepare()
        {
            _engine = new Engine(SetOptions)
                            .SetValue("log", new Action<object>(Console.WriteLine));
            _engine.Realm.GlobalObject.FastAddProperty("window", _engine.Realm.GlobalObject, false, false, false);
            _engine.Realm.GlobalObject.FastAddProperty("exports", _engine.Realm.GlobalObject, false, false, false);

            _engine.Execute(Compiler);

            _compiler = _engine.Realm.GlobalObject.Get("transform") ?? throw new InvalidOperationException("Missing compiler");
        }

        private void SetOptions(Options opt)
        {
            opt.DebugMode(false).TimeoutInterval(TimeSpan.FromMinutes(5));
        }
    }
}
