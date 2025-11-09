using PureFix.Examples.Skeleton;
using PureFix.Examples.TradeCapture;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;

namespace PureFix.Test.ModularTypes.Env
{
    internal class SetConstraintHelper(IFixDefinitions definitions)
    {
        public IFixDefinitions Definitions { get; } = definitions;

        public void IsComponent(IContainedSet set, int index, string name, bool expected)
        {
            Assert.Multiple(() =>
            {
                Assert.That(set, Is.Not.Null);
                var field = set.Fields[index];
                Assert.That(field.Type, Is.EqualTo(ContainedFieldType.Component));
                Assert.That(field.Name, Is.EqualTo(name));
                Assert.That(field.Required, Is.EqualTo(expected));
                Assert.That(set.Components.ContainsKey(name));
            });
        }

        public void IsGroup(IContainedSet set, int index, string name, bool expected)
        {
            Assert.Multiple(() =>
            {
                Assert.That(set, Is.Not.Null);
                var field = set.Fields[index];
                Assert.That(field.Type, Is.EqualTo(ContainedFieldType.Group));
                Assert.That(field.Name, Is.EqualTo(name));
                Assert.That(field.Required, Is.EqualTo(expected));
                Assert.That(set.Groups.ContainsKey(name));
            });
        }

        public void IsSimple(IContainedSet set, int index, string name, bool expected)
        {
            Assert.Multiple(() =>
            {
                Assert.That(set, Is.Not.Null);
                var field = set?.Fields[index];
                Assert.That(field, Is.Not.Null);
                Assert.That(field.Type, Is.EqualTo(ContainedFieldType.Simple));
                Assert.That(field.Name, Is.EqualTo(name));
                Assert.That(field.Required, Is.EqualTo(expected));
                Assert.That(Definitions.Simple.TryGetValue(name, out var masterDef), Is.True);
                Assert.That(masterDef, Is.Not.Null);
                Assert.That(masterDef.Name, Is.EqualTo(name));
                var tag = masterDef.Tag;
                Assert.That(set.ContainedTag.ContainsKey(tag));
                Assert.That(set.Simple.ContainsKey(name));
                Assert.That(set.LocalTag.ContainsKey(tag));
                Assert.That(set.TagToSimple.ContainsKey(tag));
                Assert.That(set.LocalNameToField.ContainsKey(name));
            });
        }

        public void IsEnum(IReadOnlyDictionary<string, FieldEnum> enums, string key, string expectedVal,
            string expectedDescription)
        {
            Assert.Multiple(() =>
            {
                Assert.That(enums, Is.Not.Null);
                Assert.That(enums.TryGetValue(key, out var en), Is.True);
                Assert.That(en?.Val, Is.EqualTo(expectedVal));
                Assert.That(en?.Description, Is.EqualTo(expectedDescription));
                Assert.That(en?.Key, Is.EqualTo(key));
            });
        }
    }
}
