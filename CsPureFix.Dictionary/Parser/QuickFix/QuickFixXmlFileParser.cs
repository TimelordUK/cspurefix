using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using static PureFix.Dictionary.Parser.QuickFix.QuickFixXmlFileParser.Node;

namespace PureFix.Dictionary.Parser.QuickFix;

public partial class QuickFixXmlFileParser
{
    public FixDefinitions Definitions { get; }
    public Queue<Node> Queue { get; } = new ();
    
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

            /*
             * any message is a set of fields where each field may be simple, component or a group
             * where components and groups may recursively contain other sets.
             * in quickfix notation the header and trailer are not included in each definition so
             * must be added.
             */
            case ElementType.MessageDefinition:
            {
                MessageDefinition(node);
                break;
            }

            /*
             * an actual component definition (which may itself contain components and groups)
             * these are referenced as declaration fields.
             */
            case ElementType.ComponentDefinition:
            {
                ComponentDefinition(node);
                break;
            }

            /*
             * the edge will have head as the declared field and tail to the set in which the declaration should be added.
             * at this point all field definitions will exist.
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

            /*
             * this is a reference to a component that is defined elsewhere - there will only be one
             * definition instance, yet it may be included in many sets.
             */
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
        // all top level components which will later be further expanded
        ParseComponents(doc);
        ParseHeader(doc);
        ParseTrailer(doc);
        // can now resolve message types
        ParseMessages(doc);
        // keep expanding and resolving until every set is fully resolved.
        while (Queue.Count > 0)
        {
            var element = Queue.Dequeue();
            Work(element);
        }
    }

    private ComponentFieldDefinition GetComponentDefinition(Node node)
    {
        /*
         * regardless of how many times the component is used, only 1 definition is held
         * the context is captured in a contained field.
         */
        if (Definitions.Component.TryGetValue(node.Name, out var definition)) return definition;
        definition = new ComponentFieldDefinition(node.Name, null, null, node.Name);
        Definitions.AddComponent(definition);

        return definition;
    }

    private void ComponentDeclaration(Node node)
    {
        // if there is no definition yet created for this component, then create one.
        var definition = GetComponentDefinition(node);
        var edge = node.Edges[0];
        if (_containedSets.TryGetValue(edge.Tail, out var parentSet))
        {
            var containedComponent = new ContainedComponentField(definition, parentSet.Fields.Count, node.IsRequired(), null);
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
        // need to wrap the expanded set in a header and trailer as this is assumed within the quick fix xml.
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
        if (node.Edges.Count == 0)
        {
            throw new InvalidDataException($"node {node} has no edges to find tail for inline group definition.");
        }
        var edge = node.Edges[0];
        if (_containedSets.TryGetValue(edge.Tail, out var parentSet))
        {
            // there must be a backing simple field definition representing the no in the group.
            var att = node.AsAttributeDict();
            if (Definitions.Simple.TryGetValue(node.Name, out var noOFieldDefinition))
            {
                var name = att["name"];
                var definition =
                    new GroupFieldDefinition(name, null, null, noOFieldDefinition, name);
                var containedGroup = new ContainedGroupField(definition, parentSet.Fields.Count, node.IsRequired(), null);
                parentSet.Add(containedGroup);
                _containedSets[edge.Head] = definition;
                var childNode = MakeNode(name, node.Element, ElementType.GroupDefinition);
                // the definition has a unique node in graph, but only exists within the context of the container in which 
                // it is defined, unlike components which are globally scoped.
                _containedSets[childNode.ID] = definition;
                node.MakeEdge(childNode.ID);
                childNode.MakeEdge(edge.Tail);
                ExpandSet(childNode);
            }
            else
            {
                throw new InvalidDataException($"{node.Name} does not exist in simple field definitions to construct inline group");
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
            throw new InvalidDataException($"node {node} has no edges to find tail for simple field declaration.");
        }
        var edge = node.Edges[0];
        if (_containedSets.TryGetValue(edge.Tail, out var parentSet))
        {
            if (_nodes.TryGetValue(edge.Head, out var simpleNode) &&
                Definitions.Simple.TryGetValue(simpleNode.Name, out var sd))
            {
                var containedSimpleField = new ContainedSimpleField(sd, parentSet.Fields.Count, node.IsRequired(), false, null);
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

    /*
     * a group or collection needs to be further expanded into its constituents,
     * groups and fields will later arrive once more to further resolve deeper into
     * the definition.
     */
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

    /*
     * this is a declared field which can not be further resolved - a definition to this field
     * would already be resolved providing it actually exists in the XML, in this case add a node
     * which will place the declared field in its owning set at tail of the new edge.
     */

    private void ExpandField(Node node, XElement element)
    {
        var at = element.AsAttributeDict();
        ConstructTailNode(at["name"], node, element, ElementType.SimpleFieldDeclaration);
    }

    private void ExpandGroup(Node node, XElement element)
    {
        var at = element.AsAttributeDict();
        var name = at["name"];
        var inlinedFields = element.Descendants();
        var elementType = inlinedFields.Any() ? ElementType.InlineGroupDefinition : ElementType.GroupDeclaration;
        ConstructTailNode(name, node, element, elementType);
    }

    private void ExpandComponent(Node node, XElement element)
    {
        var at = element.AsAttributeDict();
        var name = at["name"];
        var inlinedFields = element.Descendants();
        var elementType = inlinedFields.Any() ? ElementType.ComponentDefinition : ElementType.ComponentDeclaration;
        ConstructTailNode(name, node, element, elementType);
    }
}