using System;
using System.Collections.Generic;

namespace GlfwGenerator
{
    public class TypeNameMappings
    {
        private readonly Dictionary<string, string> _nameMappings = new Dictionary<string, string>()
        {
            { "uint8_t", "byte" },
            { "uint32_t", "uint" },
            { "uint32_t*", "uint*" },
            { "unsigned int", "uint" },
            { "uint64_t", "ulong" },
            { "int32_t", "int" },
            { "VkResult", "int" },
            { "int64_t", "long" },
            { "int64_t*", "long*" },
            { "char", "byte" },
            { "char*", "byte*" },
            { "char**", "byte**" },
            { "unsigned char", "byte" },
            { "unsigned char*", "byte*" },
            { "size_t", "UIntPtr" },
            { "DWORD", "uint" },
            { "VkInstance", "IntPtr" },
            { "VkSurfaceKHR*", "void*" },
            { "VkPhysicalDevice", "IntPtr" },
            { "VkAllocationCallbacks*", "IntPtr" },
        };

        internal IEnumerable<KeyValuePair<string, string>> GetAllMappings()
        {
            return _nameMappings;
        }

        public void AddMapping(string originalName, string newName)
        {
            _nameMappings.Remove(originalName);
            _nameMappings.Add(originalName, newName);
        }

        public string GetMappedName(string name)
        {
            if (_nameMappings.TryGetValue(name, out string mappedName))
            {
                return GetMappedName(mappedName);
            }
            else if (name.StartsWith("PFN"))
            {
                return "IntPtr";
            }
            else
            {
                return name;
            }
        }
    }
}
