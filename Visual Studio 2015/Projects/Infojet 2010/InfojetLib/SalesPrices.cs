using System;
using System.Data;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for SalesPricesStandard.
	/// </summary>
	public class SalesPrices
	{

		private Database database;
		private Infojet infojetContext;

		public SalesPrices(Database database, Infojet infojetContext)
		{
			this.database = database;
			this.infojetContext = infojetContext;
		}

		public SalesPrice getItemPrice(Item item)
		{
			SalesPricesCalculation salesPrices = getCalculationMethod();

			return salesPrices.getItemPrice(item);
		}

		public SalesPrice getItemPrice(Item item, UserSession userSession, string currencyCode)
		{
			SalesPricesCalculation salesPrices = getCalculationMethod();

			return salesPrices.getItemPrice(item, userSession, currencyCode);
		}

 
		public SalesPrice getItemPrice(Item item, UserSession userSession, string currencyCode, float quantity)
		{
			SalesPricesCalculation salesPrices = getCalculationMethod();

			return salesPrices.getItemPrice(item, userSession, currencyCode, quantity);

		}

		public SalesPrice getItemGroupPrice(Item item, string customerPriceGroupCode, string currencyCode)
		{
			SalesPricesCalculation salesPrices = getCalculationMethod();

			return salesPrices.getItemGroupPrice(item, customerPriceGroupCode, currencyCode);
		}

        public Hashtable getItemGroupPrice(DataSet dataSet, string customerPriceGroupCode, string currencyCode)
        {
            SalesPricesCalculation salesPrices = getCalculationMethod();

            return salesPrices.getItemGroupPrice(dataSet, customerPriceGroupCode, currencyCode);

        }

        public Hashtable getItemPrice(DataSet dataSet, UserSession userSession, string currencyCode, float quantity)
        {
            SalesPricesCalculation salesPrices = getCalculationMethod();

            return salesPrices.getItemPrice(dataSet, userSession, currencyCode, quantity);

        }

        public Hashtable getItemPrice(DataSet dataSet, UserSession userSession, string currencyCode)
        {
            SalesPricesCalculation salesPrices = getCalculationMethod();

            return salesPrices.getItemPrice(dataSet, userSession, currencyCode);

        }
		private SalesPricesCalculation getCalculationMethod()
		{
			if (infojetContext.webSite.priceCalcType == 0)
			{
				return new SalesPricesStandard(infojetContext);
			}
			else
			{
				return new SalesPricesUnique(infojetContext);
			}

		}
	}
}