using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebLayoutRows.
	/// </summary>
	public class WebCartLines
	{
		private Infojet infojetContext;


        public WebCartLines(Infojet infojetContext)
		{
			//
			// TODO: Add constructor logic here
			//

            this.infojetContext = infojetContext;

		}

		public DataSet getCartLines(string sessionId, string webSiteCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT l.[Session ID], l.[Entry No_], l.[Item No_], l.[Unit Of Measure Code], l.[Unit Price], l.[Quantity], l.[Amount], l.[Extra 1], l.[Extra 2], l.[Extra 3], l.[Extra 4], l.[Extra 5], l.[Web User Account No], l.[Reference No_], l.[From Date], l.[To Date], l.[Web Site Code], i.[Description], t.[Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] t ON t.[Item No_] = i.[No_] AND t.[Language Code] = @languageCode WHERE l.[Session ID] = @sessionId AND l.[Web Site Code] = @webSiteCode AND l.[Item No_] = i.[No_] ORDER BY l.[Entry No_]");
            databaseQuery.addStringParameter("sessionId", sessionId, 100);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

        public DataSet getCartLines2(string sessionId, string webSiteCode)
        {
            if (infojetContext.configuration.nav2013mode)
            {
                //if (webSiteCode == "SCANDIC") throw new Exception("SELECT l.[Session ID], l.[Entry No_], l.[Item No_], l.[Unit Of Measure Code], l.[Unit Price], l.[Quantity], l.[Amount], l.[Extra 1], l.[Extra 2], l.[Extra 3], l.[Extra 4], l.[Extra 5], l.[Web User Account No], l.[Reference No_], l.[From Date], l.[To Date], l.[Web Site Code], i.[Description], t.[Description], v.[Web Model No_], ms.[Availability], ms.[Visibility], ms.[Fixed Inventory Value], it.[Availability], it.[Visibility], it.[Fixed Inventory Value], ms.[Min_ Orderable Quantity], it.[Min_ Orderable Quantity] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] t ON t.[Item No_] = i.[No_] AND t.[Language Code] = @languageCode LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] v ON v.[Item No_] = i.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Item Setting") + "] ms ON v.[Web Model No_] = ms.[No_] AND ms.[Type] = 1 LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Item Setting") + "] it ON i.[No_] = it.[No_] AND it.[Type] = 0 WHERE l.[Session ID] = @sessionId AND l.[Web Site Code] = @webSiteCode AND l.[Item No_] = i.[No_] AND ((v.[Item Variant Code] = l.[Extra 1]) OR (v.[Item Variant Code] IS NULL)) ORDER BY l.[Entry No_]");
                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT l.[Session ID], l.[Entry No_], l.[Item No_], l.[Unit Of Measure Code], l.[Unit Price], l.[Quantity], l.[Amount], l.[Extra 1], l.[Extra 2], l.[Extra 3], l.[Extra 4], l.[Extra 5], l.[Web User Account No], l.[Reference No_], l.[From Date], l.[To Date], l.[Web Site Code], i.[Description], t.[Description], v.[Web Model No_], ms.[Availability], ms.[Visibility], ms.[Fixed Inventory Value], it.[Availability], it.[Visibility], it.[Fixed Inventory Value], ms.[Min_ Orderable Quantity], it.[Min_ Orderable Quantity], (SELECT [Fixed Inventory Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Inventory") + "] varInv WHERE varInv.[Item No_] = l.[Item No_] AND varInv.[Variant Code] = l.[Extra 1]) as fixedInvValue FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] t ON t.[Item No_] = i.[No_] AND t.[Language Code] = @languageCode LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] v ON v.[Item No_] = i.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Item Setting") + "] ms ON v.[Web Model No_] = ms.[No_] AND ms.[Type] = 1 LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Item Setting") + "] it ON i.[No_] = it.[No_] AND it.[Type] = 0 WHERE l.[Session ID] = @sessionId AND l.[Web Site Code] = @webSiteCode AND l.[Item No_] = i.[No_] AND ((v.[Item Variant Code] = l.[Extra 1]) OR (v.[Item Variant Code] IS NULL)) ORDER BY l.[Entry No_]");
                databaseQuery.addStringParameter("sessionId", sessionId, 100);
                databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);


                SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                return dataSet;
            }
            else
            {
                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT l.[Session ID], l.[Entry No_], l.[Item No_], l.[Unit Of Measure Code], l.[Unit Price], l.[Quantity], l.[Amount], l.[Extra 1], l.[Extra 2], l.[Extra 3], l.[Extra 4], l.[Extra 5], l.[Web User Account No], l.[Reference No_], l.[From Date], l.[To Date], l.[Web Site Code], i.[Description], t.[Description], v.[Web Model No_], ms.[Availability], ms.[Visibility], ms.[Fixed Inventory Value], it.[Availability], it.[Visibility], it.[Fixed Inventory Value], ms.[Min_ Orderable Quantity], it.[Min_ Orderable Quantity] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] t ON t.[Item No_] = i.[No_] AND t.[Language Code] = @languageCode LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] v ON v.[Item No_] = i.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Item Setting") + "] ms ON v.[Web Model No_] = ms.[No_] AND ms.[Type] = 1 LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Item Setting") + "] it ON i.[No_] = it.[No_] AND it.[Type] = 0 WHERE l.[Session ID] = @sessionId AND l.[Web Site Code] = @webSiteCode AND l.[Item No_] = i.[No_] ORDER BY l.[Entry No_]");
                databaseQuery.addStringParameter("sessionId", sessionId, 100);
                databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);


                SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                return dataSet;
            }
        }
 
        public DataSet getCartLinesWithVAT(string sessionId, string webSiteCode, string vatBusPostingGroup)
        {

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT l.[Session ID], l.[Entry No_], l.[Item No_], l.[Unit Of Measure Code], l.[Unit Price], l.[Quantity], l.[Amount], l.[Extra 1], l.[Extra 2], l.[Extra 3], l.[Extra 4], l.[Extra 5], l.[Web User Account No], l.[Reference No_], l.[From Date], l.[To Date], l.[Web Site Code], i.[Description], t.[Description], "+infojetContext.systemDatabase.convertField("p.[VAT %]")+", i.[Gross Weight], i.[Unit Volume], p.[VAT Calculation Type] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] t ON t.[Item No_] = i.[No_] AND t.[Language Code] = @languageCode AND t.[Variant Code] = '' LEFT JOIN [" + infojetContext.systemDatabase.getTableName("VAT Posting Setup") + "] p ON p.[VAT Prod_ Posting Group] = i.[VAT Prod_ Posting Group] AND p.[VAT Bus_ Posting Group] = @vatBusPostingGroup WHERE l.[Session ID] = @sessionId AND l.[Web Site Code] = @webSiteCode AND l.[Item No_] = i.[No_] ORDER BY l.[Entry No_]");
            databaseQuery.addStringParameter("sessionId", sessionId, 100);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);
            databaseQuery.addStringParameter("vatBusPostingGroup", vatBusPostingGroup, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

        public float calcModelQuantity(string sessionId, string webSiteCode, string itemNo)
        {

            //DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] v, [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] v2 WHERE l.[Session ID] = @sessionId AND l.[Web Site Code] = @webSiteCode AND l.[Item No_] = v.[Item No_] AND v.[Web Model No_] = v2.[Web Model No_] AND v2.[Item No_] = @itemNo");
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] l WHERE l.[Session ID] = @sessionId AND l.[Web Site Code] = @webSiteCode AND l.[Item No_] = @itemNo");
            databaseQuery.addStringParameter("sessionId", sessionId, 100);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("itemNo", itemNo, 20);

            float qty = 0;

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) qty = float.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            return qty;
        }


        public DataSet getCartLines(string sessionId, string itemNo, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo, string webSiteCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_], [From Date], [To Date], [Web Site Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Session ID] = @sessionId AND [Item No_] = @itemNo AND [Extra 1] = @extra1 AND [Extra 2] = @extra2 AND [Extra 3] = @extra3 AND [Extra 4] = @extra4 AND [Extra 5] = @extra5 AND [Reference No_] = @referenceNo AND [Web Site Code] = @webSiteCode ORDER BY [Entry No_]");
            databaseQuery.addStringParameter("sessionId", sessionId, 100);
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("extra1", extra1, 20);
            databaseQuery.addStringParameter("extra2", extra2, 20);
            databaseQuery.addStringParameter("extra3", extra3, 20);
            databaseQuery.addStringParameter("extra4", extra4, 20);
            databaseQuery.addStringParameter("extra5", extra5, 20);
            databaseQuery.addStringParameter("referenceNo", referenceNo, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

		public DataSet getCartLines(string sessionId, string itemNo, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo, DateTime fromDate, DateTime toDate, string webSiteCode)
		{
            if (fromDate.Year < 1753) fromDate = new DateTime(1753, 1, 1);
            if (toDate.Year < 1753) toDate = new DateTime(1753, 1, 1);

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_], [From Date], [To Date], [Web Site Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Session ID] = @sessionId AND [Item No_] = @itemNo AND [Extra 1] = @extra1 AND [Extra 2] = @extra2 AND [Extra 3] = @extra3 AND [Extra 4] = @extra4 AND [Extra 5] = @extra5 AND [Reference No_] = @referenceNo AND [From Date] = @fromDate AND [To Date] = @toDate AND [Web Site Code] = @webSiteCode ORDER BY [Entry No_]");
            databaseQuery.addStringParameter("sessionId", sessionId, 100);
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("extra1", extra1, 20);
            databaseQuery.addStringParameter("extra2", extra2, 20);
            databaseQuery.addStringParameter("extra3", extra3, 20);
            databaseQuery.addStringParameter("extra4", extra4, 20);
            databaseQuery.addStringParameter("extra5", extra5, 20);
            databaseQuery.addStringParameter("referenceNo", referenceNo, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}


		public DataSet getCartLines(string sessionId, string webUserAccountNo, string webSiteCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_], [From Date], [To Date] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Web User Account No] = @webUserAccountNo AND [Session ID] <> @sessionId AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("sessionId", sessionId, 100);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);
            databaseQuery.addStringParameter("webUserAccountNo", webUserAccountNo, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

        /*
        public DataSet getAuthenticatedCartLines(string webUserAccountNo)
        {
            SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_], [Web Site Code] FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Web User Account No] = '" + webUserAccountNo + "' ORDER BY [Item No_]");
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }
           */

        public DataSet getWebUserAccountCartLines(string webUserAccountNo, string itemNo, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo, DateTime fromDate, DateTime toDate, string webSiteCode)
        {
            if (fromDate.Year < 1753) fromDate = new DateTime(1753, 1, 1);
            if (toDate.Year < 1753) toDate = new DateTime(1753, 1, 1);

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_], [From Date], [To Date], [Web Site Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Web User Account No] = @webUserAccountNo AND [Item No_] = @itemNo AND [Extra 1] = @extra1 AND [Extra 2] = @extra2 AND [Extra 3] = @extra3 AND [Extra 4] = @extra4 AND [Extra 5] = @extra5 AND [Reference No_] = @referenceNo AND [From Date] = @fromDate AND [To Date] = @toDate AND [Web Site Code] = @webSiteCode ORDER BY [Entry No_]");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("extra1", extra1, 20);
            databaseQuery.addStringParameter("extra2", extra2, 20);
            databaseQuery.addStringParameter("extra3", extra3, 20);
            databaseQuery.addStringParameter("extra4", extra4, 20);
            databaseQuery.addStringParameter("extra5", extra5, 20);
            databaseQuery.addStringParameter("referenceNo", referenceNo, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);
            databaseQuery.addStringParameter("webUserAccountNo", webUserAccountNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

      
		public DataSet convertToItemDataSet(DataSet webCartDataSet)
		{
			DataSet itemDataSet = webCartDataSet.Copy();
			itemDataSet.Tables[0].Columns.Remove("Session ID");
            itemDataSet.Tables[0].Columns.Remove("Entry No_");

			return itemDataSet;
		}

	}
}
