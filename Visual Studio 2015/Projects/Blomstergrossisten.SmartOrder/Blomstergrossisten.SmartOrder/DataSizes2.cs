using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItems.
    /// </summary>
    public class DataSizes2 : DataCollection
    {
        private SmartDatabase smartDatabase;

        public DataSizes2(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
        }

        public DataSizes2(XmlElement tableElement, SmartDatabase smartDatabase)
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

                DataSize2 dataSize2 = new DataSize2(record, smartDatabase, true);

                i++;
            }
        }

        public DataSet getDataSet()
        {
            SqlCeDataAdapter sizeAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM size2");

            DataSet sizeDataSet = new DataSet();
            sizeAdapter.Fill(sizeDataSet, "size2");
            sizeAdapter.Dispose();

            return sizeDataSet;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
