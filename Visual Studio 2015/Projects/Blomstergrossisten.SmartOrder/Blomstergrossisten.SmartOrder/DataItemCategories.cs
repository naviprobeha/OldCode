using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// BEHA 2006-01-12 Går mot samma tabell som ProductGroup.
    /// </summary>
    public class DataItemCategories : DataCollection
    {
        private SmartDatabase smartDatabase;

        public DataItemCategories(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            //
            // TODO: Add constructor logic here
            //
        }

        public DataItemCategories(XmlElement tableElement, SmartDatabase smartDatabase)
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

                DataItemCategory dataItem = new DataItemCategory(record, smartDatabase, true);

                i++;
            }
        }

        public DataSet getDataSet()
        {
            SqlCeDataAdapter prodGroupAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM productGroup");

            DataSet prodGroupDataSet = new DataSet();
            prodGroupAdapter.Fill(prodGroupDataSet, "productGroup");
            prodGroupAdapter.Dispose();

            return prodGroupDataSet;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
