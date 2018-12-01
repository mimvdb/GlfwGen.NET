using System;
using System.IO;

namespace GlfwGenerator
{
    // Largely copied from the generator project for https://github.com/mellinoe/vk
    public class CsCodeWriter
    {
        private const string Indent = "    ";
        private readonly StreamWriter _sw;

        private int _indentLevel = 0;

        public CsCodeWriter(StreamWriter sw)
        {
            _sw = sw;
        }

        public void Using(string ns)
        {
            _sw.WriteLine($"using {ns};");
        }

        public void WriteHeader()
        {
            _sw.WriteLine("// <auto-generated>");
            _sw.WriteLine("// This file is generated by GlfwGenerator");
            _sw.WriteLine("// </auto-generated>");
        }

        public void PushBlock()
        {
            WriteLine('{');
            _indentLevel += 1;
        }

        public CodeIndent PushIndent()
        {
            return new CodeIndent(this);
        }

        public CodeBlock PushBlock(string ns)
        {
            return new CodeBlock(this, ns);
        }

        public IfDefSection PushIfDef(string condition)
        {
            return new IfDefSection(this, condition);
        }

        public void PopBlock()
        {
            _indentLevel -= 1;
            WriteLine('}');
        }

        public void PopIndent()
        {
            _indentLevel -= 1;
        }

        public void WriteLine()
        {
            _sw.WriteLine();
        }

        public void WriteLine(string text)
        {
            WriteIndentation();
            _sw.WriteLine(text);
        }

        public void WriteLine(char c)
        {
            WriteIndentation();
            _sw.WriteLine(c);
        }

        public void Write(string text)
        {
            _sw.Write(text);
        }

        public void WriteIndentation()
        {
            for (int i = 0; i < _indentLevel; i++)
            {
                _sw.Write(Indent);
            }
        }

        public class CodeBlock : IDisposable
        {
            private readonly CsCodeWriter _cw;

            public CodeBlock(CsCodeWriter cw, string header)
            {
                _cw = cw;
                _cw.WriteLine(header);
                _cw.PushBlock();
            }

            public void Dispose()
            {
                _cw.PopBlock();
            }
        }

        public class CodeIndent : IDisposable
        {
            private readonly CsCodeWriter _cw;

            public CodeIndent(CsCodeWriter cw)
            {
                _cw = cw;
                _cw._indentLevel++;
            }

            public void Dispose()
            {
                _cw.PopIndent();
            }
        }

        public class IfDefSection : IDisposable
        {
            private readonly CsCodeWriter _cw;
            private readonly string _condition;

            public IfDefSection(CsCodeWriter cw, string condition)
            {
                _cw = cw;
                _condition = condition;
                _cw.WriteLine($"#if {condition}");
            }

            public void Dispose()
            {
                _cw.WriteLine($"#endif // {_condition}");
            }
        }
    }
}
