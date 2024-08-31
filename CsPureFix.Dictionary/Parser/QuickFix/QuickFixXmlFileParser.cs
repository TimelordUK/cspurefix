using System.ComponentModel;
using System.Data.SqlTypes;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using PureFix.Buffer.tag;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using static PureFix.Dictionary.Parser.QuickFix.QuickFixXmlFileParser.Node;

namespace PureFix.Dictionary.Parser.QuickFix;

public partial class QuickFixXmlFileParser
{
    public FixDefinitions Definitions { get; }
    public Queue<Node> Queue { get; } = new ();
    private int _nextId;
    private Dictionary<int, Node> _nodes = new();
    private readonly Dictionary<int, List<Edge>> _edges = new();

    public class Node
    {
        public record Edge(int Head, int Tail);
        public string Name { get; }
        public int ID { get; }
        public XElement Element { get; }
        public ElementType Type { get; }
        private readonly List<Edge> _edges = new ();
        public IReadOnlyList<Edge> Edges => _edges;

        public Node(int id, string name, ElementType elementType, XElement element)
        {
            ID = id;
            Name = name;
            Element = element;
            Type = elementType;
        }

        public override string ToString()
        {
            return $"Node: ID = {ID}, Name = {Name}, Type = {Type}, element = {Element}, Edges = {string.Join(", ", Edges)}";
        }

        public Edge MakeEdge(int tail)
        {
            var edge = new Edge(ID, tail);
            _edges.Add(edge);
            return edge;
        }

        public enum ElementType
        {
            MessageDefinition,
            FieldDefinition,
            FieldDeclaration,
            InlineGroupDefinition,
            ComponentDefinition,
            GroupDefinition,
            GroupDeclaration,
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
            case ElementType.FieldDefinition:
            {
                var sd = GetField(node.Element);
                Definitions.AddSimple(sd);
                break;
            }

            case ElementType.MessageDefinition:
            {
                var md = GetMessage(node.Element);
                Definitions.AddMessaqe(md);
                ExpandSet(node);
                break;
            }

            case ElementType.FieldDeclaration:
            {
                if (_nodes.TryGetValue(node.Edges[0].Tail, out var containedSet))
                {
                    switch (containedSet.Type)
                    {
                        case ElementType.MessageDefinition:
                        {
                            if (Definitions.Message.TryGetValue(containedSet.Name, out var md))
                            {

                            }
                            else
                            {
                                throw new InvalidDataException(
                                    $"declared field {node} has no parent set {containedSet.Name}");
                            }
                            break;
                        }
                    }
                }
                break;
            }
        }
    }

    private void AddEdge(Edge edge)
    {
        if (!_edges.TryGetValue(edge.Head, out var pCached))
        {
            _edges[edge.Head] = pCached = new List<Edge>();
        }
        pCached.Add(edge);

        if (!_edges.TryGetValue(edge.Tail, out var cCached))
        {
            _edges[edge.Tail] = cCached = new List<Edge>();
        }
        cCached.Add(edge);
    }

    public void ExpandSet(Node node)
    {
        foreach (var element in node.Element.Descendants("field"))
        {
            switch (element.Name.LocalName)
            {
                case "field":
                {
                    var at = AsAttributeDict(element);
                    var childNode = MakeNode(at["name"], element, ElementType.FieldDeclaration);
                    var edge = node.MakeEdge(childNode.ID);
                    childNode.MakeEdge(node.ID);
                    AddEdge(edge);
                    break;
                }

                case "group":
                {
                    var at = AsAttributeDict(element);
                    var inlinedFields = element.Descendants("field").ToList();
                    if (inlinedFields.Count > 0)
                    {
                        var tailNode = MakeNode(at["name"], element, ElementType.InlineGroupDefinition);
                        var edge = node.MakeEdge(tailNode.ID);
                        tailNode.MakeEdge(node.ID);
                        AddEdge(edge);
                    }
                    else
                    {
                        var tailNode = MakeNode(at["name"], element, ElementType.GroupDeclaration);
                        var edge = node.MakeEdge(tailNode.ID);
                        tailNode.MakeEdge(node.ID);
                        AddEdge(edge);
                    }
                    break;
                }
            }
        }
    }
}