using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PureFix.Dictionary.Contained;
using static PureFix.Dictionary.Parser.QuickFix.QuickFixXmlFileParser.Node;

namespace PureFix.Dictionary.Parser.QuickFix
{
    public partial class QuickFixXmlFileParser
    {
        public class Node(int id, string name, ElementType elementType, XElement element)
        {
            public readonly record struct Edge(int Head, int Tail);
            public string Name { get; } = name;
            public int ID { get; } = id;
            public XElement Element { get; } = element;
            public ElementType Type { get; } = elementType;
            private readonly List<Edge> _edges = [];
            public IReadOnlyList<Edge> Edges => _edges;
            public IReadOnlyDictionary<string, string> AsAttributeDict() => Element.AsAttributeDict();
            public bool IsRequired() => Name == "StandardHeader" || 
                                        Name == "StandardTrailer" || 
                                        (AsAttributeDict().TryGetValue("required", out var val) && val == "Y");

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

        /*
         * a graph representing the data dictionary
         */
        private readonly Dictionary<int, Node> _nodes = [];
        private readonly Dictionary<int, List<Edge>> _edges = [];
        /*
         * indexed on node id, this allows an edge to quickly find the set in which to place a field.
         */
        private readonly Dictionary<int, ContainedFieldSet> _containedSets = [];
        private Node? _header;
        private Node? _trailer;

        private int _nextId;

        private void AddEdge(Edge edge)
        {
            if (!_edges.TryGetValue(edge.Head, out var pCached))
            {
                _edges[edge.Head] = pCached = [];
            }
            pCached.Add(edge);

            if (!_edges.TryGetValue(edge.Tail, out var cCached))
            {
                _edges[edge.Tail] = cCached = [];
            }
            cCached.Add(edge);
        }

        private void ConstructTailNode(string name, Node headNode, XElement element, ElementType type)
        {
            var tailNode = MakeNode(name, element, type);
            var edge = headNode.MakeEdge(tailNode.ID);
            tailNode.MakeEdge(headNode.ID);
            AddEdge(edge);
        }

        private Node MakeNode(string name, XElement messageElement, ElementType type)
        {
            var node = new Node(_nextId++, name, type, messageElement);
            _nodes[node.ID] = node;
            Queue.Enqueue(node);
            return node;
        }
    }
}
