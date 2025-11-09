using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using System;
using System.IO;

var dictionaryPath = "../Data/FIX44.xml";
var outputPath = "../generated-types";

Console.WriteLine($"Generating full FIX 4.4 types from {dictionaryPath}...");

var definitions = new FixDefinitions();
var parser = new QuickFixXmlFileParser(definitions);
parser.Parse(dictionaryPath);

Console.WriteLine($"Parsed {definitions.Message.Count} messages");
Console.WriteLine($"Parsed {definitions.Component.Count} components");

var options = ModularGeneratorOptions.FromDictionaryPath(
    dictionaryPath,
    outputPath,
    definitions);

Console.WriteLine($"Assembly name: {options.AssemblyName}");
Console.WriteLine($"Output: {Path.Join(outputPath, options.AssemblyName)}");

var generator = new ModularGenerator(definitions, options);
generator.Process();

Console.WriteLine($"Generation complete! Generated {definitions.Message.Count} messages.");
