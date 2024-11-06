using System;

namespace PowerBISync
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public interface Logger
    {
        void write(string message, int type);
    }
}
