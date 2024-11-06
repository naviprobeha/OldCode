using System;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for ServiceArgument.
    /// </summary>
    public interface ServiceArgument
    {
        XmlElement toDOM(XmlDocument xmlDocumentContext);
    }
}
