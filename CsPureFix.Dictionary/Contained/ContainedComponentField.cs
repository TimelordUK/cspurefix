using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PureFix.Dictionary.Contained
{
    internal class ContainedComponentField(
        ComponentFieldDefinition? definition,
        int position,
        bool required,
        string? overrideName)
        : ContainedField(
            overrideName ?? definition?.Name ?? throw new ArgumentException("could not resolve a name for component"),
            position, ContainedFieldType.Component, required)
    {
        public ComponentFieldDefinition? Definition { get; } = definition;

        public override string ToString()
        {
            return Definition == null ? "" : $"[{Position}:{Required}]= C.{Definition.Fields.Count} ({Name})";
        }
    }
}
