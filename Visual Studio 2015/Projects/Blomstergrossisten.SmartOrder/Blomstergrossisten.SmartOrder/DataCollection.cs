using System;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItem.
    /// </summary>
    public interface DataCollection
    {

        void fromDOM(XmlElement dataItemElement, SmartDatabase smartDatabase);
        XmlElement toDOM(XmlDocument xmlDocumentContext);

    }
}
