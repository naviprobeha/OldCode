using System;
using System.Xml;

namespace SmyckaReceiptService.Handlers
{
    public class XMLHelper
    {
        public XMLHelper()
        {
        }

        public string GetNodeValue(XmlElement xmlElement, string nodeName)
        {
            XmlElement element = (XmlElement)xmlElement.SelectSingleNode(nodeName);
            if (element != null)
            {
                if (element.FirstChild != null)
                {
                    return element.FirstChild.Value;
                }
            }
            return "";
        }

        public static string GetFieldText(XmlElement documentElement, string name)
        {
            XmlElement element = (XmlElement)documentElement.SelectSingleNode(name);
            if (element != null)
            {
                XmlText textNode = (XmlText)element.FirstChild;
                if (textNode != null)
                {
                    return textNode.Value;
                }
            }
            return "";
        }

        public static int GetFieldInt(XmlElement documentElement, string name)
        {
            string text = GetFieldText(documentElement, name);
            int intValue = 0;
            if (int.TryParse(text, out intValue)) return intValue;
            return 0;


        }

        public static XmlElement AddElement(XmlElement parentElement, string elementName, string elementValue)
        {
            XmlElement xmlElement = null;
            AddElement(ref parentElement, elementName, elementValue, "", ref xmlElement);
            return xmlElement;
        }

        public static XmlElement AddElement(ref XmlElement parentElement, string elementName, string elementValue, string nameSpace, ref XmlElement xmlElement)
        {
            xmlElement = parentElement.OwnerDocument.CreateElement(elementName, nameSpace);
            if (elementValue != "")
            {
                XmlText xmlText = parentElement.OwnerDocument.CreateTextNode(elementValue);
                xmlElement.AppendChild(xmlText);
            }
            parentElement.AppendChild(xmlElement);
            return xmlElement;
        }

        public static XmlElement AddFieldElement(XmlElement parentElement, string fieldNo, string fieldValue)
        {
            XmlElement fieldElement = AddElement(parentElement, "field", fieldValue);
            fieldElement.SetAttribute("no", fieldNo);

            return fieldElement;

        }

        public static string convertBool(bool boolValue)
        {
            if (boolValue) return "TRUE";
            return "FALSE";
        }
    }
}
