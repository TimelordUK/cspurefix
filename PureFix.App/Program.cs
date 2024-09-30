// See https://aka.ms/new-console-template for more information

using Arrow.Threading.Tasks;
using PureFix.ConsoleApp;
using CommandLine;

class TestClass
{
    static async Task Main(string[] args)
    {
        var res = Parser.Default.ParseArguments<Options>(args);
        var options = res.Value;
        var fixPath = options.FixLogPath;
        var app = options.Application;

        if (!string.IsNullOrEmpty(app))
        {
            var runner = new Runner();
            await runner.Run();
        }
        else
        {
            var parser = new FixLogParser(options.DictPath, options.FixLogPath, options.OutputFormat);
        }
    }
}


