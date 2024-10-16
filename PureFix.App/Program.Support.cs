// See https://aka.ms/new-console-template for more information

using CommandLine;
using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;

namespace PureFix.ConsoleApp;

internal partial class Program
{
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
        generatorOptions.BackingTypeNamespace = $"PureFix.Types.{name}.QuickFix";
        return generatorOptions;
    }
}