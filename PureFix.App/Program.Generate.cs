// See https://aka.ms/new-console-template for more information

using CommandLine;
using PureFix.Dictionary.Compiler;

namespace PureFix.ConsoleApp;

internal partial class Program
{
    private static void Generate(CommandOptions options)
    {
        if (options.Dicts.Any())
        {
            foreach (var d in options.Dicts)
            {
                Generate(d, options.OutputPath);
            }
        }
        else if (!string.IsNullOrEmpty(options.DictPath))
        {
            Generate(options.DictPath, options.OutputPath);
        } else
        {
            Console.WriteLine("please specify a dictionary to genereate.");
        }
    }

    private static void Generate(string dict, string optionsOutputPath)
    {
        var definitions = GetDefinitions(dict);
        var generatorOptions = GetGeneratorOptions(definitions, dict, optionsOutputPath);
        var generator = new MessageGenerator(null, definitions, generatorOptions);
        generator.Process();
    }
}