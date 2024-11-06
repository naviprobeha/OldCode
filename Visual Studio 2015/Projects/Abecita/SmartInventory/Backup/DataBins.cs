using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataBins : DataCollection
	{
		private SmartDatabase smartDatabase;

		public DataBins(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataBins(XmlElement tableElement, SmartDatabase smartDatabase)
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
	
				DataBin dataBin = new DataBin(record, smartDatabase, true);

				i++;
			}
		}

		public DataSet getDataSet()
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT * FROM bin");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "bin");
			adapter.Dispose();

			return dataSet;
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			return null;
		}
	}
}
