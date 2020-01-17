using System;
using System.Diagnostics;
using System.IO;

namespace dotnet_ts_testing.Engines
{
    abstract class JsEngine : IJsEngine
    {
        public string Type { get; set; } = "tsc";
        public bool Minimize { get; set; } = false;

        protected abstract string Engine { get; }

        protected string Compiler => Read($"scripts/{Type}{(Minimize ? ".min" : string.Empty)}.js");


        protected abstract string Compile(string code);

        private string Read(string file) => File.ReadAllText($"{file}");



        public void Test(string test)
        {
            var expected = Read($"tests/{test}.js");
            var code = Read($"tests/{test}.ts");

            Console.WriteLine($"[{Engine}] Test {test} started ...");
            PrintLine();
            try
            {
                var start = Stopwatch.StartNew();
                var actual = Compile(code);

                PrintLine();

                if (actual != expected)
                {
                    Console.Error.WriteLine($"[{Engine}] Test {test} failed in {start.Elapsed}");
                    PrintLine();
                    Console.WriteLine(actual);
                }
                else
                    Console.WriteLine($"[{Engine}] Test {test} succeeded in {start.Elapsed}");
            }
            catch (Exception ex)
            {
                PrintLine();
                Console.Error.WriteLine($"[{Engine}] Test {test} failed\n{ex}");
            }
            PrintLine();
        }

        private static void PrintLine()
        {
            Console.WriteLine($"----------------------------------");
        }
    }
}
