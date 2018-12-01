namespace GlfwGen
{
    // Suppress warnings for "Field XXX is never used"
    #pragma warning disable 0169
    public struct GLFWvidmode
    {
        /*! The width, in screen coordinates, of the video mode.
        */
        int width;
        /*! The height, in screen coordinates, of the video mode.
        */
        int height;
        /*! The bit depth of the red channel of the video mode.
        */
        int redBits;
        /*! The bit depth of the green channel of the video mode.
        */
        int greenBits;
        /*! The bit depth of the blue channel of the video mode.
        */
        int blueBits;
        /*! The refresh rate, in Hz, of the video mode.
        */
        int refreshRate;
    }

    public unsafe struct GLFWgammaramp
    {
        /*! An array of value describing the response of the red channel.
        */
        ushort* red;
        /*! An array of value describing the response of the green channel.
        */
        ushort* green;
        /*! An array of value describing the response of the blue channel.
        */
        ushort* blue;
        /*! The number of elements in each array.
        */
        uint size;
    }

    public unsafe struct GLFWimage
    {
        /*! The width, in pixels, of this image.
        */
        int width;
        /*! The height, in pixels, of this image.
        */
        int height;
        /*! The pixel data of this image, arranged left-to-right, top-to-bottom.
        */
        byte* pixels;
    }
    #pragma warning restore 0169
}
