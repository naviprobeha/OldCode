using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataWhseItemStores : DataCollection
	{
		private SmartDatabase smartDatabase;

		public DataWhseItemStores(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataWhseItemStores(XmlElement tableElement, SmartDatabase smartDatabase)
		{
			fromDOM(tableElement, smartDatabase);
		}


		public void fromDOM(XmlElement tableElement, SmartDatabase smartDatabase)
		{
		}

		public DataSet getDataSet()
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT * FROM whseItemStore WHERE toBinCode = ''");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "whseItemStore");
			adapter.Dispose();

			return dataSet;
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			DataSetup dataSetup = new DataSetup(smartDatabase);

			XmlElement tableElement = xmlDocumentContext.CreateElement("T");
			tableElement.SetAttribute("NO", "58014");


			SqlCeDataReader dataReader = smartDatabase.query("SELECT seqNo, locationCode, binCode, handleUnitId, itemNo, quantity, toBinCode FROM WhseItemStore");

			while(dataReader.Read())
			{
				try
				{
					XmlElement recordElement = xmlDocumentContext.CreateElement("R");
					recordElement.SetAttribute("M", "I");
					
					XmlElement fieldElement;
					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "1");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("7"));
					recordElement.AppendChild(fieldElement);

		
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "3");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+dataReader.GetInt32(0)));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "14");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(4)));
					recordElement.AppendChild(fieldElement);
					
								
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "26");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetFloat(5).ToString()));
					recordElement.AppendChild(fieldElement);


					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50007");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(3)));
					recordElement.AppendChild(fieldElement);


					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50010");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(1)));
					recordElement.AppendChild(fieldElement);
		
	
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50013");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(2)));
					recordElement.AppendChild(fieldElement);
		
			
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50050");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(6)));
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
			smartDatabase.nonQuery("DELETE FROM whseItemStore");
		}

	}
}
