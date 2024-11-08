using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using static PureFix.Dictionary.Parser.QuickFix.QuickFixXmlFileParser.Node;

namespace PureFix.Dictionary.Parser.QuickFix;

public partial class QuickFixXmlFileParser(IFixDefinitions definitions)
{
    public IFixDefinitions Definitions { get; } = definitions;
    public Queue<Node> Queue { get; } = new ();

    private void ParseVersion(XDocument doc)
    {
        var version = doc.Descendants("fix").First();
        var atts = version.AsAttributeDict();
        var major = int.Parse(atts.GetValueOrDefault("major") ?? "0");
        var minor = int.Parse(atts.GetValueOrDefault("minor") ?? "0");
        var servicepack = int.Parse(atts.GetValueOrDefault("servicepack") ?? "0");
        var description = major != 5 || servicepack == 0
            ? $"FIX.{major}.{minor}"
            : $"FIX.{major}.{minor}SP{servicepack}";
        Definitions.SetVersion(VersionUtil.Resolve(description));
    }

    public void Parse(string path)
    {
        using var streamReader = File.OpenText(path);
        var txt = streamReader.ReadToEnd();
        ParseText(txt);
    }

    public void ParseText(string xml)
    {
        // first parse all fields including their enum definitions, and add to the dictionary
        var doc = XDocument.Parse(xml);
        ParseVersion(doc);
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
            Work(Queue.Dequeue());
        }
        /*
         * At this point all fields on all sets are placed, however the parent to say an instrument
         * is not aware of the tags contained in these sub fields, this indexer will start from every
         * message and re-index itself and all child components and groups such that at each level
         * they contain all tags below them - this is essential for the segment parser to work.
         */
        var indexer = new IndexVisitor();
        indexer.Compute(Definitions);
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
             * with a group set defined, it is now expanded and populated with fields.
             */
            case ElementType.GroupDefinition:
            {
                GroupDefinition(node);
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
        if (_header is null) throw new InvalidOperationException("header not set");
        if (_trailer is null) throw new InvalidOperationException("trailer not set");

        var md = GetMessage(node.Element);
        Definitions.AddMessage(md);
        _containedSets[node.ID] = md;
        // need to wrap the expanded set in a header and trailer as this is assumed within the quick fix xml.
        ConstructTailNode("StandardHeader", node, _header.Element, ElementType.ComponentDeclaration);
        ExpandSet(node);
        ConstructTailNode("StandardTrailer", node, _trailer.Element, ElementType.ComponentDeclaration);
    }

    private void ComponentDefinition(Node node)
    {
        var definition = GetComponentDefinition(node);
        _containedSets[node.ID] = definition;
        ExpandSet(node);
    }

    private void GroupDefinition(Node node)
    {
        if (_containedSets.TryGetValue(node.Edges[0].Tail, out var definition))
        {
            _containedSets[node.Edges[0].Head] = definition;
            ExpandSet(node);
        }
        else
        {
            throw new InvalidDataException($"node {node} has no edges to find tail for group definition.");
        }
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
                var name = NameFrom(att);
                var definition = new GroupFieldDefinition(name, null, null, noOFieldDefinition, name);
                var containedGroup = new ContainedGroupField(definition, parentSet.Fields.Count, node.IsRequired(), null);
                parentSet.Add(containedGroup);
                _containedSets[edge.Head] = definition;
                ConstructTailNode(name, node, node.Element, ElementType.GroupDefinition);
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

    /*
     * a group or collection needs to be further expanded into its constituents,
     * groups and fields will later arrive once more to further resolve deeper into
     * the definition.
     */
    public void ExpandSet(Node node)
    {
        foreach (var element in node.Element.Elements())
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
        ConstructTailNode(NameFrom(at), node, element, ElementType.SimpleFieldDeclaration);
    }

    private void ExpandGroup(Node node, XElement element)
    {
        ExpandSet(node, element, ElementType.InlineGroupDefinition, ElementType.GroupDeclaration);
    }

    private void ExpandComponent(Node node, XElement element)
    {
        ExpandSet(node, element, ElementType.ComponentDefinition, ElementType.ComponentDeclaration);
    }

    /*
     * if declaring, then a definition node should exist
     */

    private void ExpandSet(Node node, XElement element, ElementType defineElement, ElementType declareElement)
    {
        var at = element.AsAttributeDict();
        var name = NameFrom(at);
        var inlinedFields = element.Elements();
        var elementType = inlinedFields.Any() ? defineElement : declareElement;
        ConstructTailNode(name, node, element, elementType);
    }
}