using System.Data.SqlTypes;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using PureFix.Buffer.tag;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Parser.QuickFix;

public partial class QuickFixXmlFileParser
{
    public FixDefinitions Definitions { get; }
    public Queue<Node> Queue { get; } = new ();
    private int _nextId;
    private Dictionary<int, Node> _nodes = new();

    public class Node
    {
        public string Name { get; }
        public int ID { get; }
        public XElement Element { get; }
        public ElementType Type { get; }
        private readonly List<Node> _depenedencies = new ();
        public IReadOnlyList<Node> Dependencies => _depenedencies;

        public Node(int id, string name, ElementType elementType, XElement element)
        {
            ID = id;
            Name = name;
            Element = element;
            Type = elementType;
        }
        
        public enum ElementType
        {
            MessageDefinition,
            FieldDefinition,
            FieldDeclaration,
            InlineGroupDefinition,
            ComponentDefinition,
            GroupDefinition,
            ComponentDeclaration,
        }
    }

    public QuickFixXmlFileParser(FixDefinitions definitions)
    {
        Definitions = definitions;
    }

    public void Parse(string path)
    {
        // first parse all fields including their enum definitions, and add to the dictionary
        var doc = XDocument.Load(path);
        ParseFields(doc);
        ParseMessages(doc);
        while (Queue.Count > 0)
        {
            var element = Queue.Dequeue();
            Work(element);
        }
    }

    public void Work(Node node)
    {
        switch (node.Type)
        { 
            /*
             * all messages, components and groups eventually reduce to sets of fields,
             * here we are making an actual field definition which can be contained by any
             * of above sets.  The definition has no context, but once declared in a component
             * it has a position and hence context.
             */
            case Node.ElementType.FieldDefinition:
            {
                var sd = GetField(node.Element);
                Definitions.AddSimple(sd);
                break;
            }

            case Node.ElementType.MessageDefinition:
            {
                var md = GetMessage(node.Element);
                Definitions.AddMessaqe(md);
                break;
            }
        }
    }
}