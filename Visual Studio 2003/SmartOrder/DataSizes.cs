using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataSizes : DataCollection
	{
		private SmartDatabase smartDatabase;
		private DataItem dataItem;

		public DataSizes(SmartDatabase smartDatabase, DataItem dataItem)
		{
			this.smartDatabase = smartDatabase;
			this.dataItem = dataItem;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSizes(XmlElement tableElement, SmartDatabase smartDatabase)
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
			SqlCeDataAdapter sizeAdapter = smartDatabase.dataAdapterQuery("SELECT c.* FROM itemSize i, size c WHERE i.itemNo = '"+dataItem.no+"' AND i.sizeCode = c.code");
			
			DataSet sizeDataSet = new DataSet();
			sizeAdapter.Fill(sizeDataSet, "size");
			sizeAdapter.Dispose();

			return sizeDataSet;
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			return null;
		}
	}
}
