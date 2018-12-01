// <auto-generated>
// This file is generated by GlfwGenerator
// </auto-generated>
using System;

namespace GlfwGen
{
    public struct GLFWmonitor
    {
        public readonly IntPtr IntPtr;

        public GLFWmonitor(IntPtr pointer)
        {
            IntPtr = pointer;
        }
        public static implicit operator IntPtr(GLFWmonitor monitor) => monitor.IntPtr;
        public static implicit operator GLFWmonitor(IntPtr pointer) => new GLFWmonitor(pointer);
    }
    public struct GLFWwindow
    {
        public readonly IntPtr IntPtr;

        public GLFWwindow(IntPtr pointer)
        {
            IntPtr = pointer;
        }
        public static implicit operator IntPtr(GLFWwindow window) => window.IntPtr;
        public static implicit operator GLFWwindow(IntPtr pointer) => new GLFWwindow(pointer);
    }
    public struct GLFWcursor
    {
        public readonly IntPtr IntPtr;

        public GLFWcursor(IntPtr pointer)
        {
            IntPtr = pointer;
        }
        public static implicit operator IntPtr(GLFWcursor cursor) => cursor.IntPtr;
        public static implicit operator GLFWcursor(IntPtr pointer) => new GLFWcursor(pointer);
    }
}
