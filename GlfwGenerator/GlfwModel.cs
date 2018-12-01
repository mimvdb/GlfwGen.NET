using System;
using System.Collections.Generic;
using System.IO;

namespace GlfwGenerator
{
    public class GlfwModel
    {
        public List<GlfwConstant> Constants { get; set; }
        public List<GlfwOpaquePointer> Handles { get; set; }
        public List<GlfwDelegate> Delegates { get; set; }
        public List<GlfwFunction> Functions { get; set; }

        private GlfwModel() {
            Constants = new List<GlfwConstant>();
            Handles = new List<GlfwOpaquePointer>();
            Delegates = new List<GlfwDelegate>();
            Functions = new List<GlfwFunction>();
        }

        public static GlfwModel FromHeaderStream(StreamReader stream)
        {
            var model = new GlfwModel();
            Stage stage = Stage.Begin;

            string line;
            while ((line = stream.ReadLine()) != null)
            {
                switch (stage)
                {
                    case Stage.Begin:
                    {
                        if (line.Contains("GLFW API tokens"))
                        {
                            // Skip comment line
                            stream.ReadLine();
                            stage = Stage.Constants;
                        }
                    } break;
                    case Stage.Constants:
                    {
                        if (line.Contains("GLFW API types"))
                        {
                            // Skip comment line
                            stream.ReadLine();
                            stage = Stage.Types;
                        }

                        if (line.StartsWith("#define"))
                        {
                            model.Constants.Add(new GlfwConstant(line));
                        }
                    } break;
                    case Stage.Types:
                    {
                        if (line.Contains("GLFW API functions"))
                        {
                            // Skip comment line
                            stream.ReadLine();
                            stage = Stage.Functions;
                        }

                        // TODO multiline structs
                        if (line.StartsWith("typedef void") && line.EndsWith(";"))
                        {
                            model.Delegates.Add(new GlfwDelegate(line));
                        }
                        else if (line.StartsWith("typedef struct") && line.EndsWith(";"))
                        {
                            model.Handles.Add(new GlfwOpaquePointer(line));
                        }
                    } break;
                    case Stage.Functions:
                    {
                        if (line.StartsWith("GLFWAPI"))
                        {
                            model.Functions.Add(new GlfwFunction(line));
                        }
                    } break;
                }
            }

            return model;
        }

        private enum Stage
        {
            Begin,
            Constants,
            Types,
            Functions
        }
    }
}
