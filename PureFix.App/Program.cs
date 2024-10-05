// See https://aka.ms/new-console-template for more information

using PureFix.ConsoleApp;
using CommandLine;
using PureFix.Types;
using System.Text;

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
            var parser = new FixLogParser(options);
            if (options.Tail)
            {
                var follower = new FollowingTail(new FileInfo(options.FixLogPath),
                  Encoding.ASCII,
                  parser.Parse);
            }
        }
    }
}


