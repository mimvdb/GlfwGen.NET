using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NativeLibraryLoader;

namespace GlfwGen
{
    internal static unsafe class GlfwLoader
    {
        public static readonly NativeLibrary s_glfwLib = LoadGlfw();

		private static NativeLibrary LoadGlfw()
		{
			string[] names;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				names = new[]
				{
					"glfw3.dll"
				};
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				names = new[]
				{
					"libglfw.so.3",
					"libglfw3.so"
				};
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				names = new[]
				{
					"libglfw3.dylib"
				};
			}
			else
			{
				Debug.WriteLine("Unknown GLFW platform. Attempting to load \"GLFW\"");
				names = new[] { "glfw3.dll" };
			}

			NativeLibrary lib = new NativeLibrary(names);
			return lib;
		}
    }
	public static unsafe partial class GlfwNative
	{
		/// <summary>
		/// Loads a function from the glfw library with the given name
		/// </summary>
		public static T LoadFunction<T>(string name)
		{
			return GlfwLoader.s_glfwLib.LoadFunction<T>(name);
		}
	}
}
