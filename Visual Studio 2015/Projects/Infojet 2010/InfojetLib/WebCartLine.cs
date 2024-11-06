using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class WebCartLine
	{
		private Infojet infojetContext;

		public string sessionId;
		public int entryNo;
		public string itemNo;
		public string unitOfMeasureCode;
		public float unitPrice;
		public float quantity;
		public float amount;

		public string extra1;
		public string extra2;
		public string extra3;
		public string extra4;
		public string extra5;

		public string webUserAccountNo;

		public string referenceNo;

        public WebCartLine(Infojet infojetContext, int entryNo)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this.entryNo = entryNo;

            getFromDatabase();
        }

        public WebCartLine(Infojet infojetContext, string sessionId, Item item)
		{
            this.infojetContext = infojetContext;
            this.sessionId = sessionId;

			this.itemNo = item.no;
			this.unitOfMeasureCode = item.salesUnitOfMeasure;

		}

		public WebCartLine(Infojet infojetContext, DataRow dataRow)
		{
            this.infojetContext = infojetContext;

			this.sessionId = dataRow.ItemArray.GetValue(0).ToString();
			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.itemNo = dataRow.ItemArray.GetValue(2).ToString();
			this.unitOfMeasureCode = dataRow.ItemArray.GetValue(3).ToString();
			this.unitPrice = float.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.quantity = float.Parse(dataRow.ItemArray.GetValue(5).ToString());
			this.amount = float.Parse(dataRow.ItemArray.GetValue(6).ToString());

			this.extra1 = dataRow.ItemArray.GetValue(7).ToString();
			this.extra2 = dataRow.ItemArray.GetValue(8).ToString();
			this.extra3 = dataRow.ItemArray.GetValue(9).ToString();
			this.extra4 = dataRow.ItemArray.GetValue(10).ToString();
			this.extra5 = dataRow.ItemArray.GetValue(11).ToString();

			this.webUserAccountNo = dataRow.ItemArray.GetValue(12).ToString();

			this.referenceNo = dataRow.ItemArray.GetValue(13).ToString();
		}

		private void getFromDatabase()
		{
			SqlDataReader dataReader;

            if (infojetContext.userSession != null)
            {
                dataReader = infojetContext.systemDatabase.query("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = '" + this.entryNo + "'");
            }
            else
            {
                dataReader = infojetContext.systemDatabase.query("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = '" + this.entryNo + "' AND [Session ID] = '" + infojetContext.sessionId + "'");
            }

			if (dataReader.Read())
			{

				sessionId = dataReader.GetValue(0).ToString();
				entryNo = dataReader.GetInt32(1);
				itemNo = dataReader.GetValue(2).ToString();
				unitOfMeasureCode = dataReader.GetValue(3).ToString();
				unitPrice = float.Parse(dataReader.GetValue(4).ToString());
				quantity = float.Parse(dataReader.GetValue(5).ToString());
				amount = float.Parse(dataReader.GetValue(6).ToString());

				extra1 = dataReader.GetValue(7).ToString();
				extra2 = dataReader.GetValue(8).ToString();
				extra3 = dataReader.GetValue(9).ToString();
				extra4 = dataReader.GetValue(10).ToString();
				extra5 = dataReader.GetValue(11).ToString();

				webUserAccountNo = dataReader.GetValue(12).ToString();

				referenceNo = dataReader.GetValue(13).ToString();
			}

			dataReader.Close();

		}

		public void save()
		{
            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Entry No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = '" + this.entryNo + "' AND [Session ID] = '" + sessionId + "'");
			bool exists = dataReader.Read();
			dataReader.Close();

			if (exists)
			{
                infojetContext.systemDatabase.nonQuery("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] SET [Item No_] = '" + this.itemNo + "', [Unit Of Measure Code] = '" + this.unitOfMeasureCode + "', [Unit Price] = '" + this.unitPrice.ToString().Replace(",", ".") + "', [Quantity] = '" + this.quantity.ToString().Replace(",", ".") + "', [Amount] = '" + this.amount.ToString().Replace(",", ".") + "', [Extra 1] = '" + this.extra1 + "', [Extra 2] = '" + this.extra2 + "', [Extra 3] = '" + this.extra3 + "', [Extra 4] = '" + this.extra4 + "', [Extra 5] = '" + this.extra5 + "', [Web User Account No] = '" + this.webUserAccountNo + "', [Reference No_] = '" + this.referenceNo + "' WHERE [Entry No_] = '" + this.entryNo + "' AND [Session ID] = '"+sessionId+"'");
			}
			else
			{

                infojetContext.systemDatabase.nonQuery("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] ([Session ID], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_]) VALUES ('" + this.sessionId + "', '" + this.itemNo + "', '" + this.unitOfMeasureCode + "', '" + this.unitPrice.ToString().Replace(",", ".") + "', '" + this.quantity.ToString().Replace(",", ".") + "', '" + this.amount.ToString().Replace(",", ".") + "', '" + this.extra1 + "', '" + this.extra2 + "', '" + this.extra3 + "', '" + this.extra4 + "', '" + this.extra5 + "', '" + this.webUserAccountNo + "', '" + referenceNo + "')");
			}
			

		}

		public Item getItem()
		{
            return new Item(infojetContext.systemDatabase, this.itemNo);
		}

		public void delete()
		{
            infojetContext.systemDatabase.nonQuery("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = '" + this.entryNo + "' AND [Session ID] = '" + this.sessionId + "'");
		}

	}
}
