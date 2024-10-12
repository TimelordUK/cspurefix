// See https://aka.ms/new-console-template for more information

using CommandLine;
using PureFix.Dictionary.Compiler;

namespace PureFix.ConsoleApp;

internal partial class Program
{
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
}