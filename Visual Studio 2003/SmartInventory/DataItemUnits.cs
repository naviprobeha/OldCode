using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataItemUnits : DataCollection
	{
		private SmartDatabase smartDatabase;

		public DataItemUnits(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataItemUnits(XmlElement tableElement, SmartDatabase smartDatabase)
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
	
				DataItemUnit dataItemUnit = new DataItemUnit(record, smartDatabase, true);

				i++;
			}
		}

		public DataSet getDataSet()
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT * FROM itemUnit");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "itemUnit");
			adapter.Dispose();

			return dataSet;
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			XmlElement tableElement = xmlDocumentContext.CreateElement("T");
			tableElement.SetAttribute("NO", "58016");

			SqlCeDataReader dataReader = smartDatabase.query("SELECT itemNo, code, quantity FROM itemUnit");

			while(dataReader.Read())
			{
				try
				{
					XmlElement recordElement = xmlDocumentContext.CreateElement("R");
					recordElement.SetAttribute("M", "I");
					
					XmlElement fieldElement;
					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "1");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(1)));
					recordElement.AppendChild(fieldElement);
			
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "10");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+dataReader.GetFloat(2)));
					recordElement.AppendChild(fieldElement);
							
					
					tableElement.AppendChild(recordElement);
					
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Close();
			dataReader.Dispose();

			return tableElement;
		}

		public void deleteAll()
		{
			smartDatabase.nonQuery("DELETE FROM itemUnit");
		}

	}
}
