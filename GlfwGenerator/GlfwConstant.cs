using System;
using System.Linq;

namespace GlfwGenerator
{
    public class GlfwConstant : ICsWritable
    {
        private readonly string outputLine;

        /// <summary>
        /// Generate constant from #define line
        /// </summary>
        /// <param name="line"></param>
        public GlfwConstant(string line)
        {
            string[] parts = line.Trim().Split(new char[]{}, StringSplitOptions.RemoveEmptyEntries);
            this.outputLine = $"public const int {parts[1]} = {parts[2]};";
            if (parts.Length > 3)
            {
                this.outputLine += $" {string.Join(" ", parts.Skip(3))}";
            }
        }

        public override string ToString()
        {
            return outputLine;
        }

        public void WriteCS(CsCodeWriter cw, TypeNameMappings tnm)
        {
            cw.WriteLine(ToString());
        }
    }
}
