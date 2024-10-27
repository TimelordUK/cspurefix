using PureFix.Dictionary.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Contained
{
    public interface IContainedSet
    {
        /**
         * index of name to any group that may be present within the field list
         */
        IReadOnlyDictionary<string, IContainedSet> Groups { get; }

        /**
         * index of name to any component that may be present within the field list
         */
        IReadOnlyDictionary<string, IContainedSet> Components { get; }

        /**
        * index of name to any simple field that may be present within the field list
        */
        IReadOnlyDictionary<string, ContainedSimpleField> Simple { get; }

        /**
         *  sequence of fields representing this type - can be simple, group or component
         */
        IReadOnlyList<ContainedField> Fields { get; }

        /**
         * any tag at any level i.e. does this set contain a tag
         */
        IReadOnlyDictionary<int, bool> ContainedTag { get; }

        /**
         * any tag at any level ordered i.e. all tags flattened to list
         */
        IReadOnlyList<int> FlattenedTag { get; }

        /**
         * any data tags contained length within this set.
         */
        IReadOnlyDictionary<int, bool> ContainedLength { get; }

        /**
         * tags only in repository at this level, not from any at deeper levels
         */
        IReadOnlyDictionary<int, ContainedSimpleField> LocalTag { get; }

        /**
         * tags marked required at this level only
         */
        IReadOnlyDictionary<int, ContainedSimpleField> LocalRequired { get; }

        /**
         * all tags contained within this field set flattened from all levels
         */
        IReadOnlyDictionary<int, ContainedSimpleField> TagToSimple { get; }

        /**
         * direct any tag contained within this set to field one level down where it belongs.
         */
        IReadOnlyDictionary<int, (IContainedSet parent, ContainedField field)> TagToField { get; }

        /**
         * only repository directly in this set indexed by name
         */
        IReadOnlyDictionary<string, ContainedField> LocalNameToField { get; }

        /**
         * for FixMl notation this set of fields appear as attributes i.e. <Pty ID="323" R="38">
         */
        IReadOnlyDictionary<string, ContainedSimpleField> NameToLocalAttribute { get; }

        /**
         * all attributes in order of being declared
         */
        IReadOnlyList<ContainedSimpleField> LocalAttribute { get; }

        ContainedSetType Type { get; }
        string Name { get; }
        string? Category { get; }
        string? Abbreviation { get; }
        string Description { get; }

        /**
         * at any level on this set, first declared simple field
         */
        ContainedSimpleField? FirstSimple { get; }

        bool ContainsRaw { get; }

        string GetPrefix();

        string GetFieldName(int tag);
        IContainedSet? GetSet(string path);

        IReadOnlyList<int> Keys();
        void Index();
    }
}
