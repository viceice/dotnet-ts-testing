using dotnet_ts_testing.Engines;
using System;

namespace dotnet_ts_testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("dotnet ts converter");
            var type = "tsc";
            var engines = new IJsEngine[] { new JintJsEngine(), new JurassicJsEngine(), new V8JsEngine(), new ChakraJsEngine() };

            foreach (var e in engines)
            {
                e.Type = type;
                e.Test("test");
            }
        }
    }
}
