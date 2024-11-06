using System;
using System.Xml;

namespace Navipro.Sandberg.Common
{
    /// <summary>
    /// Summary description for ServiceArgument.
    /// </summary>
    public interface ServiceArgument
    {
        XmlElement toDOM(XmlDocument xmlDoc);
    }
}
