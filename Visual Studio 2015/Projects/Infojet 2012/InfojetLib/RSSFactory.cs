using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

namespace Navipro.Infojet.Lib
{
    public class RSSFactory
    {
        private Infojet infojetContext;

        public RSSFactory(Infojet infojetCotext)
        {
            this.infojetContext = infojetContext;

        }

        public XmlDocument createRSSFeed()
        {
            WebPages webPages = new WebPages(infojetContext.systemDatabase);
            DataSet webPageDataSet = webPages.getRSSPages(infojetContext.webSite.code, infojetContext.languageCode);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\"?><rss version=\"2.0\"/>");

            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement channelElement = xmlDoc.CreateElement("channel");
            docElement.AppendChild(channelElement);

            XmlElement titleElement = xmlDoc.CreateElement("title");
            XmlText titleText = xmlDoc.CreateTextNode(infojetContext.translate("TITLE"));
            titleElement.AppendChild(titleText);
            channelElement.AppendChild(titleElement);

            XmlElement linkElement = xmlDoc.CreateElement("title");
            XmlText linkText = xmlDoc.CreateTextNode(infojetContext.webSite.location);
            linkElement.AppendChild(linkText);
            channelElement.AppendChild(linkElement);

            XmlElement linkElement = xmlDoc.CreateElement("title");
            XmlText linkText = xmlDoc.CreateTextNode(infojetContext.webSite.location);
            linkElement.AppendChild(linkText);
            channelElement.AppendChild(linkElement);

        }
    }
}
