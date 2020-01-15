using Esprima;
using Jint;
using Jint.Native;
using System;
using System.IO;

namespace dotnet_ts_testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("dotnet ts converter");
            var type = "tsc";
            var source = File.ReadAllText($"scripts/{type}.js");
            var prog = new JavaScriptParser(source, new ParserOptions { SourceType = SourceType.Script }).ParseProgram();

            var engine = new Engine(c => c.DebugMode(false))
                            .SetValue("log", new Action<object>(Console.WriteLine));
            engine.Global.FastAddProperty("window", engine.Global, false, false, false);


            engine.Execute(prog);

            const string codeFile = "test";
            var code = File.ReadAllText($"scripts/{codeFile}.ts");
            var compiler = engine.Global.Get(type).AsObject();
            var res = compiler.Get("transform").Invoke(code, null, $"{codeFile}.ts");

            Console.WriteLine("-----------------------");
            Console.WriteLine($"before:\n\n{code}");
            Console.WriteLine("-----------------------");
            Console.WriteLine("after: \n\n" + res.AsString());
        }
    }
}
