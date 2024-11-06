using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ExtendedTextLines.
	/// </summary>
	public class CurrencyExchangeRates
	{
		private Database database;

		public CurrencyExchangeRates(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public CurrencyExchangeRate getCurrentExchangeRate(string currencyCode)
		{
			CurrencyExchangeRate currencyExchRate = null;

            DatabaseQuery databaseQuery = database.prepare("SELECT TOP 1 [Currency Code], [Starting Date], [Exchange Rate Amount], [Adjustment Exch_ Rate Amount], [Relational Currency Code], [Relational Exch_ Rate Amount], [Fix Exchange Rate Amount], [Relational Adjmt Exch Rate Amt] FROM ["+database.getTableName("Currency Exchange Rate")+"] WHERE [Currency Code] = @currencyCode ORDER BY [Starting Date] DESC");
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

			if (dataReader.Read())
			{
				currencyExchRate = new CurrencyExchangeRate(database, dataReader);
			}

			dataReader.Close();

			return currencyExchRate;

		}

        public static float convertCurrency(Database database, string currencyCode, float amount)
        {
            CurrencyExchangeRates currencyExchangeRates = new CurrencyExchangeRates(database);
            CurrencyExchangeRate currencyExchangeRate = currencyExchangeRates.getCurrentExchangeRate(currencyCode);

            return amount / (currencyExchangeRate.relationalExchRateAmount / currencyExchangeRate.exchangeRateAmount);
        }

	}
}
