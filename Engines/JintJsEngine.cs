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
            _engine.Realm.GlobalObject.FastAddProperty("window", _engine.Realm.GlobalObject, false, false, false);
            _engine.Realm.GlobalObject.FastAddProperty("exports", _engine.Realm.GlobalObject, false, false, false);

            _engine.Execute(Compiler);

            _compiler = _engine.Realm.GlobalObject.Get("transform") ?? throw new InvalidOperationException("Missing compiler");
        }
    }
}
