using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Xml;
using System.Linq;
using System.Net.Http.Headers;
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
    /*
     * a graph representing the data dictionary
     */
    private readonly Dictionary<int, Node> _nodes = new();
    private readonly Dictionary<int, List<Edge>> _edges = new();
    /*
     * indexed on node id, this allows an edge to quickly find the set in which to place a field.
     */
    private readonly Dictionary<int, ContainedFieldSet> _containedSets = new();

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
            SimpleFieldDefinition,
            SimpleFieldDeclaration,
            InlineGroupDefinition,
            GroupDefinition,
            GroupDeclaration,
            ComponentDefinition,
            ComponentDeclaration,
        }
    }

    public QuickFixXmlFileParser(FixDefinitions definitions)
    {
        Definitions = definitions;
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
            case ElementType.SimpleFieldDefinition:
            {
                var sd = GetField(node.Element);
                Definitions.AddSimple(sd);
                break;
            }

            case ElementType.MessageDefinition:
            {
                MessageDefinition(node);
                break;
            }

            case ElementType.ComponentDefinition:
            {
                ComponentDefinition(node);
                break;
            }

            /*
             * the edge will have head as the declared field and tail to the set in which the declaration should be added.
             */
            case ElementType.SimpleFieldDeclaration:
            {
                SimpleFieldDeclaration(node);
                break;
            }

            /*
             * in this case the group is defined locally private to the container in which it belongs.
             */
            case ElementType.InlineGroupDefinition:
            {
                InlineGroupDefinition(node);
                break;
            }

            case ElementType.ComponentDeclaration:
            {
                ComponentDeclaration(node);
                break;
            }
        }
    }

    public void Parse(string path)
    {
        // first parse all fields including their enum definitions, and add to the dictionary
        var doc = XDocument.Load(path);
        ParseFields(doc);
        ParseComponents(doc);
        ParseMessages(doc);
        while (Queue.Count > 0)
        {
            var element = Queue.Dequeue();
            Work(element);
        }
    }

    private ComponentFieldDefinition GetComponentDefinition(Node node)
    {
        if (!Definitions.Component.TryGetValue(node.Name, out var definition))
        {
            definition = new ComponentFieldDefinition(node.Name, null, null, node.Name);
            Definitions.AddComponent(definition);
        }

        return definition;
    }

    private void ComponentDeclaration(Node node)
    {
        // if there is no definition yet created for this component, then create one.
        var definition = GetComponentDefinition(node);
        var edge = node.Edges[0];
        if (_containedSets.TryGetValue(edge.Tail, out var parentSet))
        {

            var att = AsAttributeDict(node.Element);
            var containedComponent = new ContainedComponentField(definition, parentSet.Fields.Count, att["required"] == "Y", null);
            parentSet.Add(containedComponent);
            _containedSets[edge.Head] = definition;
        }
        else
        {
            throw new InvalidDataException($"edge {edge} tail has no contained set on which to place declared component");
        }
    }

    private void MessageDefinition(Node node)
    {
        var md = GetMessage(node.Element);
        Definitions.AddMessaqe(md);
        _containedSets[node.ID] = md;
        ExpandSet(node);
    }

    private void ComponentDefinition(Node node)
    {
        var definition = GetComponentDefinition(node);
        _containedSets[node.ID] = definition;
        ExpandSet(node);
    }

    private void InlineGroupDefinition(Node node)
    {
        var edge = node.Edges[0];
        if (_containedSets.TryGetValue(edge.Tail, out var parentSet))
        {
            var att = AsAttributeDict(node.Element);
            if (Definitions.Simple.TryGetValue(node.Name, out var noOFieldDefinition))
            {
                var name = att["name"];
                var definition =
                    new GroupFieldDefinition(name, null, null, noOFieldDefinition, name);
                var containedGroup = new ContainedGroupField(definition, parentSet.Fields.Count,
                    att["required"] == "Y", null);
                parentSet.Add(containedGroup);
                _containedSets[edge.Tail] = definition;
                var childNode = MakeNode(name, node.Element, ElementType.GroupDefinition);
                _containedSets[childNode.ID] = definition;
                node.MakeEdge(childNode.ID);
                childNode.MakeEdge(edge.Tail);
                ExpandSet(childNode);
            }
            else
            {
                throw new InvalidDataException($"{node.Name} does not exist in simple field definitions");
            }
        }
        else
        {
            throw new InvalidDataException($"edge {edge} tail has no contained set on which to place declared inline group");
        }
    }

    private void SimpleFieldDeclaration(Node node)
    {
        if (node.Edges.Count == 0)
        {
            Debugger.Break();
        }
        var edge = node.Edges[0];
        if (_containedSets.TryGetValue(edge.Tail, out var parentSet))
        {
            var att = AsAttributeDict(node.Element);
            if (_nodes.TryGetValue(edge.Head, out var simpleNode) &&
                Definitions.Simple.TryGetValue(simpleNode.Name, out var sd))
            {
                var containedSimpleField = new ContainedSimpleField(sd, parentSet.Fields.Count,
                    att["required"] == "Y",
                    false, null);
                parentSet.Add(containedSimpleField);
            }
            else
            {
                throw new InvalidDataException(
                    $"element {node} edge {edge} cannot be located as a simple field {simpleNode?.Name}");
            }
        }
        else
        {
            throw new InvalidDataException($"edge {edge} tail has no contained set on which to place declared field");
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
        foreach (var element in node.Element.Descendants())
        {
            switch (element.Name.LocalName)
            {
                case "field":
                {
                    ExpandField(node, element);
                    break;
                }

                case "group":
                {
                    ExpandGroup(node, element);
                    break;
                }

                case "component":
                {
                    ExpandComponent(node, element);
                    break;
                }
            }
        }
    }

    private void ExpandField(Node node, XElement element)
    {
        var at = AsAttributeDict(element);
        var childNode = MakeNode(at["name"], element, ElementType.SimpleFieldDeclaration);
        var edge = node.MakeEdge(childNode.ID);
        childNode.MakeEdge(node.ID);
        AddEdge(edge);
    }

    private void ExpandGroup(Node node, XElement element)
    {
        var at = AsAttributeDict(element);
        var name = at["name"];
        var inlinedFields = element.Descendants().ToList();
        if (inlinedFields.Count > 0)
        {
            var tailNode = MakeNode(name, element, ElementType.InlineGroupDefinition);
            var edge = node.MakeEdge(tailNode.ID);
            tailNode.MakeEdge(node.ID);
            AddEdge(edge);
        }
        else
        {
            var tailNode = MakeNode(name, element, ElementType.GroupDeclaration);
            var edge = node.MakeEdge(tailNode.ID);
            tailNode.MakeEdge(node.ID);
            AddEdge(edge);
        }
    }

    private void ExpandComponent(Node node, XElement element)
    {
        var at = AsAttributeDict(element);
        var inlinedFields = element.Descendants().ToList();
        var name = at["name"];
        if (inlinedFields.Count > 0)
        {
            var tailNode = MakeNode(name, element, ElementType.ComponentDefinition);
            var edge = node.MakeEdge(tailNode.ID);
            tailNode.MakeEdge(node.ID);
            AddEdge(edge);
        }
        else
        {
            var tailNode = MakeNode(name, element, ElementType.ComponentDeclaration);
            var edge = node.MakeEdge(tailNode.ID);
            var tailEdge = tailNode.MakeEdge(node.ID);
            AddEdge(edge);
            AddEdge(tailEdge);
        }
    }
}