using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemPrices.
    /// </summary>
    public class DataLineDiscounts : DataCollection
    {
        private SmartDatabase smartDatabase;

        public DataLineDiscounts(XmlElement tableElement, SmartDatabase smartDatabase)
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

                DataLineDiscount dataLineDiscount = new DataLineDiscount(record, smartDatabase, true);

                i++;
            }
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
