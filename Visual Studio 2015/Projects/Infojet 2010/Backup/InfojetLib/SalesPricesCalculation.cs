using System;
using System.Data;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for SalesPricesCalculation.
	/// </summary>
	public interface SalesPricesCalculation
	{
		SalesPrice getItemPrice(Item item);
        SalesPrice getItemPrice(Item item, UserSession userSession, string currencyCode);
		SalesPrice getItemPrice(Item item, UserSession userSession, string currencyCode, float quantity);
		SalesPrice getItemGroupPrice(Item item, string customerPriceGroupCode, string currencyCode);

        Hashtable getItemPrice(DataSet dataSet, UserSession userSession, string currencyCode);
        Hashtable getItemPrice(DataSet dataSet, UserSession userSession, string currencyCode, float quantity);
        Hashtable getItemGroupPrice(DataSet dataSet, string customerPriceGroupCode, string currencyCode);
    }
}
