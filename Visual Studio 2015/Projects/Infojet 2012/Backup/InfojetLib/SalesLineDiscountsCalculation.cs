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
        Hashtable getItemDiscount(DataSet dataSet, Customer customer, string currencyCode);
        Hashtable getItemDiscount(DataSet dataSet, string currencyCode, string customerNo, string customerDiscGroupCode);

        void setCampaignCode(string campaignCode);
    }
}