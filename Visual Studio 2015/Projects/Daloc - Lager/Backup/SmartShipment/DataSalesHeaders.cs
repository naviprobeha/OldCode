using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace SmartShipment
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

			Agent agent = new Agent(smartDatabase);

			if (readyFlag)
			{
				salesHeaderAdapter = smartDatabase.dataAdapterQuery("SELECT *, '"+agent.agentId+"'+LTRIM(STR(no)) as orderNo FROM salesHeader WHERE ready = 1");
			}
			else
			{
				salesHeaderAdapter = smartDatabase.dataAdapterQuery("SELECT *, '"+agent.agentId+"'+LTRIM(STR(no)) as orderNo FROM salesHeader WHERE ready = 0");
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
		
			
			
			XmlElement tableElement = xmlDocumentContext.CreateElement("T");
			tableElement.SetAttribute("NO", "36");

			SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM salesHeader WHERE ready = 1");

			while(dataReader.Read())
			{
				try
				{
					XmlElement recordElement = xmlDocumentContext.CreateElement("R");
					recordElement.SetAttribute("M", "I");
					
					XmlElement fieldElement;
					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "1");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("1"));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "2");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(1)));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "3");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(agent.agentId+""+(int)dataReader.GetValue(0)));
					recordElement.AppendChild(fieldElement);
			
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "5");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(2))));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "7");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(3))));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "8");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(4))));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "85");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(5))));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "9");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(6))));
					recordElement.AppendChild(fieldElement);

					string deliveryCode = (string)dataReader.GetValue(7);
					if (!deliveryCode.Equals("Standard"))
					{
						fieldElement = xmlDocumentContext.CreateElement("F");
						fieldElement.SetAttribute("NO", "12");
						fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(7))));
						recordElement.AppendChild(fieldElement);
					}

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "13");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(8))));
					recordElement.AppendChild(fieldElement);
			
					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "15");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(9))));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "16");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(10))));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "91");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(11))));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "17");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(12))));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "18");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(13))));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "19");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii(System.DateTime.Today.ToString("yyyy-MM-dd"))));
					recordElement.AppendChild(fieldElement);


					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50035");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(16))));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "104");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(StringConverter.convertToAscii((string)dataReader.GetValue(17))));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "80901");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetInt32(18).ToString()));
					recordElement.AppendChild(fieldElement);


					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "37");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetFloat(19).ToString()));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "35");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("TRUE"));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "60002");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(20)));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50041");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(21)));
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
