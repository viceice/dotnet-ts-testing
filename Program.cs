using dotnet_ts_testing.Engines;
using System;
using System.Linq;

namespace dotnet_ts_testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("dotnet ts converter");
            var type = "tsc";
            var min = args.Any(s => s == "--min" || s == "-m");
            var engines = new IJsEngine[] { new JintJsEngine(), new JurassicJsEngine(), new V8JsEngine(), new ChakraJsEngine(), new NilJsEngine() };

            foreach (var e in engines)
            {
                using (e)
                {
                    e.Type = type;
                    e.Minimize = min;
                    e.Test("test");
                }
            }
        }
    }
}
