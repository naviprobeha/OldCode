using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NaviPro.Alufluor.Idus.Library.Helpers
{
    public class XmlHelper
    {


        public static string getNodeValue(XmlElement parentElement, string nodeName)
        {
            XmlElement element = (XmlElement)parentElement.SelectSingleNode(nodeName);
            if (element.FirstChild != null)
            {
                return element.InnerText;
            }
            return "";
        }

        public static XmlElement AddElement(ref XmlElement parentElement, string nodeName, string nodeValue, string ns)
        {
            XmlElement childElement = parentElement.OwnerDocument.CreateElement(nodeName, ns);
            if (nodeValue != "")
            {
                XmlText xmlText = parentElement.OwnerDocument.CreateTextNode(nodeValue);
                childElement.AppendChild(xmlText);
            }

            parentElement.AppendChild(childElement);

            return childElement;
        }

    }
}
