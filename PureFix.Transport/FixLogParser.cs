using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PureFix.Transport
{
    public class FixLogParser
    {
        private readonly IMessageParser _asciiParser;
        public Action<IMessageView> OnView { get; set; } = (v) => { };

        private void GetViews(string file)
        {
            using var streamReader = File.OpenText(file);
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                if (line != null)
                {
                    Parse(line);
                }
            }
        }

        public void Parse(string txt)
        {
            var b = Encoding.UTF8.GetBytes(txt);
            _asciiParser.ParseFrom(b, b.Length, (p, v) => OnView(v));
        }

        public FixLogParser(string dictPath, byte delim = (byte)'|')
        {
            var definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(definitions);
            _asciiParser = new AsciiParser(definitions) { Delimiter = delim };
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
