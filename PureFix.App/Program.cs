// See https://aka.ms/new-console-template for more information

using PureFix.ConsoleApp;
using CommandLine;
using PureFix.Types;

class TestClass
{
    static async Task Main(string[] args)
    {
        var res = Parser.Default.ParseArguments<Options>(args);
        var options = res.Value;
        var app = options.Application;

        if (!string.IsNullOrEmpty(app))
        {
            var runner = new Runner();
            await runner.Run();
        }
        else
        {
            var mf = options.GetFactory();
            var parser = new FixLogParser(options.DictPath, mf, options.FixLogPath, options.OutputFormat);
        }
    }
}


