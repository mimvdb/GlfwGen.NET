using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GlfwGenerator
{
    public class ParameterList
    {
        private readonly List<Parameter> parameters;

        public ParameterList(string parameterString, bool withNames = false)
        {
            string[] parts = parameterString.Trim().Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
            parameters = new List<Parameter>();

            foreach (var param in parts)
            {
                if (param == "void")
                {
                    continue;
                }
                string type;
                if (param.Trim().StartsWith("const "))
                {
                    type = param.Trim().Substring(6);
                }
                else
                {
                    type = param.Trim();
                }

                if (withNames)
                {
                    int lastSpace = type.LastIndexOf(' ');
                    parameters.Add(new Parameter(type.Substring(0, lastSpace), type.Substring(lastSpace + 1)));
                }
                else
                {
                    parameters.Add(new Parameter(type));
                }
            }

            foreach (var group in parameters.GroupBy(i => i.type))
            {
                string normName = NormalizeTypeToName(group.Key);
                int counter = 0;
                foreach (var param in group)
                {
                    if (!string.IsNullOrEmpty(param.name))
                    {
                        continue;
                    }
                    param.name = normName + ++counter;
                }
            }
        }

        private string NormalizeTypeToName(string type)
        {
            CultureInfo.InvariantCulture.TextInfo.ToTitleCase(type);
            string name = type.Replace(" ", "").Replace("*", "Ptr");

            return name.Substring(0, 1).ToLower() + name.Substring(1);
        }

        public string ToString(TypeNameMappings tnm)
        {
            var paramStrings = parameters.Select(i =>
            {
                string type = tnm.GetMappedName(i.type);
                return $"{type} {i.name}";
            });
            return string.Join(", ", paramStrings);
        }

        public string ToNames()
        {
            return string.Join(", ", parameters.Select(i => i.name));
        }
    }

    public class Parameter
    {
        private readonly static Dictionary<string, string> _nameTransforms = new Dictionary<string, string>
        {
            ["string"] = "str"
        };

        public readonly string type;
        public string name;

        public Parameter(string type)
        {
            this.type = type.Trim();
        }

        public Parameter(string type, string name) : this(type)
        {
            while (_nameTransforms.ContainsKey(name))
            {
                name = _nameTransforms[name];
            }
            
            this.name = name;
        }
    }
}
