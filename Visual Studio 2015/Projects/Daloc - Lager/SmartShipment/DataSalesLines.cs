using System;
using System.Xml;
using System.Data.SqlServerCe;
using System.Data;
using System.Collections.Specialized;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for DataSalesLines.
	/// </summary>
	public class DataSalesLines : DataCollection
	{
		private SmartDatabase smartDatabase;
		private NameValueCollection quantityVector;

		public DataSalesLines(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public void fromDOM(XmlElement tableElement, SmartDatabase smartDatabase)
		{
		}

		public DataSet getDataSet(DataSalesHeader dataSalesHeader)
		{
			SqlCeDataAdapter salesLineAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM salesLine WHERE orderNo = '"+dataSalesHeader.no+"' ORDER BY no DESC");
			
			DataSet salesLineDataSet = new DataSet();
			salesLineAdapter.Fill(salesLineDataSet, "salesLine");
			salesLineAdapter.Dispose();

			return salesLineDataSet;
		}

		public int countSalesLines(DataSalesHeader dataSalesHeader)
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM salesLine WHERE orderNo = '"+dataSalesHeader.no+"'");

			if (dataReader.Read())
			{
				return dataReader.GetInt32(0);
			}

			return 0;
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			DataSetup dataSetup = new DataSetup(smartDatabase);
			Agent agent = new Agent(smartDatabase);

			XmlElement tableElement = xmlDocumentContext.CreateElement("T");
			tableElement.SetAttribute("NO", "37");

			SqlCeDataReader dataReader = smartDatabase.query("SELECT l.no, orderNo, customerNo, itemNo, quantity, l.discount, deliveryDate, unitPrice, amount, hanging, referenceNo FROM salesLine l, salesHeader h WHERE h.ready = 1 AND h.no = l.orderNo");

			while(dataReader.Read())
			{
				try
				{
					XmlElement recordElement = xmlDocumentContext.CreateElement("R");
					recordElement.SetAttribute("M", "I");
					
					XmlElement fieldElement;
					
					string itemNo = "";
					
					itemNo = (string)dataReader.GetValue(3);


					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "1");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("1"));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "3");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(agent.agentId+""+(int)dataReader.GetValue(1)));
					recordElement.AppendChild(fieldElement);
			
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "4");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+((int)dataReader.GetValue(0) * 10000)));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "5");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("2"));
					recordElement.AppendChild(fieldElement);
					
								
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "6");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(itemNo.Replace("½", "_-HALF-_")));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "15");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+dataReader.GetValue(4).ToString()));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "22");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((float.Parse(dataReader.GetValue(7).ToString()) / (1-(float.Parse(dataReader.GetValue(5).ToString())/100))).ToString().Replace(".", ","))); // A-pris
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "27");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetValue(5).ToString().Replace(".", ","))); // Rabatt
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "29");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetValue(8).ToString()));
					recordElement.AppendChild(fieldElement);

					/*
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "27");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+dataReader.GetValue(5)));
					recordElement.AppendChild(fieldElement);
					*/
					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50017");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(6)));
					recordElement.AppendChild(fieldElement);
					
					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "50060");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(dataReader.GetValue(7).ToString().Replace(".", ","))); // Apris-netto
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("F");
					fieldElement.SetAttribute("NO", "80900");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(9)));
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

		public void setAdditionalInfo(DataSalesHeader dataSalesHeader, DataItem dataItem, string additionalInfo)
		{
			try
			{
				smartDatabase.nonQuery("UPDATE salesLine SET "+additionalInfo+" WHERE orderNo = '"+dataSalesHeader.no+"' AND itemNo = '"+dataItem.no+"'");
			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}

		}

	}
}
