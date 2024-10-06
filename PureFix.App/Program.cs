// See https://aka.ms/new-console-template for more information

using PureFix.ConsoleApp;
using CommandLine;


class TestClass
{
    static async Task Main(string[] args)
    {
        var res = Parser.Default.ParseArguments<Options>(args);
        var options = res.Value;
        var app = options.Application;

        if (!string.IsNullOrEmpty(app))
        {
            await Runner.Run(options);
        }
        else
        {
            var parser = new FixLogParser(options);
            if (options.Tail)
            {
                parser.Tail();
            }else
            {
                parser.Snapshot();
            }                
        }
    }
}


