using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataItems.
    /// </summary>
    public class DataPaymentMethods : DataCollection
    {
        private SmartDatabase smartDatabase;

        public DataPaymentMethods(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            //
            // TODO: Add constructor logic here
            //
        }

        public DataPaymentMethods(XmlElement tableElement, SmartDatabase smartDatabase)
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

                DataPaymentMethod dataItem = new DataPaymentMethod(record, smartDatabase, true);

                i++;
            }
        }

        public DataSet getDataSet()
        {
            SqlCeDataAdapter paymentMethodAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM paymentMethod ORDER BY code");

            DataSet paymentMethodDataSet = new DataSet();
            paymentMethodAdapter.Fill(paymentMethodDataSet, "paymentMethod");
            paymentMethodAdapter.Dispose();

            return paymentMethodDataSet;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
