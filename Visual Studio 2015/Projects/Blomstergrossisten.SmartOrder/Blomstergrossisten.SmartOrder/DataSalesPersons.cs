using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItems.
    /// </summary>
    public class DataSalesPersons : DataCollection
    {
        private SmartDatabase smartDatabase;

        public DataSalesPersons(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSalesPersons(XmlElement tableElement, SmartDatabase smartDatabase)
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

                DataSalesPerson dataSalesPerson = new DataSalesPerson(record, smartDatabase, true);

                i++;
            }
        }

        public DataSet getDataSet()
        {
            SqlCeDataAdapter dataAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM salesPerson ORDER BY code");

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "salesPerson");
            dataAdapter.Dispose();

            return dataSet;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
