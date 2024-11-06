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
        Hashtable getItemPrice(DataSet dataSet, Customer customer, string currencyCode);
        Hashtable getItemPrice(DataSet dataSet, string customerPriceGroupCode, string currencyCode);

        void setCampaignCode(string campaignCode);
    }
}
