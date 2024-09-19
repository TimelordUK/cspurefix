// See https://aka.ms/new-console-template for more information

using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using Serilog;
using Serilog.Core;


using Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

log.Information("hello this is a log message");

var definitions = new FixDefinitions();
var parser = new QuickFixXmlFileParser(definitions);
// parser.Parse("FIX44.xml");
parser.Parse("FIX50SP2.xml");
var logon = definitions.Message["A"];
Console.WriteLine(logon);