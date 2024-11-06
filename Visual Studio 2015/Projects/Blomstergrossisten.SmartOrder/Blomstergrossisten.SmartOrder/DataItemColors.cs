using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemColors.
    /// </summary>
    public class DataItemColors : DataCollection
    {
        private SmartDatabase smartDatabase;
        private DataItem dataItem;

        public DataItemColors(SmartDatabase smartDatabase, DataItem dataItem)
        {
            this.smartDatabase = smartDatabase;
            this.dataItem = dataItem;
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemColors(XmlElement tableElement, SmartDatabase smartDatabase)
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

                DataItemColor dataItemColor = new DataItemColor(record, smartDatabase, true);

                i++;
            }
        }

        public DataSet getDataSet()
        {
            SqlCeDataAdapter colorAdapter = smartDatabase.dataAdapterQuery("SELECT c.* FROM itemColor i, color c WHERE i.itemNo = '" + dataItem.no + "' AND i.colorCode = c.code");

            DataSet colorDataSet = new DataSet();
            colorAdapter.Fill(colorDataSet, "color");
            colorAdapter.Dispose();

            return colorDataSet;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
