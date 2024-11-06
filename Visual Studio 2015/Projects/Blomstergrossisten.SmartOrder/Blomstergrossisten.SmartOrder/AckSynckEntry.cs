using System;
using System.Xml;


namespace SmartOrder
{
    /// <summary>
    /// Summary description for Publication.
    /// </summary>
    public class AckSynckEntry : ServiceArgument
    {
        private string entryNo;

        public AckSynckEntry(string entryNo)
        {
            this.entryNo = entryNo;
            //
            // TODO: Add constructor logic here
            //
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement entryNoElement = xmlDocumentContext.CreateElement("ENTRY_NO");
            XmlText xmlText = xmlDocumentContext.CreateTextNode(entryNo);
            entryNoElement.AppendChild(xmlText);



            return entryNoElement;
        }

    }
}
