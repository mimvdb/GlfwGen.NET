namespace GlfwGenerator
{
    public interface ICsWritable
    {
        void WriteCS(CsCodeWriter cw, TypeNameMappings tnm);
    }
}
