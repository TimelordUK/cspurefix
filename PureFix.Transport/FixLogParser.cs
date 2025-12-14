using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PureFix.Transport
{
    public class FixLogParser : IFixLogParser
    {
        private readonly AsciiParser _asciiParser;
        public IFixDefinitions Definitions { get; }
        public Action<IMessageView> OnView { get; set; } = _ => { };

        private void GetViews(string file)
        {
            using var streamReader = File.OpenText(file);
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                if (line == null) continue;
                var idx = line.IndexOf("8=FIX", StringComparison.Ordinal);
                if (idx != -1)
                {
                    line = line[idx..];
                }
                Parse(line);
            }
        }

        public void Parse(string txt)
        {
            var b = Encoding.UTF8.GetBytes(txt);
            _asciiParser.ParseFrom(b, b.Length, (_, v) => OnView(v));
        }

        public FixLogParser(IFixConfig config)
        {
            var definitions = config.Definitions;
            Definitions = definitions ?? throw new ArgumentNullException(nameof(definitions));
            var qf = new QuickFixXmlFileParser(Definitions);
            _asciiParser = new AsciiParser(Definitions)
            {
                Delimiter = config.LogDelimiter
            };
        }

        public FixLogParser(string dictPath, byte delim = (byte)'|')
        {
            Definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(Definitions);
            _asciiParser = new AsciiParser(Definitions) { Delimiter = delim };
            qf.Parse(dictPath);
        }

        public void Snapshot(string file)
        {
            GetViews(file);
        }

        public void Tail(string file)
        {
            _ = new FollowingTail(new FileInfo(file),
            Encoding.ASCII, Parse);
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
