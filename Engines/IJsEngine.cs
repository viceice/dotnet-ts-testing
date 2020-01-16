namespace dotnet_ts_testing.Engines
{
    interface IJsEngine
    {
        string Type { get; set; }
        void Test(string test);
    }
}
