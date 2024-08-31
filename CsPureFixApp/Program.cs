// See https://aka.ms/new-console-template for more information

using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;

var definitions = new FixDefinitions();
var parser = new QuickFixXmlFileParser(definitions);
// parser.Parse("FIX44.xml");
parser.Parse("FIX50SP2.xml");
Console.WriteLine(definitions.Message["A"]);