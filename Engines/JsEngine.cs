using System;
using System.IO;

namespace dotnet_ts_testing.Engines
{
    abstract class JsEngine : IJsEngine
    {
        public string Type { get; set; } = "tsc";

        protected abstract string Engine { get; }

        protected string Compiler => Read($"scripts/{Type}.js");


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
                var actual = Compile(code);

                PrintLine();

                if (actual != expected)
                {
                    Console.Error.WriteLine($"[{Engine}] Test {test} failed");
                    PrintLine();
                    Console.WriteLine(actual);
                }
                else
                    Console.WriteLine($"[{Engine}] Test {test} succeeded");
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
