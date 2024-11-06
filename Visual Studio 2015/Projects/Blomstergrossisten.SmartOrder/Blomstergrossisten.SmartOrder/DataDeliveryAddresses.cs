using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
    /// <summary>
    /// Summary description for DataDeliveryAddresses.
    /// </summary>
    public class DataDeliveryAddresses : DataCollection
    {
        private SmartDatabase smartDatabase;

        public DataDeliveryAddresses(SmartDatabase smartDatabase)
        {
            this.smartDatabase = smartDatabase;
            //
            // TODO: Add constructor logic here
            //
        }

        public DataDeliveryAddresses(XmlElement tableElement, SmartDatabase smartDatabase)
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

                DataDeliveryAddress deliveryAddress = new DataDeliveryAddress(record, smartDatabase, true);

                i++;
            }
        }

        public DataSet getDataSet(DataCustomer dataCustomer)
        {
            SqlCeDataAdapter deliveryAddressAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM deliveryAddress WHERE customerNo = '" + dataCustomer.no + "'");

            DataSet deliveryAddressDataSet = new DataSet();
            deliveryAddressAdapter.Fill(deliveryAddressDataSet, "deliveryAddress");
            deliveryAddressAdapter.Dispose();

            return deliveryAddressDataSet;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            return null;
        }
    }
}
