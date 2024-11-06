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

		public string sessionId = "";
		public int entryNo;
		public string itemNo = "";
		public string unitOfMeasureCode = "";
		public float unitPrice;
		public float quantity;
		public float amount;

		public string extra1 = "";
		public string extra2 = "";
		public string extra3 = "";
		public string extra4 = "";
		public string extra5 = "";

		public string webUserAccountNo = "";

		public string referenceNo = "";
        public string webSiteCode = "";

        public DateTime fromDate;
        public DateTime toDate;

        public WebCartLine()
        {
        }

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

            this.fromDate = DateTime.Parse(dataRow.ItemArray.GetValue(14).ToString());
            this.toDate = DateTime.Parse(dataRow.ItemArray.GetValue(15).ToString());

            this.webSiteCode = dataRow.ItemArray.GetValue(16).ToString();
		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery;

            if (infojetContext.userSession != null)
            {
                databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_], [From Date], [To Date], [Web Site Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = @entryNo");
            }
            else
            {
                databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_], [From Date], [To Date], [Web Site Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = @entryNo AND [Session ID] = @sessionId");
            }

            databaseQuery.addIntParameter("entryNo", entryNo);
            databaseQuery.addStringParameter("sessionId", infojetContext.sessionId, 100);


            SqlDataReader dataReader = databaseQuery.executeQuery();

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

                fromDate = dataReader.GetDateTime(14);
                toDate = dataReader.GetDateTime(15);

                webSiteCode = dataReader.GetValue(16).ToString();
			}

			dataReader.Close();

		}

		public void save()
		{
            if (fromDate.Year < 1753) fromDate = new DateTime(1753, 1, 1);
            if (toDate.Year < 1753) toDate = new DateTime(1753, 1, 1);

            
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Entry No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = @entryNo AND [Session ID] = @sessionId");
            databaseQuery.addIntParameter("entryNo", entryNo);
            databaseQuery.addStringParameter("sessionId", infojetContext.sessionId, 100);


            SqlDataReader dataReader = databaseQuery.executeQuery();

            bool exists = dataReader.Read();
			dataReader.Close();

			if (exists)
			{
                databaseQuery = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] SET [Item No_] = @itemNo, [Unit Of Measure Code] = @unitOfMeasureCode, [Unit Price] = @unitPrice, [Quantity] = @quantity, [Amount] = @amount, [Extra 1] = @extra1, [Extra 2] = @extra2, [Extra 3] = @extra3, [Extra 4] = @extra4, [Extra 5] = @extra5, [Web User Account No] = @webUserAccountNo, [Reference No_] = @referenceNo, [From Date] = @fromDate, [To Date] = @toDate, [Web Site Code] = @webSiteCode WHERE [Entry No_] = @entryNo AND [Session ID] = @sessionId");
                databaseQuery.addStringParameter("itemNo", itemNo, 20);
                databaseQuery.addStringParameter("unitOfMeasureCode", unitOfMeasureCode, 20);
                databaseQuery.addDecimalParameter("unitPrice", unitPrice);
                databaseQuery.addDecimalParameter("quantity", quantity);
                databaseQuery.addDecimalParameter("amount", amount);
                databaseQuery.addStringParameter("extra1", extra1, 20);
                databaseQuery.addStringParameter("extra2", extra2, 20);
                databaseQuery.addStringParameter("extra3", extra3, 20);
                databaseQuery.addStringParameter("extra4", extra4, 20);
                databaseQuery.addStringParameter("extra5", extra5, 20);
                databaseQuery.addStringParameter("webUserAccountNo", webUserAccountNo, 20);
                databaseQuery.addStringParameter("referenceNo", referenceNo, 50);
                databaseQuery.addDateTimeParameter("fromDate", fromDate);
                databaseQuery.addDateTimeParameter("toDate", toDate);
                databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                databaseQuery.addIntParameter("entryNo", entryNo);
                databaseQuery.addStringParameter("sessionId", sessionId, 100);


                databaseQuery.execute();

			}
			else
			{

                databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] ([Session ID], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_], [From Date], [To Date], [Web Site Code]) VALUES (@sessionId, @itemNo, @unitOfMeasureCode, @unitPrice, @quantity, @amount, @extra1, @extra2, @extra3, @extra4, @extra5, @webUserAccountNo, @referenceNo, @fromDate, @toDate, @webSiteCode)");

                databaseQuery.addStringParameter("itemNo", itemNo, 20);
                databaseQuery.addStringParameter("unitOfMeasureCode", unitOfMeasureCode, 20);
                databaseQuery.addDecimalParameter("unitPrice", unitPrice);
                databaseQuery.addDecimalParameter("quantity", quantity);
                databaseQuery.addDecimalParameter("amount", amount);
                databaseQuery.addStringParameter("extra1", extra1, 20);
                databaseQuery.addStringParameter("extra2", extra2, 20);
                databaseQuery.addStringParameter("extra3", extra3, 20);
                databaseQuery.addStringParameter("extra4", extra4, 20);
                databaseQuery.addStringParameter("extra5", extra5, 20);
                databaseQuery.addStringParameter("webUserAccountNo", webUserAccountNo, 20);
                databaseQuery.addStringParameter("referenceNo", referenceNo, 50);
                databaseQuery.addDateTimeParameter("fromDate", fromDate);
                databaseQuery.addDateTimeParameter("toDate", toDate);
                databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                databaseQuery.addStringParameter("sessionId", sessionId, 100);


                databaseQuery.execute();

                this.entryNo = (int)infojetContext.systemDatabase.getInsertedSeqNo();
			}
			

		}

		public Item getItem()
		{
            //return new Item(infojetContext, this.itemNo);
            return Item.get(infojetContext, this.itemNo);
		}

		public void delete()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Entry No_] = @entryNo AND [Session ID] = @sessionId");
            databaseQuery.addIntParameter("entryNo", entryNo);
            databaseQuery.addStringParameter("sessionId", sessionId, 100);
            databaseQuery.execute();
		}

        public WebCartConfigLineCollection getWebCartConfigLines()
        {
            return WebCartConfigLine.getCartConfigLines(infojetContext, entryNo, true);
        }

	}
}
