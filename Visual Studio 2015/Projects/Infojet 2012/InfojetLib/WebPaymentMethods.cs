using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPageLines.
	/// </summary>
	public class WebPaymentMethods
	{
        private Infojet infojetContext;

		public WebPaymentMethods(Infojet infojetContext)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
		}

		public DataSet getWebPaymentMethods(string webSiteCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Description], [Payment Method Code], [Type], [Service Parameter], [Service Code], [Require Freight Fee], [Check Credit Limit], [Check Due Invoices], [Check All Invoices], [Check Orders], [Check Cart], [Specify Checkouts] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment Method") + "] WHERE [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);
		}

        public WebPaymentMethodCollection getWebPaymentMethodCollection(string webSiteCode, string languageCode, WebCheckout webCheckout)
        {

            WebPaymentMethodCollection webPaymentMethodCollection = new WebPaymentMethodCollection();

            DataSet dataSet = this.getWebPaymentMethods(webSiteCode);

            int i = 0;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebPaymentMethod webPaymentMethod = new WebPaymentMethod(infojetContext, dataSet.Tables[0].Rows[i]);
                
                bool creditCheckOk = true;
                if (webPaymentMethod.checkCreditLimit)
                {
                    creditCheckOk = false;
                    if (infojetContext.userSession != null)
                    {
                        creditCheckOk = webPaymentMethod.checkCredit(webCheckout);

                    }
                }

                bool connectionExists = true;
                if (webPaymentMethod.specifyCheckouts)
                {
                    if (!webPaymentMethod.checkPaymentMethodConnection(webCheckout)) connectionExists = false;
                }

                if ((creditCheckOk) && (connectionExists) && ((!webPaymentMethod.requireFreightFee) || ((webPaymentMethod.requireFreightFee) && (webCheckout.webCartHeader.freightFee > 0))))
                {
                    DataSet webPaymentMethodDetailsDataSet = null;

                    webPaymentMethodDetailsDataSet = webPaymentMethod.getDetails(languageCode, webCheckout.webCartHeader, infojetContext.currencyCode);

                    int j = 0;
                    while (j < webPaymentMethodDetailsDataSet.Tables[0].Rows.Count)
                    {


                        WebPaymentMethodDetail webPaymentMethodDetail = new WebPaymentMethodDetail(infojetContext.systemDatabase, webPaymentMethodDetailsDataSet.Tables[0].Rows[j]);

                        webPaymentMethod.applyDetails(webPaymentMethodDetail);
                        webPaymentMethodCollection.Add(webPaymentMethod);

                        j++;
                    }
                }
                i++;
            }


            return webPaymentMethodCollection;
        }
	}
}
