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
		private Database database;


		public WebCartLines(Database database)
		{
			//
			// TODO: Add constructor logic here
			//

			this.database = database;

		}

		public DataSet getCartLines(string sessionId)
		{
			SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_] FROM ["+database.getTableName("Web Cart Line")+"] WHERE [Session ID] = '"+sessionId+"' ORDER BY [Item No_]");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

		public DataSet getCartLines(string sessionId, string itemNo, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo)
		{
            SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_] FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Session ID] = '" + sessionId + "' AND [Item No_] = '" + itemNo + "' AND [Extra 1] = '" + extra1 + "' AND [Extra 2] = '" + extra2 + "' AND [Extra 3] = '" + extra3 + "' AND [Extra 4] = '" + extra4 + "' AND [Extra 5] = '" + extra5 + "' AND [Reference No_] = '" + referenceNo + "' ORDER BY [Item No_]");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}


		public DataSet getCartLines(string sessionId, string webUserAccountNo)
		{
            SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_] FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Web User Account No] = '" + webUserAccountNo + "' AND [Session ID] <> '" + sessionId + "'");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

        /*
        public DataSet getAuthenticatedCartLines(string webUserAccountNo)
        {
            SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_] FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Web User Account No] = '" + webUserAccountNo + "' ORDER BY [Item No_]");
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }
           */

        public DataSet getWebUserAccountCartLines(string webUserAccountNo, string itemNo, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo)
        {
            SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Session ID], [Entry No_], [Item No_], [Unit Of Measure Code], [Unit Price], [Quantity], [Amount], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No], [Reference No_] FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Web User Account No] = '" + webUserAccountNo + "' AND [Item No_] = '" + itemNo + "' AND [Extra 1] = '" + extra1 + "' AND [Extra 2] = '" + extra2 + "' AND [Extra 3] = '" + extra3 + "' AND [Extra 4] = '" + extra4 + "' AND [Extra 5] = '" + extra5 + "' AND [Reference No_] = '" + referenceNo + "' ORDER BY [Item No_]");
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
