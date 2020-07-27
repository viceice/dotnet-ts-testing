using System;

namespace dotnet_ts_testing.Engines
{
    interface IJsEngine: IDisposable
    {
        string Type { get; set; }
        bool Minimize { get; set; }
        bool Failed { get; }

        void Test(string test);
    }
}
