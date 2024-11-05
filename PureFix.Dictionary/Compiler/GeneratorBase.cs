using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Parser;

namespace PureFix.Dictionary.Compiler
{
    public abstract class GeneratorBase
    {
        private readonly string m_Root;
        private readonly Options m_Options;
        private readonly IFixDefinitions m_FixDefinitions;

        private readonly HashSet<string> m_FilesGenerated = new(StringComparer.OrdinalIgnoreCase);

        protected GeneratorBase(string root, IFixDefinitions fixDefinitions, Options options)
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
                Process(message);
            }
            PostProcess();
        }

        public void Process(MessageDefinition message)
        {
            var filename = MakeFilename(message.Name, message);
            if (m_FilesGenerated.Add(filename))
            {
                var content = GenerateType(message);
                WriteFile(filename, content);
            }
        }

        public virtual void PostProcess() { }

        private void Process(string parentPath, ContainedGroupField field)
        {
            if (field.Definition == null) return;
            var filename = MakeFilename(parentPath, field.Definition);
            if(m_FilesGenerated.Add(filename))
            {
                var content = GenerateType(parentPath, field);
                WriteFile(filename, content);
            }
        }

        private void Process(string parentPath, ContainedComponentField component)
        {
            if (component.Definition == null) return;
            var filename = MakeFilename(parentPath, component.Definition);
            if(m_FilesGenerated.Add(filename))
            {
                var content = GenerateType(parentPath, component);
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

        protected IFixDefinitions FixDefinitions
        {
            get{return m_FixDefinitions;}
        }

        protected void ApplyFields(CodeGenerator generator, string parentPath, IContainedSet set)
        {
            ContainedField? last = null;
            var fields = set.Fields;
            for(int i = 0; i < fields.Count; i++)
            {
                var field = fields[i];
                var next = (i == fields.Count - 1 ? null : fields[i + 1]);

                if(field is ContainedSimpleField simpleField)
                {
                    HandleFieldProperty(generator, parentPath, i, simpleField, last, next);
                }
                else if(field is ContainedComponentField componentField)
                {
                    HandleComponentProperty(generator, parentPath + set.Name, i, componentField);
                    Process(parentPath + set.Name, componentField);
                }
                else if(field is ContainedGroupField groupField)
                {
                    HandleGroupProperty(generator, parentPath + set.Name, i, groupField);
                    Process(parentPath + set.Name, groupField);
                }

                last = field;
            }
        }

        protected abstract void HandleGroupProperty(CodeGenerator generator, string parentPath, int index, ContainedGroupField field);
        protected abstract void HandleComponentProperty(CodeGenerator generator, string parentPath, int index, ContainedComponentField field);
        protected abstract void HandleFieldProperty(CodeGenerator generator, string parentPath, int index, ContainedSimpleField field, ContainedField? last, ContainedField? next);

        protected abstract string GenerateType(MessageDefinition message);
        protected abstract string GenerateType(string parentPath, ContainedComponentField component);
        protected abstract string GenerateType(string parentPath, ContainedGroupField group);

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

        protected string MakeFilename(string parentPath, IContainedSet set)
        {
            var typename = MakeTypeName(parentPath, set);

            if(set is MessageDefinition)
            {
                return Path.Combine(m_Root, typename + ".cs");
            }

            return Path.Combine(m_Root, "Types", typename + ".cs");
        }

        protected string MakeEnumFilename(string enumName)
        {
            return Path.Combine(m_Root, "Types", enumName + ".cs");
        }

        protected string MakeTypeName(string parentPath, IContainedSet set)
        {
            if (set.Type == ContainedSetType.Component)
            {
                return set.Name + "Component";
            }
            if (set.Type == ContainedSetType.Group)
            {
                return $"{parentPath}{set.Name}";
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
