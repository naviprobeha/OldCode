using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataWhseActivityLines : DataCollection
	{
		public static int WHSE_ACTION_TAKE = 0; 
		public static int WHSE_ACTION_PLACE = 1; 

		public static int WHSE_STATUS_NONE = 0; 
		public static int WHSE_STATUS_HANDLED = 1; 
		
		private SmartDatabase smartDatabase;

		public DataWhseActivityLines(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataWhseActivityLines(XmlElement tableElement, SmartDatabase smartDatabase)
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
	
				DataWhseActivityLine dataWhseActivityLine = new DataWhseActivityLine(record, smartDatabase, true);

				i++;
			}
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			DataSetup dataSetup = new DataSetup(smartDatabase);

			XmlElement tableElement = xmlDocumentContext.CreateElement("T");
			tableElement.SetAttribute("NO", "58014");

			SqlCeDataReader dataReader = smartDatabase.query("SELECT seqNo, whseActivityNo, whseActivityType, lineEntryNo, zone, locationCode, binCode, itemNo, quantity, freq, action, handleUnitId, status, linkedToLineNo FROM WhseActivityLine");

			while(dataReader.Read())
			{
				try
				{
					XmlElement recordElement = xmlDocumentContext.CreateElement("R");
					recordElement.SetAttribute("M", "I");
					
					XmlElement fieldElement;
					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "1");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+dataReader.GetInt32(2)));
					recordElement.AppendChild(fieldElement);

				
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "2");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(1)));
					recordElement.AppendChild(fieldElement);
			
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "3");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+dataReader.GetInt32(3)));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "14");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(7)));
					recordElement.AppendChild(fieldElement);
					
								
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "26");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetFloat(8).ToString()));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50007");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(11)));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50008");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetInt32(10).ToString()));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50010");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(5)));
					recordElement.AppendChild(fieldElement);

				
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50011");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(5)));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50012");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetInt32(10).ToString()));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50013");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(6)));
					recordElement.AppendChild(fieldElement);
					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50020");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetInt32(13).ToString()));
					recordElement.AppendChild(fieldElement);
					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50021");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetInt32(12).ToString()));
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

		public DataSet getDataSet(DataWhseActivityHeader dataWhseActivityHeader)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT * FROM whseActivityLine WHERE whseActivityNo = '"+dataWhseActivityHeader.no+"' AND whseActivityType = '"+dataWhseActivityHeader.type+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "whseActivityLine");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getJobDataSet(DataWhseActivityHeader dataWhseActivityHeader, int freq, int action, int status, bool checkBin)
		{
			string binQuery = "";
			if (checkBin)
			{
				binQuery = " AND binCode = ''";
			}
			
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT * FROM whseActivityLine WHERE whseActivityNo = '"+dataWhseActivityHeader.no+"' AND whseActivityType = '"+dataWhseActivityHeader.type+"' AND freq = '"+freq+"' AND action = '"+action+"' AND status = '"+status+"'"+binQuery);
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "whseActivityLine");
			adapter.Dispose();

			return dataSet;
		}

		public int countLines(int type, int freq, int action, int status, bool checkBin)
		{
			string binQuery = "";
			if (checkBin) binQuery = " AND binCode = ''";

			SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM whseActivityLine WHERE whseActivityType = '"+type+"' AND freq = '"+freq+"' AND action = '"+action+"' AND status = '"+status+"'"+binQuery);

			if (dataReader.Read())
			{
				try
				{
					return dataReader.GetInt32(0);
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();
			return 0;
		}

		public int countLines(DataWhseActivityHeader dataWhseActivityHeader, int freq, int action, int status, bool checkBin)
		{
			string binQuery = "";
			if (checkBin) binQuery = " AND binCode = ''";

			SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM whseActivityLine WHERE whseActivityNo = '"+dataWhseActivityHeader.no+"' AND whseActivityType = '"+dataWhseActivityHeader.type+"' AND freq = '"+freq+"' AND action = '"+action+"' AND status = '"+status+"'"+binQuery);

			if (dataReader.Read())
			{
				try
				{
					return dataReader.GetInt32(0);
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}
			}
			dataReader.Dispose();
			return 0;
		}
		
	}
}
