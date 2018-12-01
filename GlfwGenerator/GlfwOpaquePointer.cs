using System;

namespace GlfwGenerator
{
    public class GlfwOpaquePointer : ICsWritable
    {
        private readonly string structName;
        private readonly string memberName;

        public GlfwOpaquePointer(string line)
        {
            string[] parts = line.Trim().Split(new char[]{}, StringSplitOptions.RemoveEmptyEntries);
            this.structName = parts[2];
            this.memberName = MemberName();
        }

        private string MemberName()
        {
            if (structName.StartsWith("GLFW"))
            {
                return structName.Substring(4);
            }
            else
            {
                return structName.Substring(0, 1).ToLower() + structName.Substring(1);
            }
        }

        public void WriteCS(CsCodeWriter cw, TypeNameMappings tnm)
        {
            tnm.AddMapping(structName+"*", structName);
            using (cw.PushBlock($"public struct {structName}"))
            {
                cw.WriteLine("public readonly IntPtr IntPtr;");
                cw.WriteLine();
                using (cw.PushBlock($"public {structName}(IntPtr pointer)"))
                {
                    cw.WriteLine("IntPtr = pointer;");
                }
                cw.WriteLine($"public static implicit operator IntPtr({structName} {memberName}) => {memberName}.IntPtr;");
                cw.WriteLine($"public static implicit operator {structName}(IntPtr pointer) => new {structName}(pointer);");
            }
        }
    }
}
