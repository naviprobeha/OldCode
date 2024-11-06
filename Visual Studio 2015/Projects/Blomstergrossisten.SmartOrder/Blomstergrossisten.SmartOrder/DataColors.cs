using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItems.
    /// </summary>
    public class DataColors : DataCollection
    {
        private SmartDatabase smartDatabase;

        public DataColors(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
        }

        public DataColors(XmlElement tableElement, SmartDatabase smartDatabase)
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

                DataColor dataColor = new DataColor(record, smartDatabase, true);

                i++;
            }
        }

        public DataSet getDataSet()
        {
            SqlCeDataAdapter colorAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM color");

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
