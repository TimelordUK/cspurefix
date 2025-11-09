#!/usr/bin/env dotnet-script
#r "PureFix.Dictionary/bin/Debug/net8.0/PureFix.Dictionary.dll"
#r "PureFix.Types.Core/bin/Debug/net8.0/PureFix.Types.Core.dll"

using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using System.IO;

var dictionaryPath = "Data/FIX44.xml";
var outputPath = "generated-types";

Console.WriteLine($"Generating FIX 4.4 types from {dictionaryPath}...");

var definitions = new FixDefinitions();
var parser = new QuickFixXmlFileParser(definitions);
parser.Parse(dictionaryPath);

Console.WriteLine($"Parsed {definitions.Messages.Count} messages");

var options = ModularGeneratorOptions.FromDictionaryPath(
    dictionaryPath,
    outputPath,
    definitions);

Console.WriteLine($"Assembly name: {options.AssemblyName}");
Console.WriteLine($"Output: {Path.Join(outputPath, options.AssemblyName)}");

var generator = new ModularGenerator(definitions, options);
generator.Process();

Console.WriteLine("Generation complete!");
