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
                Generate(d, options);
            }
        }
        else if (!string.IsNullOrEmpty(options.DictPath))
        {
            Generate(options.DictPath, options);
        }
        else
        {
            Console.WriteLine("please specify a dictionary to generate.");
        }
    }

    private static void Generate(string dict, CommandOptions options)
    {
        var definitions = GetDefinitions(dict);
        var generatorOptions = GetModularGeneratorOptions(
            definitions,
            dict,
            options.OutputPath,
            options.CustomNamespace,
            options.UseNuGetPackages,
            options.PackageVersion);

        Console.WriteLine($"Generating {definitions.Message.Count} messages to {generatorOptions.AssemblyName}");

        if (options.UseNuGetPackages)
        {
            Console.WriteLine($"  Using NuGet packages (version: {options.PackageVersion})");
        }

        if (!string.IsNullOrEmpty(options.CustomNamespace))
        {
            Console.WriteLine($"  Custom namespace: {options.CustomNamespace}");
        }

        var generator = new ModularGenerator(definitions, generatorOptions);
        generator.Process();

        Console.WriteLine($"Generation complete! Output: {Path.Join(options.OutputPath, generatorOptions.AssemblyName)}");
    }
}