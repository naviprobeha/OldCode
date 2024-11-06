using System;
using System.Data;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for SalesPricesCalculation.
	/// </summary>
	public interface SalesLineDiscountsCalculation
	{
		SalesLineDiscount getItemDiscount(Item item, UserSession userSession, string currencyCode);
		SalesLineDiscount getItemDiscount(Item item, UserSession userSession, string currencyCode, float quantity);

        Hashtable getItemDiscount(DataSet dataSet, UserSession userSession, string currencyCode);
        Hashtable getItemDiscount(DataSet dataSet, UserSession userSession, string currencyCode, float quantity);

	}
}