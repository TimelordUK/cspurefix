// See https://aka.ms/new-console-template for more information

using CommandLine;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport;
using PureFix.Types;

namespace PureFix.ConsoleApp;

internal partial class Program
{
    private static void Trim(CommandOptions options)
    {
        if (string.IsNullOrEmpty(options.DictPath))
        {
            Console.WriteLine("please specify a dictionary to genereate.");
        }
        else
        {
            var definitions = GetDefinitions(options);
            var builder = new QuickFixXmlFileBuilder(definitions);
            var encoded = builder.Write(options.MsgTypes.ToArray());
            Console.WriteLine(encoded);
        }
    }
}