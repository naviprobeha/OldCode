using System;
using System.Xml;
using System.Data.SqlServerCe;
using System.Data;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for DataSalesLines.
	/// </summary>
	public class DataSalesLines : DataCollection
	{
		private SmartDatabase smartDatabase;

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
			SqlCeDataAdapter salesLineAdapter = smartDatabase.dataAdapterQuery("SELECT itemNo, colorCode, sum(quantity) as sumQuantity FROM salesLine WHERE orderNo = '"+dataSalesHeader.no+"' GROUP BY itemNo, colorCode");
			
			DataSet salesLineDataSet = new DataSet();
			salesLineAdapter.Fill(salesLineDataSet, "salesLine");
			salesLineAdapter.Dispose();

			return salesLineDataSet;
		}

		public DataSet getDataSet(DataSalesHeader dataSalesHeader, DataItem dataItem)
		{
			SqlCeDataAdapter salesLineAdapter = smartDatabase.dataAdapterQuery("SELECT * FROM salesLine WHERE orderNo = '"+dataSalesHeader.no+"' AND itemNo = '"+dataItem.no+"'");
			
			DataSet salesLineDataSet = new DataSet();
			salesLineAdapter.Fill(salesLineDataSet, "salesLine");
			salesLineAdapter.Dispose();

			return salesLineDataSet;
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{

			Agent agent = new Agent(smartDatabase);
			
			XmlElement tableElement = xmlDocumentContext.CreateElement("TABLE");
			tableElement.SetAttribute("NO", "37");

			SqlCeDataReader dataReader = smartDatabase.query("SELECT l.no, orderNo, customerNo, itemNo, quantity FROM salesLine l, salesHeader h WHERE h.ready = 1 AND h.no = l.orderNo");

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
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(2)));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "3");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(agent.agentId+""+(int)dataReader.GetValue(1)));
					recordElement.AppendChild(fieldElement);
			
					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "4");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+((int)dataReader.GetValue(0) * 10000)));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "5");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode("2"));
					recordElement.AppendChild(fieldElement);

					
					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "6");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode((string)dataReader.GetValue(3)));
					recordElement.AppendChild(fieldElement);

					fieldElement = xmlDocumentContext.CreateElement("FIELD");
					fieldElement.SetAttribute("NO", "15");
					fieldElement.AppendChild(xmlDocumentContext.CreateTextNode(""+dataReader.GetFloat(4)));
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

		public void setAdditionalInfo(DataSalesHeader dataSalesHeader, DataItem dataItem, DataColor dataColor, string additionalInfo)
		{
			try
			{
				smartDatabase.nonQuery("UPDATE salesLine SET "+additionalInfo+" WHERE orderNo = '"+dataSalesHeader.no+"' AND itemNo = '"+dataItem.no+"' AND colorCode = '"+dataColor.code+"'");
			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}

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
