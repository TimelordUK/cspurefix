// See https://aka.ms/new-console-template for more information

using Arrow.Threading.Tasks;
using PureFIix.Test.Env;
using PureFix.ConsoleApp;
using PureFix.Dictionary.Definition;
using PureFix.Transport;
using PureFix.Transport.SocketTransport;
using PureFix.Types;
using Serilog;
using Serilog.Core;
using Microsoft.Extensions.DependencyInjection;


/*
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
*/


var runner = new Runner();
await runner.Run();


