using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataWhseActivityHeaders : DataCollection
	{
		public static int WHSE_TYPE_ARRIVAL = 1; 
		public static int WHSE_TYPE_MOVEMENT = 6; 


		private SmartDatabase smartDatabase;

		public DataWhseActivityHeaders(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataWhseActivityHeaders(XmlElement tableElement, SmartDatabase smartDatabase)
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
	
				DataWhseActivityHeader dataWhseActivityHeader = new DataWhseActivityHeader(record, smartDatabase, true);

				i++;
			}
		}

		public DataSet getDataSet(int type)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT * FROM whseActivityHeader WHERE type = '"+type+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "whseActivity");
			adapter.Dispose();

			return dataSet;
		}

		public void deleteAll(int type)
		{
			smartDatabase.nonQuery("DELETE FROM whseActivityHeader WHERE type = '"+type+"'");
			smartDatabase.nonQuery("DELETE FROM whseActivityLine WHERE whseActivityType = '"+type+"'");
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			return null;
		}
	}
}
