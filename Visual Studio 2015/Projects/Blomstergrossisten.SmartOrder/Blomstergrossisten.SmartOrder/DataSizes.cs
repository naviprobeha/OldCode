using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItems.
    /// </summary>
    public class DataSizes : DataCollection
    {
        private SmartDatabase smartDatabase;

        public DataSizes(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
        }

        public DataSizes(XmlElement tableElement, SmartDatabase smartDatabase)
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

                DataSize dataSize = new DataSize(record, smartDatabase, true);

                i++;
            }
        }

        public DataSet getDataSet()
        {
            SqlCeDataAdapter sizeAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM size");

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
