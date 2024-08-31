using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Parser.QuickFix
{
    public partial class QuickFixXmlFileParser
    {
        public class Node
        {
            public record Edge(int Head, int Tail);
            public string Name { get; }
            public int ID { get; }
            public XElement Element { get; }
            public ElementType Type { get; }
            private readonly List<Edge> _edges = new();
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

        /*
         * a graph representing the data dictionary
         */
        private readonly Dictionary<int, Node> _nodes = new();
        private readonly Dictionary<int, List<Node.Edge>> _edges = new();
        /*
         * indexed on node id, this allows an edge to quickly find the set in which to place a field.
         */
        private readonly Dictionary<int, ContainedFieldSet> _containedSets = new();

        private int _nextId;

        private Node MakeNode(string name, XElement messageElement, Node.ElementType type)
        {
            var node = new Node(_nextId++, name, type, messageElement);
            _nodes[node.ID] = node;
            Queue.Enqueue(node);
            return node;
        }
    }
}
