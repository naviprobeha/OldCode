using System;
using System.Data;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for SalesLineDiscounts.
	/// </summary>
	public class SalesLineDiscounts
	{
		private Database database;
		private Infojet infojetContext;

		public SalesLineDiscounts(Database database, Infojet infojetContext)
		{
			this.database = database;
			this.infojetContext = infojetContext;
		}

		public SalesLineDiscount getItemDiscount(Item item, UserSession userSession, string currencyCode)
		{
			SalesLineDiscountsCalculation salesLineDiscounts = getCalculationMethod();

			return salesLineDiscounts.getItemDiscount(item, userSession, currencyCode);
		}

		public SalesLineDiscount getItemDiscount(Item item, UserSession userSession, string currencyCode, float quantity)
		{
			SalesLineDiscountsCalculation salesLineDiscounts = getCalculationMethod();

			return salesLineDiscounts.getItemDiscount(item, userSession, currencyCode, quantity);

		}

		public Hashtable getItemDiscount(DataSet dataSet, UserSession userSession, string currencyCode)
		{
			SalesLineDiscountsCalculation salesLineDiscounts = getCalculationMethod();

			return salesLineDiscounts.getItemDiscount(dataSet, userSession, currencyCode);
		}

		public Hashtable getItemDiscount(DataSet dataSet, UserSession userSession, string currencyCode, float quantity)
		{
			SalesLineDiscountsCalculation salesLineDiscounts = getCalculationMethod();

			return salesLineDiscounts.getItemDiscount(dataSet, userSession, currencyCode, quantity);

		}

		private SalesLineDiscountsCalculation getCalculationMethod()
		{
			if (infojetContext.webSite.priceCalcType == 0)
			{
				return new SalesLineDiscountsStandard(database);
			}
			else
			{
				return new SalesLineDiscountsUnique(database);
			}

		}
	}
}
