using System;
using System.Linq;

namespace GlfwGenerator
{
    public class GlfwDelegate : ICsWritable
    {
        private readonly string name;
        private readonly ParameterList parameters;

        public GlfwDelegate(string line)
        {
            string[] parts = line.Trim().Split(new char[]{'(', ')'}, StringSplitOptions.RemoveEmptyEntries);

            parameters = new ParameterList(parts[2]);
            name = parts[1];
            if (name.StartsWith("* "))
            {
                name = name.Substring(2);
            }
            else if (name.StartsWith("*"))
            {
                name = name.Substring(1);
            }
        }

        public void WriteCS(CsCodeWriter cw, TypeNameMappings tnm)
        {
            cw.WriteLine($"public unsafe delegate void {name}({parameters.ToString(tnm)});");
        }
    }
}
