using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for SalesHeaders.
	/// </summary>
	public class DataSalesHeaders : DataCollection
	{
		private SmartDatabase smartDatabase;

		public DataSalesHeaders(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public void fromDOM(XmlElement tableElement, SmartDatabase smartDatabase)
		{
		}

		public DataSet getDataSet(bool readyFlag)
		{
			SqlCeDataAdapter salesHeaderAdapter;

			if (readyFlag)
			{
				salesHeaderAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM salesHeader WHERE ready = 1");
			}
			else
			{
				salesHeaderAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM salesHeader WHERE ready = 0");
			}

			
			DataSet salesHeaderDataSet = new DataSet();
			salesHeaderAdapter.Fill(salesHeaderDataSet, "salesHeader");
			salesHeaderAdapter.Dispose();

			return salesHeaderDataSet;
		}

		public void deleteReadySalesHeaders()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM salesHeader WHERE ready = 1");

			while(dataReader.Read())
			{
				try
				{
					smartDatabase.nonQuery("DELETE FROM salesLine WHERE orderNo = "+(int)dataReader.GetValue(0));
				}
				catch (SqlCeException e) 
				{
					smartDatabase.ShowErrors(e);
				}

			}
			dataReader.Close();
			dataReader.Dispose();
			smartDatabase.nonQuery("DELETE FROM salesHeader WHERE ready = 1");

		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			Agent agent = new Agent(smartDatabase);
			

			XmlElement tableElement = xmlDocumentContext.CreateElement("TABLE");
			tableElement.SetAttribute("NO", "36");

			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM salesHeader WHERE ready = 1");

			while(dataReader.Read())
			{
				try
				{
					XmlElement recordElement = xmlDocumentContext.CreateElement("RECORD");
					recordElement.SetAttribute("METHOD", "INSERT");
					
					XmlElement fieldElement;
					
					
					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "1");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("1"));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "2");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(1)));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "3");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(agent.agentId+""+(int)dataReader.GetValue(0)));
					recordElement.AppendChild(fieldElement);
			
					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "5");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(2)));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "7");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(3)));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "8");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(4)));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "85");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(5)));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "9");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(6)));
					recordElement.AppendChild(fieldElement);

					string deliveryCode = (string)dataReader.GetValue(7);
					if (!deliveryCode.Equals("Standard"))
					{
						fieldElement = xmlDocumentContext.CreateElement("FIELD");
						fieldElement.SetAttribute("NO", "12");
						fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(7)));
						recordElement.AppendChild(fieldElement);
					}

					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "13");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(8)));
					recordElement.AppendChild(fieldElement);
			
					
					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "15");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(9)));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "16");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(10)));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "91");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(11)));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "17");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(12)));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "18");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(13)));
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
	}
}
