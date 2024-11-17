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
    internal class DictMessageParser
    {
        public DictMeta Meta { get; private set; }
        public IFixDefinitions Definitions { get; private set; }
        public IFixMessageFactory? MessageFactory { get; private set; }
        public TypeInfo? MessageFactoryTypeInfo { get; private set; }
        public IMessageParser? Parser { get; private set; }

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
                var fixType=  typesAssembly.GetType(meta.Type);
                var factories = typesAssembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IFixMessageFactory))).ToList();
                var ss = factories.FirstOrDefault(t=>t.Namespace == fixNameSpace);
                if (ss != null)
                {
                    MessageFactoryTypeInfo = ss;
                    MessageFactory = Activator.CreateInstance(ss.AsType()) as IFixMessageFactory;
                    Parser = new AsciiParser(definitions);
                }
            }
        }
    }
}
