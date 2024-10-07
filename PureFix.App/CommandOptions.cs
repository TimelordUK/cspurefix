using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.ConsoleApp
{
    public class CommandOptions
    {
        [Option('f', "fix", Required = false, HelpText = "parse fix log into json or tags")]
        public string FixLogPath { get; set; } = "";

        [Option('d', "dict", Required = false, HelpText = "dictionary xml file.")]
        public string DictPath { get; set; } = "";

        [Option('o', "output", Required = false, HelpText = "output tags | json", Default = "tags")]
        public string OutputFormat { get; set; } = "";

        [Option('a', "application", Required = false, HelpText = "test app")]
        public string Application { get; set; } = "";

        [Option('t', "tail", Required = false, HelpText = "tail the fix log and decode")]
        public bool Tail { get; set; } = false;

        [Option('I', "initiator", Required = false, HelpText = "initiator json config", Default = "test-qf52-initiator.json")]
        public string Initiator { get; set; } = "";

        [Option('A', "acceptor", Required = false, HelpText = "acceptor json config")]
        public string Acceptor { get; set; } = "";

        [Option('g', "generate", Required = false, HelpText = "generate types for dictionary")]
        public bool Generate { get; set; } = false;

        [Option('p', "path", Required = false, HelpText = "output path of generator", Default = ".")]
        public string OutputPath { get; set; } = "";

        [Option('T', "trim", Required = false, HelpText = "list of message types 0 1 2 3 4 5 AE")]
        public IEnumerable<string> MsgTypes { get; set; } = [];
    }
}
