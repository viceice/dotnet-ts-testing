using NiL.JS.BaseLibrary;
using NiL.JS.Core;
using NiL.JS.Extensions;
using System;

namespace dotnet_ts_testing.Engines
{
    class NilJsEngine : JsEngine
    {
        protected override string Engine => "NiL.JS";

        private string _code = string.Empty;

        protected override string Compile(string code)
        {
            var engine = new Context();
            //engine.Debugging = true;
            //engine.DebuggerCallback += Engine_DebuggerCallback;
            engine.DefineVariable("log").Assign(JSValue.Marshal(new Action<object>(Console.WriteLine)));

            _code += Compiler;
            _code += $"\nvar transform = function transform(c){{ return {Type}.transform(c);}};\n";
            engine.Eval(_code, false);

            var compiler = engine.GetVariable("transform").As<Function>();


            return compiler?.Call(new Arguments { code }).As<string>();
        }

        private void Engine_DebuggerCallback(Context sender, DebuggerCallbackEventArgs e)
        {
            Console.Clear();
            Console.WriteLine("Code:\n");
            for (var i = 0; i < _code.Length; i++)
            {
                if (i < e.Statement.Position - 50 || i > e.Statement.EndPosition + 50) continue;

                if (i >= e.Statement.Position && i <= e.Statement.EndPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.Write(_code[i]);
            }

            Console.WriteLine();
        }
    }
}
