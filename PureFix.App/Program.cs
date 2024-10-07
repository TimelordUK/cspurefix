// See https://aka.ms/new-console-template for more information

using CommandLine;
using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
namespace PureFix.ConsoleApp;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var res = Parser.Default.ParseArguments<CommandOptions>(args);
        var options = res.Value;
        
        if (options.Generate)
        {
            Generate(options);
        }
        else if (!string.IsNullOrEmpty(options.Application))
        {
            await Runner.Run(options);
        }
        else
        {
           ParseLog(options);            
        }
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