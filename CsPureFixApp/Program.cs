// See https://aka.ms/new-console-template for more information

using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;

Console.WriteLine("Hello, World!");
var parser = new QuickFixXmlFileParser(new FixDefinitions());
// parser.Parse("FIX44.xml");
parser.Parse("FIX50SP2.xml");