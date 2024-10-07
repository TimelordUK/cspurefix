// See https://aka.ms/new-console-template for more information

using CommandLine;
using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Parser.QuickFix;

namespace PureFix.ConsoleApp;

internal partial class Program
{
    public static async Task Main(string[] args)
    {
        var res = Parser.Default.ParseArguments<CommandOptions>(args);
        var options = res.Value;

        if (options.MsgTypes?.Count() > 0)
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

    private static void Trim(CommandOptions options)
    {
        if (string.IsNullOrEmpty(options.DictPath))
        {
            Console.WriteLine("please specify a dictionary to genereate.");
        } else
        {
            var definitions = GetDefinitions(options);
            var builder = new QuickFixXmlFileBuilder(definitions);
            var encoded = builder.Write(options.MsgTypes.ToArray());
            Console.WriteLine(encoded);
        }
    }

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

    private static void Generate(CommandOptions options)
    {
        if (string.IsNullOrEmpty(options.DictPath))
        {
            Console.WriteLine("please specify a dictionary to genereate.");
        }
        else
        {
            var definitions = GetDefinitions(options);
            var generatorOptions = GetGeneratorOptions(definitions, options);
            var generator = new MessageGenerator(null, definitions, generatorOptions);
            generator.Process();
        }
    }

    private static void ParseLog(CommandOptions options)
    {
        var parser = new FixLogParser(options);
        if (options.Tail)
        {
            parser.Tail();
        }
        else
        {
            parser.Snapshot();
        }
    }
}