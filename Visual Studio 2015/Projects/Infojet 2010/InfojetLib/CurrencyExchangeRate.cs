using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for CurrencyExchangeRate.
	/// </summary>
	public class CurrencyExchangeRate
	{
		private Database database;

		public string currencyCode;
		public DateTime startingDate;
		public float exchangeRateAmount;
		public float adjustmentExchRateAmount;
		public string relationalCurrencyCode;
		public float relationalExchRateAmount;
		public float fixExchangeRateAmount;
		public float relationalAdjExchRateAmount;


		public CurrencyExchangeRate(Database database, string currencyCode, DateTime startingDate)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.currencyCode = currencyCode;
			this.startingDate = startingDate;

			getFromDatabase();
		}

		public CurrencyExchangeRate(Database database, SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;

			fromReader(dataReader);	 
		}


		private void fromReader(SqlDataReader dataReader)
		{
			currencyCode = dataReader.GetValue(0).ToString();
			startingDate = dataReader.GetDateTime(1);
			exchangeRateAmount = float.Parse(dataReader.GetValue(2).ToString());
			adjustmentExchRateAmount = float.Parse(dataReader.GetValue(3).ToString());
			relationalCurrencyCode = dataReader.GetValue(4).ToString();
			relationalExchRateAmount = float.Parse(dataReader.GetValue(5).ToString());
			fixExchangeRateAmount = float.Parse(dataReader.GetValue(6).ToString());
			relationalAdjExchRateAmount = float.Parse(dataReader.GetValue(7).ToString());

		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Currency Code], [Starting Date], [Exchange Rate Amount], [Adjustment Exch_ Rate Amount], [Relational Currency Code], [Relational Exch_ Rate Amount], [Fix Exchange Rate Amount], [Relational Adjmt Exch Rate Amt] FROM [" + database.getTableName("Currency Exchange Rate") + "] WHERE [Currency Code] = @currencyCode AND [Starting Date] = @startingDate");
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
            databaseQuery.addDateTimeParameter("startingDate", startingDate);

            SqlDataReader dataReader = databaseQuery.executeQuery();

    		if (dataReader.Read())
			{
				fromReader(dataReader);
			}

			dataReader.Close();


		}


	}
}
