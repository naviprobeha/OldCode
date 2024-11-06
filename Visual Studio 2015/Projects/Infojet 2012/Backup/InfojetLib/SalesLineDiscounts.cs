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
        private string campaignCode = "";

        public SalesLineDiscounts(Database database, Infojet infojetContext)
        {
            this.database = database;
            this.infojetContext = infojetContext;
        }

        public void setCampaignCode(string campaignCode)
        {
            this.campaignCode = campaignCode;
        }
 
        public Hashtable getItemDiscount(DataSet dataSet, Customer customer, string currencyCode)
        {
            SalesLineDiscountsCalculation salesLineDiscounts = getCalculationMethod();
            salesLineDiscounts.setCampaignCode(campaignCode);

            if (customer != null)
            {
                return salesLineDiscounts.getItemDiscount(dataSet, customer, currencyCode);
            }
            else
            {
                return salesLineDiscounts.getItemDiscount(dataSet, null, currencyCode);
            }
        }



        private SalesLineDiscountsCalculation getCalculationMethod()
        {
            if (infojetContext.webSite.priceCalcType == 0)
            {
                return new SalesLineDiscountsStandard(infojetContext);
            }
            else
            {
                return new SalesLineDiscountsUnique(database);
            }

        }
    }
}
