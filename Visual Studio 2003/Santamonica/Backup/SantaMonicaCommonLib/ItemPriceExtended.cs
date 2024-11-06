using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class ItemPriceExtended
	{
		public int entryNo;
		public string itemNo;
		public DateTime startingDate;
		public DateTime endingDate;
		public string customerPriceGroup;
		public string unitOfMeasureCode;
		public decimal quantityFrom;
		public decimal quantityTo;
		public decimal lineAmount;

		public string updateMethod;

		public ItemPriceExtended(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.itemNo = dataReader.GetValue(0).ToString();
			this.startingDate = dataReader.GetDateTime(1);
			this.endingDate = dataReader.GetDateTime(2);
			this.customerPriceGroup = dataReader.GetValue(3).ToString();
			this.unitOfMeasureCode = dataReader.GetValue(4).ToString();
			this.quantityFrom = dataReader.GetDecimal(5);
			this.quantityTo = dataReader.GetDecimal(6);
			this.lineAmount = dataReader.GetDecimal(7);
		}


		public ItemPriceExtended(XmlElement element, Database database, bool saveValues)
		{
			//
			// TODO: Add constructor logic here
			//

			fromDOM(element);
			if (saveValues) save(database);

		}

		public ItemPriceExtended(Database database, Item item, int quantity, Customer customer)
		{
			//
			// TODO: Add constructor logic here
			//

			getActualLineAmount(database, item, quantity, customer);

		}

		public void fromDOM(XmlElement recordElement)
		{
			XmlAttribute updateMethod = recordElement.GetAttributeNode("M");
			XmlAttribute key = recordElement.GetAttributeNode("KEY");
			this.updateMethod = updateMethod.FirstChild.Value;

			startingDate = new DateTime(1753,1,1,0,0,0);
			endingDate = new DateTime(1753,1,1,0,0,0);

			if (this.updateMethod.Equals("D"))
			{
				// Item No.
				int startPos = key.FirstChild.Value.IndexOf("(")+1;
				int endPos = key.FirstChild.Value.IndexOf(")");
				itemNo = key.FirstChild.Value.Substring(startPos,endPos-startPos); 

				// From
				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				quantityFrom = decimal.Parse(key.FirstChild.Value.Substring(startPos,endPos-startPos));

				// Start Date
				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				if (key.FirstChild.Value.Substring(startPos,endPos-startPos) != "")
				{
					startingDate = DateTime.Parse("20"+key.FirstChild.Value.Substring(startPos,endPos-startPos)+" 00:00:00"); 
				}

				// Customer Price Group
				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				customerPriceGroup = key.FirstChild.Value.Substring(startPos,endPos-startPos);

			}


			XmlNodeList fields = recordElement.GetElementsByTagName("F");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				try
				{
					if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
					if (fieldNo.FirstChild.Value.Equals("1")) itemNo = fieldValue.Replace("'", "");
					if (fieldNo.FirstChild.Value.Equals("2")) unitOfMeasureCode = fieldValue.Replace("'", "");
					if (fieldNo.FirstChild.Value.Equals("3")) quantityFrom = decimal.Parse(fieldValue.Replace("'", ""));
					if (fieldNo.FirstChild.Value.Equals("4")) quantityTo = decimal.Parse(fieldValue.Replace("'", ""));
					if (fieldNo.FirstChild.Value.Equals("5")) lineAmount = decimal.Parse(fieldValue.Replace("'", ""));

					if (fieldNo.FirstChild.Value.Equals("6"))
					{
						if (fieldValue.Replace("'", "") != "")
						{
							startingDate = DateTime.Parse("20"+fieldValue.Replace("'", "")+" 00:00:00");
						}
					}

					if (fieldNo.FirstChild.Value.Equals("7"))
					{
						if (fieldValue.Replace("'", "") != "")
						{
							endingDate = DateTime.Parse("20"+fieldValue.Replace("'", "")+" 00:00:00");
						}
					}

					if (fieldNo.FirstChild.Value.Equals("8")) customerPriceGroup = fieldValue.Replace("'", "");
				}
				catch(Exception e)
				{
					
					throw new Exception("Error on field "+fieldNo.FirstChild.Value+" ("+fieldValue+"): "+e.Message);
				}
				i++;
			}

		}

		public void save(Database database)
		{

			//SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
			AgentItemPriceUpdates agentItemPriceUpdates = new AgentItemPriceUpdates();

			if (updateMethod == "D")
			{
				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Item Price Extended] WHERE [Item No] = '"+itemNo+"' AND [Quantity From] = '"+quantityFrom.ToString().Replace(",", ".")+"' AND [Starting Date] = '"+startingDate+"' AND [Customer Price Group] = '"+customerPriceGroup+"'");
				if (dataReader.Read())
				{
					this.entryNo = dataReader.GetInt32(0);
					dataReader.Close();
					database.nonQuery("DELETE FROM [Item Price Extended] WHERE [Item No] = '"+itemNo+"' AND [Quantity From] = '"+quantityFrom.ToString().Replace(",", ".")+"' AND [Starting Date] = '"+startingDate+"' AND [Customer Price Group] = '"+customerPriceGroup+"'", true);
					//synchQueue.enqueueAllAgents(database, 7, this.entryNo.ToString(), 2);
				}
			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Item Price Extended] WHERE [Item No] = '"+itemNo+"' AND [Quantity From] = '"+quantityFrom.ToString().Replace(",", ".")+"' AND [Starting Date] = '"+startingDate+"' AND [Customer Price Group] = '"+customerPriceGroup+"'");

				if (dataReader.Read())
				{
					this.entryNo = dataReader.GetInt32(0);

					dataReader.Close();
					database.nonQuery("UPDATE [Item Price Extended] SET [Line Amount] = '"+lineAmount.ToString().Replace(",", ".")+"', [Ending Date] = '"+endingDate+"', [Quantity To] = '"+quantityTo.ToString().Replace(",", ".")+"' WHERE [Item No] = '"+itemNo+"' AND [Quantity From] = '"+quantityFrom.ToString().Replace(",", ".")+"' AND [Starting Date] = '"+startingDate+"' AND [Customer Price Group] = '"+customerPriceGroup+"'");
				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Item Price Extended] ([Item No], [Starting Date], [Ending Date], [Customer Price Group], [Unit of Measure Code], [Quantity From], [Quantity To], [Line Amount]) VALUES ('"+itemNo+"','"+startingDate+"','"+endingDate+"','"+customerPriceGroup+"','"+unitOfMeasureCode+"','"+quantityFrom.ToString().Replace(",", ".")+"','"+quantityTo.ToString().Replace(",", ".")+"','"+lineAmount.ToString().Replace(",", ".")+"')");
					this.entryNo = (int)database.getInsertedSeqNo();

				}
				//synchQueue.enqueueAllAgents(database, 7, this.entryNo.ToString(), 0);
				agentItemPriceUpdates.setUpdate(database, itemNo);

			}

		}

		public void getActualLineAmount(Database database, Item item, int quantity, Customer customer)
		{

			SqlDataReader dataReader = database.query("SELECT [Entry No], [Item No], [Starting Date], [Ending Date], [Customer Price Group], [Unit of Measure Code], [Quantity From], [Quantity To], [Line Amount] FROM [Item Price Extended] WHERE [Item No] = '"+item.no+"' AND ([Starting Date] <= GETDATE() OR [Starting Date] = '1953-01-01 00:00:00' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= GETDATE() OR [Ending Date] = '1953-01-01 00:00:00' OR [Ending Date] = '1753-01-01 00:00:00') AND [Quantity From] <= '"+quantity+"' AND [Quantity To] >= '"+quantity+"' AND ([Customer Price Group] = '"+customer.priceGroupCode+"' OR [Customer Price Group] = '') ORDER BY [Line Amount]");

			if (dataReader.Read())
			{
				this.entryNo = dataReader.GetInt32(0);
				this.itemNo = dataReader.GetValue(1).ToString();
				this.startingDate = dataReader.GetDateTime(2);
				this.endingDate = dataReader.GetDateTime(3);
				this.customerPriceGroup = dataReader.GetValue(4).ToString();
				this.unitOfMeasureCode = dataReader.GetValue(5).ToString();
				this.quantityFrom = dataReader.GetDecimal(6);
				this.quantityTo = dataReader.GetDecimal(7);
				this.lineAmount = dataReader.GetDecimal(8);
				dataReader.Close();

			}
			else
			{
				dataReader.Close();
			}
			
		}

	}
}
