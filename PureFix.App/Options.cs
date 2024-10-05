using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.ConsoleApp
{
    public class Options
    {
        [Option('f', "fix", Required = false, HelpText = "fix log to parse.")]
        public string FixLogPath { get; set; } = "";

        [Option('d', "dict", Required = false, HelpText = "dictionary xml file.")]
        public string DictPath { get; set; } = "";

        [Option('o', "output", Required = false, HelpText = "output tags, json", Default = "tags")]
        public string OutputFormat { get; set; } = "";

        [Option('a', "application", Required = false, HelpText = "test app")]
        public string Application { get; set; } = "";

        [Option('t', "tail", Required = false, HelpText = "tail the fix log and decode")]
        public bool Tail { get; set; } = false;
    }
}
