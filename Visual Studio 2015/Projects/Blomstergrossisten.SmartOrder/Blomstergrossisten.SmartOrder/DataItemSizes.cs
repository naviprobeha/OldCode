using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItemSizes.
    /// </summary>
    public class DataItemSizes : DataCollection
    {
        private SmartDatabase smartDatabase;
        private DataItem dataItem;

        public DataItemSizes(SmartDatabase smartDatabase, DataItem dataItem)
        {
            this.smartDatabase = smartDatabase;
            this.dataItem = dataItem;
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemSizes(XmlElement tableElement, SmartDatabase smartDatabase)
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

                DataItemSize dataItemSize = new DataItemSize(record, smartDatabase, true);

                i++;
            }
        }

        public DataSet getDataSet()
        {
            SqlCeDataAdapter sizeAdapter = smartDatabase.dataAdapterQuery("SELECT c.* FROM itemSize i, size c WHERE i.itemNo = '" + dataItem.no + "' AND i.sizeCode = c.code ORDER BY i.sortOrder, c.code");

            DataSet sizeDataSet = new DataSet();
            sizeAdapter.Fill(sizeDataSet, "size");
            sizeAdapter.Dispose();

            return sizeDataSet;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
