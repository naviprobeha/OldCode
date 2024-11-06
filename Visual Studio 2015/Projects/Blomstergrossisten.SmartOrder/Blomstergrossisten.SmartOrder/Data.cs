using System;
using System.Collections;
using System.Xml;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for Data.
    /// </summary>
    public class Data
    {
        private Logger logger;
        private SmartDatabase smartDatabase;

        public Data(SmartDatabase smartDatabase, Logger logger)
        {
            this.smartDatabase = smartDatabase;
            this.logger = logger;

            //
            // TODO: Add constructor logic here
            //
        }

        public Data(XmlElement dataElement, SmartDatabase smartDatabase, Logger logger)
        {
            this.logger = logger;
            fromDOM(dataElement, smartDatabase);
        }

        public void fromDOM(XmlElement dataElement, SmartDatabase smartDatabase)
        {
            XmlNodeList tables = dataElement.GetElementsByTagName("T");
            int i = 0;
            while (i < tables.Count)
            {
                XmlElement table = (XmlElement)tables.Item(i);
                XmlAttribute attribute = table.GetAttributeNode("NO");

                if (attribute.FirstChild.Value.Equals("13"))
                {
                    log("Läser in säljare.");
                    DataCollection dataCollection = new DataSalesPersons(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("18"))
                {
                    log("Läser in kunder.");
                    DataCollection dataCollection = new DataCustomers(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("27"))
                {
                    log("Läser in artiklar.");
                    DataCollection dataCollection = new DataItems(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("28"))
                {
                    log("Läser in artikelpriser.");
                    DataCollection dataCollection = new DataItemPrices(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("222"))
                {
                    log("Läser in leveransadresser.");
                    DataCollection dataCollection = new DataDeliveryAddresses(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("289"))
                {
                    log("Läser in betalningsmetoder.");
                    DataCollection dataCollection = new DataPaymentMethods(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("5401"))
                {
                    log("Läser in artikelvarianter.");
                    DataCollection dataCollection = new DataItemVariantDims(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("5717"))
                {
                    log("Läser in korsreferenser.");
                    DataCollection dataCollection = new DataItemCrossReferences(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("5722"))
                {
                    log("Läser in artikelkategorier.");
                    DataCollection dataCollection = new DataItemCategories(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("5723"))
                {
                    log("Läser in produktgrupper.");
                    DataCollection dataCollection = new DataProductGroups(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("7002"))
                {
                    log("Läser in artikelpriser.");
                    DataCollection dataCollection = new DataItemPrices(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("7004"))
                {
                    log("Läser in radrabatter.");
                    DataCollection dataCollection = new DataLineDiscounts(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("56001"))
                {
                    log("Läser in färger.");
                    DataCollection dataCollection = new DataColors(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("56004"))
                {
                    log("Läser in storlekar.");
                    DataCollection dataCollection = new DataSizes(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("56007"))
                {
                    log("Läser in kupor.");
                    DataCollection dataCollection = new DataSizes2(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("56010"))
                {
                    log("Läser in färger per basartikel.");
                    DataCollection dataCollection = new DataItemColors(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("56011"))
                {
                    log("Läser in storlekar per basartikel.");
                    DataCollection dataCollection = new DataItemSizes(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("56012"))
                {
                    log("Läser in kupor per basartikel.");
                    DataCollection dataCollection = new DataItemSizes2(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("56013"))
                {
                    log("Läser in basartiklar.");
                    DataCollection dataCollection = new DataItems(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("56014"))
                {
                    log("Läser in artikelvarianter.");
                    DataCollection dataCollection = new DataItemVariants(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("56020"))
                {
                    log("Läser in säsonger.");
                    DataCollection dataCollection = new DataSeasons(table, smartDatabase);
                }

                if (attribute.FirstChild.Value.Equals("50003"))
                {
                    log("Läser in användarreferenser.");
                    DataCollection dataCollection = new DataUserReferences(table, smartDatabase);
                }


                i++;
            }

        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement dataElement = xmlDocumentContext.CreateElement("DATA");

            DataSalesHeaders salesHeaders = new DataSalesHeaders(smartDatabase);
            dataElement.AppendChild(salesHeaders.toDOM(xmlDocumentContext));

            DataSalesLines salesLines = new DataSalesLines(smartDatabase);
            dataElement.AppendChild(salesLines.toDOM(xmlDocumentContext));

            return dataElement;
        }

        private void log(string message)
        {
            if (logger != null) logger.write(message);
        }
    }
}
