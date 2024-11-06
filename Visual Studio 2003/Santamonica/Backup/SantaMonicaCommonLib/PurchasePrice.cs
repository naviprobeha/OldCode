using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class PurchasePrice
	{
		public int entryNo;
		public string itemNo;
		public string vendorNo;
		public DateTime startingDate;
		public decimal minimumQuantity;
		public DateTime endingDate;
		public decimal unitCost;

		public string updateMethod;

		public PurchasePrice(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.itemNo = dataReader.GetValue(0).ToString();
			this.vendorNo = dataReader.GetValue(1).ToString();
			this.startingDate = dataReader.GetDateTime(2);
			this.minimumQuantity = dataReader.GetDecimal(3);
			this.endingDate = dataReader.GetDateTime(4);
			this.unitCost = dataReader.GetDecimal(5);
		}


		public PurchasePrice(XmlElement element, Database database, bool saveValues)
		{
			//
			// TODO: Add constructor logic here
			//

			fromDOM(element);
			if (saveValues) save(database);

		}

		public PurchasePrice(Database database, Item item, int quantity, Organization organization)
		{
			//
			// TODO: Add constructor logic here
			//

			getActualPrice(database, item, quantity, organization, DateTime.Now);

		}

		public PurchasePrice(Database database, Item item, int quantity, Organization organization, DateTime date)
		{
			//
			// TODO: Add constructor logic here
			//

			getActualPrice(database, item, quantity, organization, date);

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

				// Vendor No
				startPos = key.FirstChild.Value.IndexOf("(", endPos)+1;
				endPos = key.FirstChild.Value.IndexOf(")", startPos);
				vendorNo = key.FirstChild.Value.Substring(startPos,endPos-startPos); 

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
				if (fieldNo.FirstChild.Value.Equals("2")) vendorNo = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("4"))
				{
					if (fieldValue.Replace("'", "") != "")
					{
						startingDate = DateTime.Parse("20"+fieldValue.Replace("'", "")+" 00:00:00");
					}
				}
				if (fieldNo.FirstChild.Value.Equals("5")) unitCost = decimal.Parse(fieldValue.Replace("'", ""));
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
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();


			if (updateMethod == "D")
			{
				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Purchase Price] WHERE [Item No] = '"+itemNo+"' AND [Vendor No] = '"+vendorNo+"' AND [Starting Date] = '"+startingDate+"' AND [Minimum Quantity] = '"+minimumQuantity+"'");
				if (dataReader.Read())
				{
					this.entryNo = dataReader.GetInt32(0);
					dataReader.Close();
					database.nonQuery("DELETE FROM [Purchase Price] WHERE [Item No] = '"+itemNo+"' AND [Vendor No] = '"+vendorNo+"' AND [Starting Date] = '"+startingDate+"' AND [Minimum Quantity] = '"+minimumQuantity+"'", true);
				}
				dataReader.Close();
			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Purchase Price] WHERE [Item No] = '"+itemNo+"' AND [Vendor No] = '"+vendorNo+"' AND [Starting Date] = '"+startingDate+"' AND [Minimum Quantity] = '"+minimumQuantity+"'");

				if (dataReader.Read())
				{
					this.entryNo = dataReader.GetInt32(0);

					dataReader.Close();
					database.nonQuery("UPDATE [Purchase Price] SET [Unit Cost] = '"+unitCost.ToString().Replace(",",".")+"', [Ending Date] = '"+endingDate+"' WHERE [Item No] = '"+itemNo+"' AND [Vendor No] = '"+vendorNo+"' AND [Starting Date] = '"+startingDate+"' AND [Minimum Quantity] = '"+minimumQuantity+"'");
				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Purchase Price] ([Item No], [Vendor No], [Starting Date], [Minimum Quantity], [Ending Date], [Unit Cost]) VALUES ('"+itemNo+"','"+vendorNo+"','"+startingDate+"','"+minimumQuantity+"','"+endingDate+"','"+unitCost.ToString().Replace(",", ".")+"')");
					this.entryNo = (int)database.getInsertedSeqNo();

				}

			}

			Organizations organizations = new Organizations();
			Organization organization = organizations.getOrganization(database, this.vendorNo, true);
			if (organization != null) organization.updateStopItemPrice();

		}

		public void getActualPrice(Database database, Item item, int quantity, Organization organization, DateTime date)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Item No], [Vendor No], [Starting Date], [Minimum Quantity], [Ending Date], [Unit Cost] FROM [Purchase Price] WHERE [Item No] = '"+item.no+"' AND ([Starting Date] <= '"+date.ToString("yyyy-MM-dd")+"' OR [Starting Date] = '1953-01-01 00:00:00' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '"+date.ToString("yyyy-MM-dd")+"' OR [Ending Date] = '1953-01-01 00:00:00' OR [Ending Date] = '1753-01-01 00:00:00') AND [Minimum Quantity] <= '"+quantity+"' AND [Vendor No] = '"+organization.navisionVendorNo+"' ORDER BY [Unit Cost] ");

			if (dataReader.Read())
			{
				this.entryNo = dataReader.GetInt32(0);
				this.itemNo = dataReader.GetValue(1).ToString();
				this.vendorNo = dataReader.GetValue(2).ToString();
				this.startingDate = dataReader.GetDateTime(3);
				this.endingDate = dataReader.GetDateTime(5);
				this.unitCost = dataReader.GetDecimal(6);
				dataReader.Close();

			}
			else
			{
				dataReader.Close();
			}
		}

	}
}
