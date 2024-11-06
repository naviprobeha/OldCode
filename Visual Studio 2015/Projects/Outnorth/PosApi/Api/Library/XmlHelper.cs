using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Api.Library
{
    public class XmlHelper
    {
        public static string GetNodeValue(XmlElement xmlElement, string nodeName, XmlNamespaceManager nsMgr)
        {
            XmlElement childElement = (XmlElement)xmlElement.SelectSingleNode(nodeName, nsMgr);
            if (childElement != null)
            {
                XmlText text = (XmlText)childElement.FirstChild;
                if (text != null)
                {
                    return text.Value;
                }
            }

            return "";
        }

        public static string GetNodeValue(XmlElement xmlElement, string nodeName)
        {
            XmlElement childElement = (XmlElement)xmlElement.SelectSingleNode(nodeName);
            if (childElement != null)
            {
                XmlText text = (XmlText)childElement.FirstChild;
                if (text != null)
                {
                    return text.Value;
                }
            }

            return "";
        }

    }
}