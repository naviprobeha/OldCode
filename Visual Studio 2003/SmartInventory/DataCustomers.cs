using System;
using System.Collections;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataCustomers.
	/// </summary>
	public class DataCustomers : DataCollection
	{
		private SmartDatabase smartDatabase;

		public DataCustomers(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataCustomers(XmlElement tableElement, SmartDatabase smartDatabase)
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
	
				DataCustomer dataCustomer = new DataCustomer(record, smartDatabase, true);

				i++;
			}
		}

		public DataSet getDataSet()
		{
			SqlCeDataAdapter customerAdapter = smartDatabase.dataAdapterQuery("SELECT *, '' AS checked FROM customer");
			
			DataSet customerDataSet = new DataSet();
			customerAdapter.Fill(customerDataSet, "customer");
			customerAdapter.Dispose();

			return customerDataSet;
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			return null;
		}
	}
}
