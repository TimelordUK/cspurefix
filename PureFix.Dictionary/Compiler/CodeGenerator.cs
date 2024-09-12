using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Compiler
{
    public class CodeGenerator
    {
        private readonly StringBuilder _Builder = new StringBuilder();
        private int _TabCount = 0;

        public BlockIndent BeginBlock()
        {
            return new(this);
        }

        public BlockIndent BeginBlock(string line)
        {
            var indent = MakeIndent();
            _Builder.Append(indent).AppendLine(line);

            return new(this);
        }

        public CodeGenerator WriteLine()
        {
            var indent = MakeIndent();
            _Builder.AppendLine(indent);

            return this;
        }

        public CodeGenerator WriteLine(string line)
        {
            var indent = MakeIndent();
            _Builder.Append(indent).AppendLine(line);

            return this;
        }

        public void Reset()
        {
            _Builder.Clear();
            _TabCount = 0;
        }

        private string MakeIndent()
        {
            return new string('\t', _TabCount);
        }

        public override string ToString()
        {
            return _Builder.ToString();
        }

        public ref struct BlockIndent
        {
            private readonly CodeGenerator _CodeGenerator;

            public BlockIndent(CodeGenerator codeGenerator)
            {
                _CodeGenerator = codeGenerator;
                _CodeGenerator.WriteLine("{");
                _CodeGenerator._TabCount++;
            }

            public void Dispose()
            {
                _CodeGenerator._TabCount--;
                _CodeGenerator.WriteLine("}");
            }
        }
    }
}
