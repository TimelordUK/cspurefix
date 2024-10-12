// See https://aka.ms/new-console-template for more information

using CommandLine;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport;
using PureFix.Types;

namespace PureFix.ConsoleApp;

internal partial class Program
{
    private static async Task RunApp(CommandOptions options)
    {
        switch (options.Application)
        {
            case "sk":
                await Runner.Run(options, MakeSkeletonAppHost);
                break;
            default:
                await Runner.Run(options, MakeTradeCaptureAppHost);
                break;
        }
    }
}