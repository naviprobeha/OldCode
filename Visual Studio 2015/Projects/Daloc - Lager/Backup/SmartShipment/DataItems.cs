using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataItems : DataCollection
	{
		private SmartDatabase smartDatabase;

		public DataItems(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataItems(XmlElement tableElement, SmartDatabase smartDatabase)
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
	
				DataItem dataItem = new DataItem(record, smartDatabase, true);

				i++;
			}
		}

		public DataSet getDataSet()
		{

			SqlCeDataAdapter itemAdapter = smartDatabase.dataAdapterQuery("SELECT no, description, baseUnit FROM item");
			
			DataSet itemDataSet = new DataSet();
			itemAdapter.Fill(itemDataSet, "item");
			itemAdapter.Dispose();

			return itemDataSet;
		}

		public DataSet getDataSet(string seasonFilter, string prodGroupFilter, int searchMethod, string searchString)
		{
			string filterString = "";

			if (!seasonFilter.Equals("Alla"))
			{
				if (filterString.Equals(""))
					filterString = "WHERE seasonCode = '"+seasonFilter+"'";
				else
					filterString = filterString + " AND seasonCode = '"+seasonFilter+"'";				
			}
			
			if (!prodGroupFilter.Equals("Alla"))
			{
				if (filterString.Equals(""))
					filterString = "WHERE productGroupCode = '"+prodGroupFilter+"'";
				else
					filterString = filterString + " AND productGroupCode = '"+prodGroupFilter+"'";				
			}

			string orderBy = "no";
			if (searchMethod == 0) orderBy = "no";
			if (searchMethod == 1) orderBy = "description";

			if (!searchString.Equals(""))
			{
				if (!filterString.Equals("")) 
				{
					searchString = " AND "+searchString;
				}
				else
				{
					searchString = " WHERE "+searchString;
				}
			}

			SqlCeDataAdapter itemAdapter = smartDatabase.dataAdapterQuery("SELECT no, description, baseUnit FROM item "+filterString+searchString+" ORDER BY "+orderBy);
			
			DataSet itemDataSet = new DataSet();
			itemAdapter.Fill(itemDataSet, "item");
			itemAdapter.Dispose();

			return itemDataSet;
		}
		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			return null;
		}
	}
}
