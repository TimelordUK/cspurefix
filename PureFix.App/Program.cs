// See https://aka.ms/new-console-template for more information

using CommandLine;
using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport;
using PureFix.Types;

namespace PureFix.ConsoleApp;

internal partial class Program
{
    public static async Task Main(string[] args)
    {
        var res = Parser.Default.ParseArguments<CommandOptions>(args);
        var options = res.Value;

        if (options == null)
        {
            Examples();
        }
        else if (options.MsgTypes?.Count() > 0 && string.IsNullOrEmpty(options.FixLogPath))
        {
            Trim(options);
        }
        else if (options.Generate)
        {
            Generate(options);
        }
        else if (!string.IsNullOrEmpty(options.Application))
        {
            await RunApp(options);
        }
        else if (!string.IsNullOrEmpty(options.FixLogPath))
        {
            ParseLog(options);
        }
        else
        {
            Examples();
        }
    }
}