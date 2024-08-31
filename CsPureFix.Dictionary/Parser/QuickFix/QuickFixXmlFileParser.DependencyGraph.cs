using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Parser.QuickFix
{
    public partial class QuickFixXmlFileParser
    {
        private Node MakeNode(string name, XElement messageElement, Node.ElementType type)
        {
            var node = new Node(_nextId++, name, type, messageElement);
            _nodes[node.ID] = node;
            Queue.Enqueue(node);
            return node;
        }
    }
}
