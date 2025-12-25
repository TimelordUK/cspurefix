// See https://aka.ms/new-console-template for more information

using CommandLine;
using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Dictionary.Parser.Repo;
using PureFix.Types;
using System.IO;

namespace PureFix.ConsoleApp;

internal partial class Program
{
    private static FixDefinitions GetDefinitions(string dictPath)
    {
        var definitions = new FixDefinitions();
        IFixDictionaryParser qfParser = File.Exists(dictPath) ?
               new QuickFixXmlFileParser(definitions) :
               new RepoFixXmlFileParser(GetVersion(dictPath), definitions);
        qfParser.Parse(dictPath);
        return definitions;
    }

    private static FixVersion GetVersion(string dictPath)
    {
        if (dictPath.Contains("FIX.5.0SP2"))
            return FixVersion.FIX50SP2;
        return FixVersion.FIX44;
    }

    private static ModularGeneratorOptions GetModularGeneratorOptions(
        IFixDefinitions definitions,
        string dictPath,
        string outputPath,
        string? customNamespace = null,
        bool usePackageReferences = false,
        string packageVersion = "0.1.0-alpha")
    {
        var options = ModularGeneratorOptions.FromDictionaryPath(
            dictPath,
            outputPath,
            definitions,
            customNamespace,
            usePackageReferences,
            packageVersion);
        return options;
    }

    [Obsolete("Use GetModularGeneratorOptions instead - this uses the old type system")]
    private static Options GetGeneratorOptions(IFixDefinitions definitions, string dictPath, string outputPath)
    {
        var generatorOptions = Options.FromVersion(definitions);
        var name = File.Exists(dictPath) ? Path.GetFileName(dictPath) : Path.GetDirectoryName(dictPath);
        name = name ?? Path.GetFileName(dictPath);
        name = name.Replace(".xml", string.Empty).Replace(".", string.Empty);
        generatorOptions.BackingTypeOutputPath = Path.Join(outputPath, "PureFix.Types", name, $"{definitions.Source}");
        generatorOptions.BackingTypeNamespace = $"PureFix.Types.{name}.{definitions.Source}";
        return generatorOptions;
    }
}