using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataProductGroups : DataCollection
	{
		private SmartDatabase smartDatabase;

		public DataProductGroups(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataProductGroups(XmlElement tableElement, SmartDatabase smartDatabase)
		{
			fromDOM(tableElement, smartDatabase);
		}

		public void fromDOM(XmlElement tableElement, SmartDatabase smartDatabase)
		{
			XmlNodeList records = tableElement.GetElementsByTagName("RECORD");
			int i = 0;
			while (i < records.Count)
			{
				XmlElement record = (XmlElement)records.Item(i);
	
				DataItem dataItem = new DataItem(record, smartDatabase, true);

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
