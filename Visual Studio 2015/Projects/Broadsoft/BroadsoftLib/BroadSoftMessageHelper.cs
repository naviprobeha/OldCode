using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for BroadWorksMessageHelper.
    /// </summary>
    public class BroadSoftMessageHelper
    {
        public BroadSoftMessageHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public XmlElement addElement(XmlElement parentXmlElement, string elementName, string elementValue)
        {
            XmlDocument xmlDoc = parentXmlElement.OwnerDocument;
            XmlElement xmlElement = xmlDoc.CreateElement(elementName);

            if ((elementValue != null) && (elementValue != ""))
            {
                XmlText textNode = xmlDoc.CreateTextNode(elementValue);
                xmlElement.AppendChild(textNode);
            }

            parentXmlElement.AppendChild(xmlElement);

            return xmlElement;

        }

        public XmlElement addElement(XmlElement parentXmlElement, string elementName)
        {
            return addElement(parentXmlElement, elementName, null);
        }

        public void addAttribute(XmlElement xmlElement, string attributeName, string attributeValue)
        {
            XmlAttribute attribute = xmlElement.OwnerDocument.CreateAttribute(attributeName);
            attribute.Value = attributeValue;
            xmlElement.Attributes.Append(attribute);
        }

        public XmlDocument createHeader(XmlDocument xmlDoc, string messageType, XmlElement xmlArgumentElement)
        {
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><BroadsoftDocument/>");

            XmlElement documentElement = xmlDoc.DocumentElement;

            addAttribute(documentElement, "protocol", "CAP");
            addAttribute(documentElement, "version", "14.0");

            XmlElement commandElement = addElement(documentElement, "command");
            addAttribute(commandElement, "commandType", messageType);

            XmlElement commandDataElement = addElement(commandElement, "commandData");
            commandDataElement.AppendChild(xmlArgumentElement);

            return xmlDoc;


        }
    }
}
