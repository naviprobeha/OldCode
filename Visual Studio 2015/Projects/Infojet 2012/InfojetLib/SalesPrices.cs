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
        private string campaignCode = "";

        public SalesPrices(Database database, Infojet infojetContext)
        {
            this.database = database;
            this.infojetContext = infojetContext;
        }

        public void setCampaignCode(string campaignCode)
        {
            this.campaignCode = campaignCode;
        }

        public Hashtable getItemPrice(DataSet dataSet, string customerPriceGroupCode, string currencyCode)
        {
            SalesPricesCalculation salesPrices = getCalculationMethod();
            salesPrices.setCampaignCode(campaignCode);
            return salesPrices.getItemPrice(dataSet, customerPriceGroupCode, currencyCode);

        }

        public Hashtable getItemPrice(DataSet dataSet, Customer customer, string currencyCode)
        {
            SalesPricesCalculation salesPrices = getCalculationMethod();
            salesPrices.setCampaignCode(campaignCode);
            return salesPrices.getItemPrice(dataSet, customer, currencyCode);

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