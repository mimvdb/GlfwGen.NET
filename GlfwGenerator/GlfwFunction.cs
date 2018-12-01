using System;

namespace GlfwGenerator
{
    public class GlfwFunction : ICsWritable
    {
        private readonly string name;
        private readonly string returnType;
        private readonly ParameterList parameters;

        public GlfwFunction(string line)
        {
            line = line.Substring(line.IndexOf("GLFW_API") + 8);

            int end = line.IndexOf(';');
            int startFunctionName = line.LastIndexOf(' ', line.IndexOf('('));

            returnType = line.Substring(0, startFunctionName).Trim();
            if (returnType.StartsWith("const "))
            {
                returnType = returnType.Substring(6);
            }

            string[] paramSplit = line.Substring(startFunctionName, end - startFunctionName).Split(new char[]{'(', ')'}, StringSplitOptions.RemoveEmptyEntries);
            name = paramSplit[0].Trim();

            parameters = new ParameterList(paramSplit[1], true);
        }

        public void WriteCS(CsCodeWriter cw, TypeNameMappings tnm)
        {
            var returnType = tnm.GetMappedName(this.returnType);

            cw.WriteLine("[UnmanagedFunctionPointer(CallingConvention.Cdecl)]");
            cw.WriteLine($"private delegate {returnType} {name}_t({parameters.ToString(tnm)});");
            cw.WriteLine($"private static {name}_t s_{name} = LoadFunction<{name}_t>(\"{name}\");");
            cw.WriteLine($"public static {returnType} {name}({parameters.ToString(tnm)}) => s_{name}({parameters.ToNames()});");
        }
    }
}
