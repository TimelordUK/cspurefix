// See https://aka.ms/new-console-template for more information

using Arrow.Threading.Tasks;
using CommandLine;
using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport.Session;
using PureFix.Types;

namespace PureFix.ConsoleApp;

internal partial class Program
{
    private static void Examples()
    {
        Console.WriteLine("trim an input dictionary file and output only messages and dependent fields on given message set.");
        Console.WriteLine("./PureFix.ConsoleApp -d FIX50SP2.xml -T 0 1 2 3 4 5 A AE");
        Console.WriteLine("");
        Console.WriteLine("tail a file and output as tag decoded resolving enums.");
        Console.WriteLine("./PureFix.ConsoleApp -f logs/test_client-fix-log20241007.txt -d FIX50SP2.xml -t -o tags");
        Console.WriteLine("");
        Console.WriteLine("tail a file and output as json objects");
        Console.WriteLine("./PureFix.ConsoleApp -f logs/test_client-fix-log20241007.txt -d FIX50SP2.xml -t -o json");
        Console.WriteLine("");        
        Console.WriteLine("parse a fix52 log in decode tag format with no tail");
        Console.WriteLine("./PureFix.ConsoleApp -f logs/test_client-fix-log20241007.txt -d FIX50SP2.xml -o tags");
        Console.WriteLine("");
        Console.WriteLine("parse a fix44 log in json format with no tail");
        Console.WriteLine("./PureFix.ConsoleApp -f logs/test_client-fix-log20241007.txt -d FIX44.xml -o json");
        Console.WriteLine("");
        Console.WriteLine("parse a fix52 log in decode tag format with no tail and filter only msg type A");
        Console.WriteLine("./PureFix.ConsoleApp -f logs/test_client-fix-log20241007.txt -d FIX50SP2.xml -o tags -T A");
        Console.WriteLine("");
        Console.WriteLine("run built in skeleton client and server to logon and heartbeat");
        Console.WriteLine("./PureFix.ConsoleApp -d FIX50SP2.xml -A test-qf52-acceptor.json -a sk");
        Console.WriteLine("");
        Console.WriteLine("run built in skeleton client and server to logon and heartbeat with fix44 config");
        Console.WriteLine("./PureFix.ConsoleApp -d FIX44.xml -I test-qf44-initiator.json -A test-qf44-acceptor.json -a sk");
        Console.WriteLine("");
        Console.WriteLine("run built in trade capture client and server to logon and heartbeat");
        Console.WriteLine("./PureFix.ConsoleApp -d FIX50SP2.xml -A test-qf52-acceptor.json -a tc");        
    }
}