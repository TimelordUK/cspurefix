// See https://aka.ms/new-console-template for more information

using Arrow.Threading.Tasks;
using PureFIix.Test.Env;
using PureFix.ConsoleApp;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
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

var factory = new ConsoleLogFactory();
var initiatorConfig = FixConfig.MakeConfigFromPaths(factory, Fix44PathHelper.DataDictRootPath, Path.Join(Fix44PathHelper.SessionRootPath, "test-qf44-initiator.json"));
var acceptorConfig = FixConfig.MakeConfigFromPaths(factory, Fix44PathHelper.DataDictRootPath, Path.Join(Fix44PathHelper.SessionRootPath, "test-qf44-acceptor.json"));

var qInitiator = new AsyncWorkQueue();
var qAcceptor = new AsyncWorkQueue();
var clock = new RealtimeClock();
var diInitiator = new TradeCaptureDI(qInitiator, clock, initiatorConfig);
var diAcceptor = new TradeCaptureDI(qAcceptor, clock, acceptorConfig);

var initiator = diInitiator.AppHost.Services.GetService<BaseTcpEntity>();
var acceptor = diAcceptor.AppHost.Services.GetService<BaseTcpEntity>();

var cts = new CancellationTokenSource();

if (initiator != null && acceptor != null)
{
    var t2 = acceptor.Start(cts.Token);
    await Task.Delay(500);
    var t1 = initiator.Start(cts.Token);
    Task.WaitAll(t1, t2);
}

