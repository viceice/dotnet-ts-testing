using NiL.JS.Core;
using NiL.JS.Extensions;
using System;

namespace dotnet_ts_testing.Engines
{
    class NilJsEngine : JsEngine
    {
        private Context _engine;
        private ICallable _compiler;

        protected override string Engine => "NiL.JS";


        protected override string Compile(string code) => _compiler.Call(JSValue.Undefined, new Arguments { code }).As<string>();

        protected override void Prepare()
        {
            _engine = new Context
            {
                { "log", JSValue.Marshal(new Action<object>(Console.WriteLine)) },
            };

            _engine.Eval("const window = this;", true);
            _engine.Eval("const global = this;", true);
            _engine.Eval("const exports = {};", true);
            _engine.Eval(Compiler, false);
            _compiler = _engine.Eval("exports.transform").As<ICallable>() ?? throw new InvalidOperationException("Missing compiler");
        }
    }
}
