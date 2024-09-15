using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PureFix.Dictionary.Compiler.MsgCompiler;

namespace PureFix.Dictionary.Compiler
{
    public abstract class BaseParserCompiler
    {
        public FixDefinitions Definitions { get; }
        public Options CompilerOptions { get; }
        private readonly Queue<CompilerType> _workQueue = [];
        private readonly Dictionary<string, CompilerType> _completed = [];
        protected readonly CodeGenerator _builder = new();
        protected CompilerType? _currentCompilerType;

        protected BaseParserCompiler(FixDefinitions definitions, Options? options = null)
        {
            Definitions = definitions;
            options ??= Options.FromVersion(definitions);
            CompilerOptions = options;
        }

        public void Generate(IReadOnlyList<string>? types = null)
        {
            types ??= CompilerOptions.MsgTypes;
            if (types == null) throw new InvalidDataException("no types defined to create");
            CreateTypes(types);
        }

        protected void Enqueue(CompilerType ct)
        {
            var fullName = GetFileName(ct);
            if (_completed.ContainsKey(fullName))
            {
                return;
            }

            _workQueue.Enqueue(ct);
            _completed[fullName] = ct;
        }

        public void CreateTypes(IReadOnlyList<string> types)
        {
            foreach (var type in types)
            {
                var set = Definitions.GetMsgOrComponent(type);
                if (set == null)
                {
                    throw new InvalidDataException($"no type {type} defined");
                }

                var ct = new CompilerType(Definitions, CompilerOptions, set, set.Name);
                Enqueue(ct);
            }

            Work();
        }

        private void Work()
        {
            while (_workQueue.Count > 0)
            {
                var compilerType = _workQueue.Dequeue();
                var compiledType = GenerateTypes(compilerType);
                var fullName = GetFileName(compilerType);

                WriteFile(fullName, compiledType);
            }
        }

        protected abstract string GenerateTypes(CompilerType compilerType);
        protected abstract string GetFileName(CompilerType ct);

        protected void WriteFile(string filename, string content)
        {
            var directory = Path.GetDirectoryName(filename);
            if (directory is not null) Directory.CreateDirectory(directory);

            File.WriteAllText(filename, content);
        }
    }
}
