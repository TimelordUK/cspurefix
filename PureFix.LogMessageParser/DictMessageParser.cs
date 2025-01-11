using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.LogMessageParser
{
    public class DictMessageParser : IDictMessageParser
    {
        public DictMeta Meta { get; }
        public IFixDefinitions Definitions { get; }
        public IFixMessageFactory? MessageFactory { get; }
        public TypeInfo? MessageFactoryTypeInfo { get; }
     
        public DictMessageParser(DictMeta meta, Assembly typesAssembly)
        {
            Meta = meta;
            var definitions = new FixDefinitions();
            var qfParser = new QuickFixXmlFileParser(definitions);
            var path = PathUtil.GetPath(meta.Dict ?? "");
            var fixNameSpace = meta.Type ?? "";
            qfParser.Parse(path);
            Definitions = definitions;
            if (meta.Type != null)
            {
                var fixType = typesAssembly.GetType(meta.Type);
                var factories = typesAssembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IFixMessageFactory))).ToList();
                var ss = factories.FirstOrDefault(t => t.Namespace == fixNameSpace);
                if (ss != null)
                {
                    MessageFactoryTypeInfo = ss;
                    MessageFactory = Activator.CreateInstance(ss.AsType()) as IFixMessageFactory;
                }
            }
        }
        
        public ParseResult Parse(ParseRequest request)
        {
            var parser = new AsciiParser(Definitions) { Delimiter = request.Delim };
            var result = new ParseResult
            {
                Request = request,
            };
            
            if (MessageFactory == null || request.Messages == null || request.Messages.Count == 0)
            {
                return result;
            }

            var messages = request.Messages;
           
            List<AsciiView> views = [];
            foreach (var l in messages)
            {
                string line = TrimFix(l);
                views.Clear();
                parser.ParseFrom(Encoding.ASCII.GetBytes(line), line.Length, (_, v) => views.Add((AsciiView)v));
                foreach (var view in views)
                {
                    var m = new ParsedMessage();
                    ParseMessage(result, view, m);
                }
            }

            return result;
        }

        private static string TrimFix(string l)
        {
            var line = l;
            var idx = line.IndexOf("8=FIX", StringComparison.Ordinal);
            if (idx != -1)
            {
                line = line[idx..];
            }

            return line;
        }

        private void ParseMessage(ParseResult result, AsciiView view, ParsedMessage m)
        {
            if (MessageFactory == null) return;
            var o = MessageFactory.ToFixMessage(view);
            if (o == null) return;
            m.Json = o;
            m.Msg = view.Buffer.ToString();
            result.Messages.Add(m);
            if (view.Tags == null) return;
            for (var i = 0; i < view.Tags.NextTagPos; i++)
            {
                var t = view.Tags[i];
                var tag = GetTag(t, view);
                if (tag != null)
                {
                    m.Tags.Add(tag);
                }
            }
        }

        private MessageTag? GetTag(TagPos t, AsciiView view)
        {
            var field = Definitions.TagToSimple.GetValueOrDefault(t.Tag);
            if (field == null) return null;
            var v = view.Buffer.GetString(t.Start, t.End + 1);
            MessageTag tag = new()
            {
                Type = field.Type,
                Fid = t.Tag,
                Name = field.Name,
                Start = t.Start,
                End = t.End,
                Description = field.IsEnum ? field.ResolveEnum(v)  : "",
                Value = v
            };
            return tag;
        }
    }
}
