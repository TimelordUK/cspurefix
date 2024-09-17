using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Compiler
{
    public abstract class GeneratorBase
    {
        private string m_Root;
        private readonly Options m_Options;
        private readonly FixDefinitions m_FixDefinitions;

        private readonly HashSet<string> m_FilesGenerated = new(StringComparer.OrdinalIgnoreCase);

        public GeneratorBase(string root, FixDefinitions fixDefinitions, Options options)
        {
            m_Root = root;
            m_FixDefinitions = fixDefinitions;
            m_Options = options;
        }

        public ISet<string> FilesGenerated
        {
            get{return m_FilesGenerated;}
        }

        public void Process()
        {
            foreach(var message in FixDefinitions.Message.Values)
            {
                var filename = MakeFilename(message);
                if(m_FilesGenerated.Add(filename))
                {
                    var content = GenerateType(message);
                    WriteFile(filename, content);
                }
            }
        }

        private void Process(ContainedGroupField field)
        {
            var filename = MakeFilename(field);
            if(m_FilesGenerated.Add(filename))
            {
                var content = GenerateType(field);
                WriteFile(filename, content);
            }
        }

        private void Process(ContainedComponentField component)
        {
            var filename = MakeFilename(component);
            if(m_FilesGenerated.Add(filename))
            {
                var content = GenerateType(component);
                WriteFile(filename, content);
            }
        }

        protected static string SelectRoot(string? root, string other)
        {
            return root ?? other;
        }

        protected Options Options
        {
            get{return m_Options;}
        }

        protected FixDefinitions FixDefinitions
        {
            get{return m_FixDefinitions;}
        }

        protected void ApplyFields(CodeGenerator generator, IReadOnlyList<ContainedField> fields)
        {
            ContainedField? last = null;
            for(int i = 0; i < fields.Count; i++)
            {
                var field = fields[i];
                var next = (i == fields.Count - 1 ? null : fields[i + 1]);

                if(field is ContainedSimpleField simpleField)
                {
                    HandleFieldProperty(generator, i, simpleField, last, next);
                }
                else if(field is ContainedComponentField componentField)
                {
                    HandleComponentProperty(generator, i, componentField);
                    Process(componentField);
                }
                else if(field is ContainedGroupField groupField)
                {
                    HandleGroupProperty(generator, i, groupField);
                    Process(groupField);
                }

                last = field;
            }
        }

        protected abstract void HandleGroupProperty(CodeGenerator generator, int index, ContainedGroupField field);
        protected abstract void HandleComponentProperty(CodeGenerator generator, int index, ContainedComponentField field);
        protected abstract void HandleFieldProperty(CodeGenerator generator, int index, ContainedSimpleField field, ContainedField? last, ContainedField? next);

        protected abstract string GenerateType(MessageDefinition message);
        protected abstract string GenerateType(ContainedComponentField component);
        protected abstract string GenerateType(ContainedGroupField group);

        protected void WriteMessageUsings(CodeGenerator generator)
        {
            foreach(var use in m_Options.DefaultUsing)
            {
                generator.WriteLine($"using {use};");
            }

            generator.WriteLine($"using {m_Options.BackingTypeNamespace}.Types;");
        }

        protected void WriteUsings(CodeGenerator generator)
        {
            foreach(var use in m_Options.DefaultUsing)
            {
                generator.WriteLine($"using {use};");
            }
        }

        protected string MakeFilename(IContainedSet set)
        {
            var typename = MakeTypeName(set);

            if(set is MessageDefinition)
            {
                return Path.Combine(m_Root, typename + ".cs");
            }

            return Path.Combine(m_Root, "Types", typename + ".cs");
        }

        protected string MakeFilename(ContainedField group)
        {
            var typename = MakeTypeName(group);
            return Path.Combine(m_Root, "Types", typename + ".cs");
        }

        protected string MakeEnumFilename(string enumName)
        {
            return Path.Combine(m_Root, "Types", enumName + ".cs");
        }

        protected string MakeTypeName(ContainedField set)
        {
            if(set is ContainedComponentField component)
            {
                return component.Definition!.Name + "Component";
            }

            return set.Name;
        }

        protected string MakeTypeName(IContainedSet set)
        {
            if(set is ContainedComponentField component)
            {
                return component.Definition!.Name + "Component";
            }

            return set.Name;
        }

        protected string MapRequired(bool value)
        {
            return value ? "true" : "false";
        }

        protected void WriteFile(string filename, string content)
        {
            var directory = Path.GetDirectoryName(filename);
            if(directory is not null) Directory.CreateDirectory(directory);

            File.WriteAllText(filename, content);
        }
    }
}
