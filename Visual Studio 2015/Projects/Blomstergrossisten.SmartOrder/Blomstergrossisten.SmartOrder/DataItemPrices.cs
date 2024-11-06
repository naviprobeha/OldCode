using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemPrices.
    /// </summary>
    public class DataItemPrices : DataCollection
    {
        private SmartDatabase smartDatabase;
        private DataItem dataItem;

        public DataItemPrices(SmartDatabase smartDatabase, DataItem dataItem)
        {
            this.smartDatabase = smartDatabase;
            this.dataItem = dataItem;
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemPrices(XmlElement tableElement, SmartDatabase smartDatabase)
        {
            fromDOM(tableElement, smartDatabase);
        }

        public void fromDOM(XmlElement tableElement, SmartDatabase smartDatabase)
        {
            XmlNodeList records = tableElement.GetElementsByTagName("R");
            int i = 0;
            while (i < records.Count)
            {
                XmlElement record = (XmlElement)records.Item(i);

                DataItemPrice dataItemPrice = new DataItemPrice(record, smartDatabase, true);

                i++;
            }
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
