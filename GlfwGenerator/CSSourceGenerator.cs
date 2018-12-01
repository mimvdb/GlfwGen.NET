using System.Collections.Generic;
using System.IO;

namespace GlfwGenerator
{
    public class CSSourceGenerator
    {
        private const string Namespace = "GlfwGen";
        private const string Class = "GlfwNative";

        public static void Generate(string outputPath, GlfwModel model)
        {
            TypeNameMappings tnm = new TypeNameMappings();
            GenerateFile(Path.Combine(outputPath, "Glfw.Constants.gen.cs"), model.Constants, new string[] {}, true, tnm);
            GenerateFile(Path.Combine(outputPath, "Glfw.Handles.gen.cs"), model.Handles, new string[] {"System"}, false, tnm);
            GenerateFile(Path.Combine(outputPath, "Glfw.Delegates.gen.cs"), model.Delegates, new string[] {"System"}, true, tnm);

            tnm.AddMapping("char*", "string");
            GenerateFile(Path.Combine(outputPath, "Glfw.Functions.gen.cs"), model.Functions, new string[] {"System", "System.Runtime.InteropServices"}, true, tnm);
        }

        private static void GenerateFile<T>(string path, List<T> csWritables, string[] usings, bool inClass, TypeNameMappings tnm) where T : ICsWritable
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                CsCodeWriter cw = new CsCodeWriter(sw);
                cw.WriteHeader();
                foreach (var use in usings)
                {
                    cw.Using(use);
                }
                cw.WriteLine();

                using (cw.PushBlock($"namespace {Namespace}"))
                {
                    if (inClass)
                    {
                        using (cw.PushBlock($"public static unsafe partial class {Class}"))
                        {
                            foreach (var writable in csWritables)
                            {
                                writable.WriteCS(cw, tnm);
                            }
                        }
                    }
                    else
                    {
                        foreach (var writable in csWritables)
                        {
                            writable.WriteCS(cw, tnm);
                        }
                    }
                }
            }
        }
    }
}
