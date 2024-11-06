using System;

namespace Navipro.Base.Common
{
    public interface Logger
    {
        void write(string source, int level, string message);
    }
}
