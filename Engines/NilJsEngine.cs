using NiL.JS.BaseLibrary;
using NiL.JS.Core;
using NiL.JS.Extensions;
using System;

namespace dotnet_ts_testing.Engines
{
    class NilJsEngine : JsEngine
    {
        protected override string Engine => "NiL.JS";


        protected override string Compile(string code)
        {
            var engine = new Context
            {
                { "exports", JSObject.CreateObject() },
                { "log", JSValue.Marshal(new Action<object>(Console.WriteLine)) }
            };

            engine.Eval(Compiler, false);

            var compiler = engine.Eval("exports.transform").As<Function>();

            var res = compiler?.Call(new Arguments { code });

            if (!res.Is<string>())
                throw new InvalidOperationException("Wrong result: " + res?.GetType()); 

            return res.As<string>();
        }
    }
}
