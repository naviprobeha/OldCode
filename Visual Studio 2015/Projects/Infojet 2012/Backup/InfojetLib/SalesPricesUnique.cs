using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for SalesPricesUnique.
    /// </summary>
    public class SalesPricesUnique : SalesPricesCalculation
    {
        private Database database;
        private Infojet infojetContext;
        private string campaignCode;

        public SalesPricesUnique(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = infojetContext.systemDatabase;
            this.infojetContext = infojetContext;
        }

        public void setCampaignCode(string campaignCode)
        {
            this.campaignCode = campaignCode;
        }

 
        public Hashtable getItemPrice(DataSet dataSet, string currencyCode, string customerNo, string customerPriceGroupCode)
        {
            return null;
        }

        public Hashtable getItemPrice(DataSet dataSet, string currencyCode, string customerPriceGroupCode)
        {
            return getItemPrice(dataSet, currencyCode, "", customerPriceGroupCode);
        }


 
        public Hashtable getItemPrice(DataSet dataSet, Customer customer, string currencyCode)
        {
            if (customer != null)
            {
                return getItemPrice(dataSet, currencyCode, customer.no, customer.customerPriceGroup);
            }
            else
            {
                return getItemPrice(dataSet, "", "", "");
            }

        }
    }
}
