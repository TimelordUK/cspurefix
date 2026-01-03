using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Dictionary.Parser.Repo;
using PureFix.Types;
using PureFix.Types.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var path = PathUtil.GetPath(meta.Dict ?? "");
            if (path == null) throw new ArgumentException($"path cannot be resolved {meta}");
            IFixDictionaryParser qfParser = File.Exists(path) ?
                new QuickFixXmlFileParser(definitions) :
                new RepoFixXmlFileParser(FixVersion.FIX50SP2, definitions);
            var fixNameSpace = meta.Type ?? "";
            qfParser.Parse(path);
            Definitions = definitions;
            if (meta.Type != null)
            {
                // First try the provided assembly, then search all loaded assemblies
                var factory = FindMessageFactory(typesAssembly, fixNameSpace);
                if (factory == null)
                {
                    // Search all loaded assemblies for the type namespace
                    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        factory = FindMessageFactory(asm, fixNameSpace);
                        if (factory != null) break;
                    }
                }

                if (factory != null)
                {
                    MessageFactoryTypeInfo = factory;
                    MessageFactory = Activator.CreateInstance(factory.AsType()) as IFixMessageFactory;
                    Console.WriteLine($"Loaded MessageFactory for {meta.Name}: {factory.FullName}");
                }
                else
                {
                    Console.WriteLine($"Warning: No MessageFactory found for {meta.Name} (namespace: {fixNameSpace})");
                }
            }
        }

        private static TypeInfo? FindMessageFactory(Assembly assembly, string fixNameSpace)
        {
            try
            {
                var factories = assembly.DefinedTypes
                    .Where(t => t.ImplementedInterfaces.Contains(typeof(IFixMessageFactory)))
                    .ToList();
                return factories.FirstOrDefault(t => t.Namespace == fixNameSpace);
            }
            catch
            {
                // Some assemblies may throw on reflection
                return null;
            }
        }
        
        public (ParseResult result, List<AsciiView> views) Parse(ParseRequest request)
        {
            var parser = new AsciiParser(Definitions) { Delimiter = request.Delim };
            var result = new ParseResult
            {
                Request = request,
            };
            
            if (MessageFactory == null || request.Messages == null || request.Messages.Count == 0)
            {
                return (result, []);
            }

            var messages = request.Messages;
           
            List<AsciiView> views = [];
            foreach (var l in messages)
            {
                var line = TrimFix(l);
                views.Clear();
                parser.ParseFrom(Encoding.ASCII.GetBytes(line), line.Length, (_, v) => views.Add((AsciiView)v));
                foreach (var view in views)
                {
                    var m = new ParsedMessage();
                    ParseMessage(result, view, m);
                }
            }

            return (result, views);
        }

        public ParseResult Structure(ParseRequest request)
        {
            var (result, views) = Parse(request);
            if (views == null || views.Count == 0) return result;
            var view = views[0];
            if (view == null) return result;
            result.Messages.Clear();
            var taken = new HashSet<string>();
            view.Structure?.Segments.ToList().ForEach(segment =>
                {
                    if (taken.Contains(segment.Name ?? "")) return;
                    if (!string.IsNullOrEmpty(segment.Name))
                    {
                        var tags = view.Structure?.GetSortedTags(segment);
                        var m = new ParsedMessage() { Name = segment.Name };
                        if (tags != null)
                        {
                            taken.Add(m.Name);
                            result.Messages.Add(m);
                            foreach (var tag in tags)
                            {
                                var mt = GetTag(tag, view);
                                if (mt != null)
                                {
                                    m.Tags.Add(mt);
                                }
                            }
                        }
                    }
                });

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
            var o = MessageFactory?.ToFixMessage(view);
            if (o == null) return;
            m.Json = o;
            m.Msg = view.Buffer.ToString();
            m.Name = view.Segment?.Name ?? string.Empty;
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
            var v = view.Buffer.GetString(t.Start, t.End + 1);
            var field = Definitions.TagToSimple.GetValueOrDefault(t.Tag);
            if (field == null) return new MessageTag {
                Type = "STRING",
                Fid = t.Tag,
                Name = $"Custom_{t.Tag}",
                Start = t.Start, 
                End = t.End,
                Description = "not in dict.",
                Value = v
            };
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
