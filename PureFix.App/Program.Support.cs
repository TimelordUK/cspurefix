// See https://aka.ms/new-console-template for more information

using CommandLine;
using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;

namespace PureFix.ConsoleApp;

internal partial class Program
{
    private static FixDefinitions GetDefinitions(string dictPath)
    {
        var definitions = new FixDefinitions();
        var qfParser = new QuickFixXmlFileParser(definitions);
        qfParser.Parse(dictPath);
        return definitions;
    }

    private static Options GetGeneratorOptions(IFixDefinitions definitions, string dictPath, string outputPath)
    {
        var generatorOptions = Options.FromVersion(definitions);
        var name = Path.GetFileName(dictPath);
        name = name.Replace(".xml", string.Empty).Replace(".", string.Empty);
        generatorOptions.BackingTypeOutputPath = Path.Join(outputPath, "PureFix.Types", name, "QuickFix");
        generatorOptions.BackingTypeNamespace = $"PureFix.Types.{name}.QuickFix";
        return generatorOptions;
    }
}