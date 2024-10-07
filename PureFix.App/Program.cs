// See https://aka.ms/new-console-template for more information

using Arrow.Threading.Tasks;
using CommandLine;
using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport.Session;
using PureFix.Types;

namespace PureFix.ConsoleApp;

internal class Program
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
            Examples(options);
        }
    }

    private static void Examples(CommandOptions options)
    {
        Console.WriteLine("trim an input dictionary file and output only messages and dependent fields on given message set.");
        Console.WriteLine("./PureFix.ConsoleApp -d FIX50SP2.xml -T 0 1 2 3 4 5 A AE");
        Console.WriteLine("");
        Console.WriteLine("tail a file and output as tag decoded resolving enums.");
        Console.WriteLine("./PureFix.ConsoleApp -f logs/test_client-fix-log20241007.txt -d FIX50SP2.xml -t -o tags");
        Console.WriteLine("");
        Console.WriteLine("tail a file and output as json objects");
        Console.WriteLine("./PureFix.ConsoleApp -f logs/test_client-fix-log20241007.txt -d FIX50SP2.xml -t -o json");
        Console.WriteLine("");        
        Console.WriteLine("parse a log in decode tag format with no tail");
        Console.WriteLine("./PureFix.ConsoleApp -f logs/test_client-fix-log20241007.txt -d FIX50SP2.xml -o tags");
        Console.WriteLine("");
        Console.WriteLine("run built in skeleton client and server to logon and heartbeat");
        Console.WriteLine("./PureFix.ConsoleApp -d FIX50SP2.xml -a sk");
        Console.WriteLine("");
        Console.WriteLine("run built in skeleton client and server to logon and heartbeat with fix44 config");
        Console.WriteLine("./PureFix.ConsoleApp -d FIX44.xml -I test-qf44-initiator.json -A test-qf44-acceptor.json -a sk");
        Console.WriteLine("");
        Console.WriteLine("run built in trade capture client and server to logon and heartbeat");
        Console.WriteLine("./PureFix.ConsoleApp -d FIX50SP2.xml -a tc");        
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

    // have different makehosts based on application type
    private static BaseAppDI MakeTradeCaptureAppHost(IFixClock clock, IFixConfig config)
    {
        var factory = new ConsoleLogFactory(config.Name());
        var queue = new AsyncWorkQueue();
        var app = new TradeCaptureDI(queue, factory, clock, config);
        return app;
    }

    private static BaseAppDI MakeSkeletonAppHost(IFixClock clock, IFixConfig config)
    {
        var factory = new ConsoleLogFactory(config.Name());
        var queue = new AsyncWorkQueue();
        var app = new SkeletonDI(queue, factory, clock, config);
        return app;
    }

    private static IFixDefinitions GetDefinitions(CommandOptions options)
    {
        var definitions = new FixDefinitions();
        var qfParser = new QuickFixXmlFileParser(definitions);
        qfParser.Parse(options.DictPath);
        return definitions;
    }

    private static Options GetGeneratorOptions(IFixDefinitions definitions, CommandOptions options)
    {
        var generatorOptions = Options.FromVersion(definitions);
        var name = Path.GetFileName(options.DictPath);
        name = name.Replace(".xml", string.Empty).Replace(".", string.Empty);
        generatorOptions.BackingTypeOutputPath = Path.Join(options.OutputPath, "PureFix.Types", name, "QuickFix");
        generatorOptions.BackingTypeNamespace = Path.Join("PureFix.Types", name, "QuickFix");
        return generatorOptions;
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