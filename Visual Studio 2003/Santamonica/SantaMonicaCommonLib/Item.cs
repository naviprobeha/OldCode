using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class Item
	{
		public string no;
		public string description;
		public string searchDescription;
		public string unitOfMeasure;
		public decimal unitPrice;
		public bool addStopItem;
		public string stopItemNo;
		public bool requireId;
		public bool invoiceToJbv;
		public string connectionItemNo;
		public bool putToDeath;
		public bool availableInMobile;
		public bool requireCashPayment;
		public decimal directCost;
		public string categoryCode;
		public bool availableOnWeb;
		public string idGroupCode;

		public string updateMethod;

		public Item(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.no = dataReader.GetValue(0).ToString();
			this.description = dataReader.GetValue(1).ToString();
			this.searchDescription = dataReader.GetValue(2).ToString();
			this.unitPrice = dataReader.GetDecimal(3);

			this.addStopItem = false;
			if (dataReader.GetValue(4).ToString() == "1") this.addStopItem = true;

			this.requireId = false;
			if (dataReader.GetValue(5).ToString() == "1") this.requireId = true;

			this.invoiceToJbv = false;
			if (dataReader.GetValue(6).ToString() == "1") this.invoiceToJbv = true;

			this.stopItemNo = dataReader.GetValue(7).ToString();

			this.connectionItemNo = dataReader.GetValue(8).ToString();

			this.unitOfMeasure = dataReader.GetValue(9).ToString();

			this.putToDeath = false;
			if (dataReader.GetValue(10).ToString() == "1") this.putToDeath = true;

			this.availableInMobile = false;
			if (dataReader.GetValue(11).ToString() == "1") this.availableInMobile = true;

			this.requireCashPayment = false;
			if (dataReader.GetValue(12).ToString() == "1") this.requireCashPayment = true;

			this.directCost = dataReader.GetDecimal(13);

			this.categoryCode = dataReader.GetValue(14).ToString();

			this.availableOnWeb = false;
			if (dataReader.GetValue(15).ToString() == "1") this.availableOnWeb = true;

			this.idGroupCode = dataReader.GetValue(16).ToString();

		}


		public Item(XmlElement element, Database database, bool saveValues)
		{
			//
			// TODO: Add constructor logic here
			//

			fromDOM(element);
			if (saveValues) save(database);

		}


		public void fromDOM(XmlElement recordElement)
		{
			XmlAttribute updateMethod = recordElement.GetAttributeNode("M");
			XmlAttribute key = recordElement.GetAttributeNode("KEY");
			this.updateMethod = updateMethod.FirstChild.Value;

			if (this.updateMethod.Equals("D"))
			{
				int startPos = key.FirstChild.Value.IndexOf("(")+1;
				int endPos = key.FirstChild.Value.IndexOf(")");
				no = key.FirstChild.Value.Substring(startPos,endPos-startPos); 
			}

			this.availableInMobile = false;
			this.availableOnWeb = false;
			this.requireCashPayment = false;
			this.addStopItem = false;
			this.requireId = false;
			this.putToDeath = false;
			this.invoiceToJbv = false;

			XmlNodeList fields = recordElement.GetElementsByTagName("F");
			int i = 0;
			while (i < fields.Count)
			{
				XmlElement field = (XmlElement)fields.Item(i);
	
				XmlAttribute fieldNo = field.GetAttributeNode("NO");
				String fieldValue = "";

				if (field.HasChildNodes) fieldValue = field.FirstChild.Value;
				
				if (fieldNo.FirstChild.Value.Equals("1")) no = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("3")) description = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("4")) searchDescription = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("8")) unitOfMeasure = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("18")) unitPrice = decimal.Parse(fieldValue.Replace("'", ""));
				if (fieldNo.FirstChild.Value.Equals("25")) directCost = decimal.Parse(fieldValue.Replace("'", ""));
				if (fieldNo.FirstChild.Value.Equals("70000")) connectionItemNo = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("70009")) stopItemNo = fieldValue.Replace("'", "");
				if (fieldNo.FirstChild.Value.Equals("70011")) categoryCode = fieldValue.Replace("'", "");

				if (fieldNo.FirstChild.Value.Equals("70008")) 
				{
					if (fieldValue.Replace("'", "") == "TRUE")
					{
						this.addStopItem = true;
					}
				}

				if (fieldNo.FirstChild.Value.Equals("70003")) 
				{
					if (fieldValue.Replace("'", "") == "TRUE")
					{
						this.requireId = true;
					}
				}
				

				if (fieldNo.FirstChild.Value.Equals("70004")) 
				{
					if (fieldValue.Replace("'", "") == "TRUE")
					{
						this.invoiceToJbv = true;
					}
				}

				if (fieldNo.FirstChild.Value.Equals("70006")) 
				{
					if (fieldValue.Replace("'", "") == "TRUE")
					{
						this.availableInMobile = true;
					}
				}

				if (fieldNo.FirstChild.Value.Equals("70007")) 
				{
					if (fieldValue.Replace("'", "") == "TRUE")
					{
						this.putToDeath = true;
					}
				}

				if (fieldNo.FirstChild.Value.Equals("70010")) 
				{
					if (fieldValue.Replace("'", "") == "TRUE")
					{
						this.requireCashPayment = true;
					}
				}

				if (fieldNo.FirstChild.Value.Equals("70012")) 
				{
					if (fieldValue.Replace("'", "") == "TRUE")
					{
						this.availableOnWeb = true;
					}
				}

				i++;
			}

		}

		public void save(Database database)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			int addStopItemVal = 0;
			int requireIdVal = 0;
			int invoiceToJbvVal = 0;
			int putToDeathVal = 0;
			int availableInMobileVal = 0;
			int requireCashPaymentVal = 0;
			int availableOnWebVal = 0;
			if (this.addStopItem) addStopItemVal = 1;
			if (this.requireId) requireIdVal = 1;
			if (this.invoiceToJbv) invoiceToJbvVal = 1;
			if (this.putToDeath) putToDeathVal = 1;
			if (this.availableInMobile) availableInMobileVal = 1;
			if (this.requireCashPayment) requireCashPaymentVal = 1;
			if (this.availableOnWeb) availableOnWebVal = 1;

			if (updateMethod == "D")
			{
				database.nonQuery("DELETE FROM [Item] WHERE [No] = '"+no+"'");
				synchQueue.enqueueAllAgents(database, 2, this.no, 2);

			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [No] FROM Item WHERE [No] = '"+no+"'");

				if (dataReader.Read())
				{
					dataReader.Close();
					database.nonQuery("UPDATE [Item] SET [Description] = '"+description+"', [Search Description] = '"+searchDescription+"', [Unit Price] = '"+unitPrice.ToString().Replace(",", ".")+"', [Add STOP-item] = '"+addStopItemVal+"', [Require ID] = '"+requireIdVal+"', [Invoice To JBV] = '"+invoiceToJbvVal+"', [STOP Item No] = '"+this.stopItemNo+"', [Connection Item No] = '"+this.connectionItemNo+"', [Unit Of Measure] = '"+unitOfMeasure+"', [Put To Death] = '"+putToDeathVal+"', [Available In Mobile] = '"+availableInMobileVal+"', [Require Cash Payment] = '"+requireCashPaymentVal+"', [Direct Cost] = '"+directCost.ToString().Replace(",", ".")+"', [Category Code] = '"+this.categoryCode+"', [Available On Web] = '"+availableOnWebVal+"', [ID Group Code] = '"+this.idGroupCode+"' WHERE [No] = '"+no+"'");

				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Item] ([No], [Description], [Search Description], [Unit Price], [Add STOP-item], [Require ID], [Invoice To JBV], [STOP Item No], [Connection Item No], [Unit Of Measure], [Put To Death], [Available In Mobile], [Require Cash Payment], [Direct Cost], [Category Code], [Available On Web], [ID Group Code]) VALUES ('"+no+"','"+description+"','"+searchDescription+"','"+unitPrice.ToString().Replace(",", ".")+"','"+addStopItemVal+"','"+requireIdVal+"','"+invoiceToJbvVal+"','"+this.stopItemNo+"','"+this.connectionItemNo+"','"+unitOfMeasure+"','"+putToDeathVal+"','"+availableInMobileVal+"','"+requireCashPaymentVal+"','"+directCost.ToString().Replace(",", ".")+"', '"+this.categoryCode+"', '"+availableOnWebVal+"', '"+idGroupCode+"')");
				}

				synchQueue.enqueueAllAgents(database, 2, this.no, 0);

			}
		}

		public bool sameAs(Item compareItem)
		{
			if (compareItem.description != description) return false;
			if (compareItem.searchDescription != searchDescription) return false;
			if (compareItem.unitOfMeasure != unitOfMeasure) return false;
			if (compareItem.unitPrice != unitPrice) return false;			
			if (compareItem.connectionItemNo != connectionItemNo) return false;
			if (compareItem.stopItemNo != stopItemNo) return false;
			if (compareItem.categoryCode != categoryCode) return false;

			if (compareItem.addStopItem != addStopItem) return false;
			if (compareItem.requireId != requireId) return false;
			if (compareItem.invoiceToJbv != invoiceToJbv) return false;
			if (compareItem.availableInMobile != availableInMobile) return false;
			if (compareItem.putToDeath != putToDeath) return false;
			if (compareItem.directCost != directCost) return false;
			if (compareItem.requireCashPayment != requireCashPayment) return false;
			if (compareItem.availableOnWeb != availableOnWeb) return false;

			return true;
		}
	}
}
