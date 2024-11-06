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
		private DataItem dataItem;

		public DataColors(SmartDatabase smartDatabase, DataItem dataItem)
		{
			this.smartDatabase = smartDatabase;
			this.dataItem = dataItem;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataColors(XmlElement tableElement, SmartDatabase smartDatabase)
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
			SqlCeDataAdapter colorAdapter = smartDatabase.dataAdapterQuery("SELECT c.* FROM itemColor i, color c WHERE i.itemNo = '"+dataItem.no+"' AND i.colorCode = c.code");
			
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
