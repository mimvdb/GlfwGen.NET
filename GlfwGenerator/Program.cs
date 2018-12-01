using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace GlfwGenerator
{
    class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.HelpOption();

            var outputOption = app.Option("-o|--out <OUTPUT_DIR>", "The directory to put the generated cs files", CommandOptionType.SingleValue);

            app.OnExecute(() => {
                string outputPath = outputOption.Value();

                if (File.Exists(outputPath))
                {
                    Console.Error.WriteLine("The given path is a file, not a folder.");
                    return 1;
                }
                else if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                using (var reader = File.OpenText(Path.Combine(AppContext.BaseDirectory, "glfw3.h")))
                {
                    Console.WriteLine("Generating glfw bindings");
                    var model = GlfwModel.FromHeaderStream(reader);
                    CSSourceGenerator.Generate(outputPath, model);
                }

                return 0;
            });

            return app.Execute(args);
        }
    }
}
