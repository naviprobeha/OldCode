using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class ItemPrice
	{
		public int entryNo;
		public string itemNo;
		public int salesType;
		public string salesCode;
		public DateTime startingDate;
		public decimal minimumQuantity;
		public DateTime endingDate;
		public decimal unitPrice;

		public string updateMethod;

		public ItemPrice(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.itemNo = dataReader.GetValue(0).ToString();
			this.salesType = dataReader.GetInt32(1);
			this.salesCode = dataReader.GetValue(2).ToString();
			this.startingDate = dataReader.GetDateTime(3);
			this.minimumQuantity = dataReader.GetDecimal(4);
			this.endingDate = dataReader.GetDateTime(5);
			this.unitPrice = dataReader.GetDecimal(6);
		}


		public ItemPrice(XmlElement element, Database database, bool saveValues)
		{
			//
			// TODO: Add constructor logic here
			//

			fromDOM(element);
			if (saveValues) save(database);

		}

		public ItemPrice(Database database, Item item, int quantity, Customer customer)
		{
			//
			// TODO: Add constructor logic here
			//

			getActualPrice(database, item, quantity, customer);

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

				// Sales Type
				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				salesType = int.Parse(key.FirstChild.Value.Substring(startPos,endPos-startPos)); 

				// Sales Code
				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				salesCode = key.FirstChild.Value.Substring(startPos,endPos-startPos); 

				// Start Date
				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				if (key.FirstChild.Value.Substring(startPos,endPos-startPos) != "")
				{
					startingDate = DateTime.Parse("20"+key.FirstChild.Value.Substring(startPos,endPos-startPos)+" 00:00:00"); 
				}

				// Currency Code
				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);

				// Variant Code
				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);

				// Unit Code
				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);

				// Minimal Quantity
				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				minimumQuantity = decimal.Parse(key.FirstChild.Value.Substring(startPos,endPos-startPos));
			}

			XmlNodeList fields = recordElement.GetElementsByTagName("F");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
				if (fieldNo.FirstChild.Value.Equals("1")) itemNo = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("2")) salesCode = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("4"))
				{
					if (fieldValue.Replace("'", "") != "")
					{
						startingDate = DateTime.Parse("20"+fieldValue.Replace("'", "")+" 00:00:00");
					}
				}
				if (fieldNo.FirstChild.Value.Equals("5")) unitPrice = decimal.Parse(fieldValue.Replace("'", ""));
				if (fieldNo.FirstChild.Value.Equals("13")) salesType = int.Parse(fieldValue.Replace("'", ""));
				if (fieldNo.FirstChild.Value.Equals("14")) minimumQuantity = decimal.Parse(fieldValue.Replace("'", ""));
				if (fieldNo.FirstChild.Value.Equals("15")) 
				{
					if (fieldValue.Replace("'", "") != "")
					{
						endingDate = DateTime.Parse("20"+fieldValue.Replace("'", "")+" 00:00:00");
					}
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
				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Item Price] WHERE [Item No] = '"+itemNo+"' AND [Sales Type] = '"+salesType+"' AND [Sales Code] = '"+salesCode+"' AND [Starting Date] = '"+startingDate+"' AND [Minimum Quantity] = '"+minimumQuantity+"'");
				if (dataReader.Read())
				{
					this.entryNo = dataReader.GetInt32(0);
					dataReader.Close();
					database.nonQuery("DELETE FROM [Item Price] WHERE [Item No] = '"+itemNo+"' AND [Sales Type] = '"+salesType+"' AND [Sales Code] = '"+salesCode+"' AND [Starting Date] = '"+startingDate+"' AND [Minimum Quantity] = '"+minimumQuantity+"'", true);
					//synchQueue.enqueueAllAgents(database, 3, this.entryNo.ToString(), 2);
				}
			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Item Price] WHERE [Item No] = '"+itemNo+"' AND [Sales Type] = '"+salesType+"' AND [Sales Code] = '"+salesCode+"' AND [Starting Date] = '"+startingDate+"' AND [Minimum Quantity] = '"+minimumQuantity+"'");

				if (dataReader.Read())
				{
					this.entryNo = dataReader.GetInt32(0);

					dataReader.Close();
					database.nonQuery("UPDATE [Item Price] SET [Unit Price] = '"+unitPrice.ToString().Replace(",",".")+"', [Ending Date] = '"+endingDate+"' WHERE [Item No] = '"+itemNo+"' AND [Sales Type] = '"+salesType+"' AND [Sales Code] = '"+salesCode+"' AND [Starting Date] = '"+startingDate+"' AND [Minimum Quantity] = '"+minimumQuantity+"'");
				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Item Price] ([Item No], [Sales Type], [Sales Code], [Starting Date], [Minimum Quantity], [Ending Date], [Unit Price]) VALUES ('"+itemNo+"','"+salesType+"','"+salesCode+"','"+startingDate+"','"+minimumQuantity+"','"+endingDate+"','"+unitPrice.ToString().Replace(",", ".")+"')");
					this.entryNo = (int)database.getInsertedSeqNo();

				}
				//synchQueue.enqueueAllAgents(database, 3, this.entryNo.ToString(), 0);
			}
			agentItemPriceUpdates.setUpdate(database, itemNo);
		}

		public void getActualPrice(Database database, Item item, int quantity, Customer customer)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Item No], [Sales Type], [Sales Code], [Starting Date], [Minimum Quantity], [Ending Date], [Unit Price] FROM [Item Price] WHERE [Item No] = '"+item.no+"' AND ([Starting Date] <= GETDATE() OR [Starting Date] = '1953-01-01 00:00:00' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= GETDATE() OR [Ending Date] = '1953-01-01 00:00:00' OR [Ending Date] = '1753-01-01 00:00:00') AND [Minimum Quantity] <= '"+quantity+"' AND ([Sales Type] = 0 AND [Sales Code] = '"+customer.no+"') ORDER BY [Unit Price] ");

			if (dataReader.Read())
			{
				this.entryNo = dataReader.GetInt32(0);
				this.itemNo = dataReader.GetValue(1).ToString();
				this.salesType = dataReader.GetInt32(2);
				this.salesCode = dataReader.GetValue(3).ToString();
				this.startingDate = dataReader.GetDateTime(4);
				//this.minimumQuantity = dataReader.GetInt32(5);
				this.endingDate = dataReader.GetDateTime(6);
				this.unitPrice = dataReader.GetDecimal(7);
				dataReader.Close();

			}
			else
			{
				dataReader.Close();

				dataReader = database.query("SELECT [Entry No], [Item No], [Sales Type], [Sales Code], [Starting Date], [Minimum Quantity], [Ending Date], [Unit Price] FROM [Item Price] WHERE [Item No] = '"+item.no+"' AND ([Starting Date] <= GETDATE() OR [Starting Date] = '1953-01-01 00:00:00' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= GETDATE() OR [Ending Date] = '1953-01-01 00:00:00' OR [Ending Date] = '1753-01-01 00:00:00') AND [Minimum Quantity] <= '"+quantity+"' AND ([Sales Type] = 1 AND [Sales Code] = '"+customer.priceGroupCode+"') ORDER BY [Unit Price] ");

				if (dataReader.Read())
				{
					this.entryNo = dataReader.GetInt32(0);
					this.itemNo = dataReader.GetValue(1).ToString();
					this.salesType = dataReader.GetInt32(2);
					this.salesCode = dataReader.GetValue(3).ToString();
					this.startingDate = dataReader.GetDateTime(4);
					//this.minimumQuantity = dataReader.GetInt32(5);
					this.endingDate = dataReader.GetDateTime(6);
					this.unitPrice = dataReader.GetDecimal(7);
					dataReader.Close();
				}
				else
				{
					dataReader.Close();

					dataReader = database.query("SELECT [Entry No], [Item No], [Sales Type], [Sales Code], [Starting Date], [Minimum Quantity], [Ending Date], [Unit Price] FROM [Item Price] WHERE [Item No] = '"+item.no+"' AND ([Starting Date] <= GETDATE() OR [Starting Date] = '1953-01-01 00:00:00' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= GETDATE() OR [Ending Date] = '1953-01-01 00:00:00' OR [Ending Date] = '1753-01-01 00:00:00') AND [Minimum Quantity] <= '"+quantity+"' AND ([Sales Type] = 2) ORDER BY [Unit Price] ");

					if (dataReader.Read())
					{
						this.entryNo = dataReader.GetInt32(0);
						this.itemNo = dataReader.GetValue(1).ToString();
						this.salesType = dataReader.GetInt32(2);
						this.salesCode = dataReader.GetValue(3).ToString();
						this.startingDate = dataReader.GetDateTime(4);
						//this.minimumQuantity = dataReader.GetInt32(5);
						this.endingDate = dataReader.GetDateTime(6);
						this.unitPrice = dataReader.GetDecimal(7);

					}
					dataReader.Close();
					
				}
			}
		}

	}
}
