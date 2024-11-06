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
            SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [Web Site Code], [Code], [Description], [Payment Method Code], [Type], [Service Parameter] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment Method") + "] WHERE [Web Site Code] = '" + webSiteCode + "'");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);
		}

        public WebPaymentMethodCollection getWebPaymentMethodCollection(string webSiteCode, string languageCode)
        {

            WebPaymentMethodCollection webPaymentMethodCollection = new WebPaymentMethodCollection();

            DataSet dataSet = this.getWebPaymentMethods(webSiteCode);

            int i = 0;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebPaymentMethod webPaymentMethod = new WebPaymentMethod(infojetContext, dataSet.Tables[0].Rows[i]);

                DataSet webPaymentMethodDetailsDataSet = webPaymentMethod.getDetails(languageCode, infojetContext.userSession.customer, infojetContext.currencyCode);

                int j = 0;
                while (j < webPaymentMethodDetailsDataSet.Tables[0].Rows.Count)
                {
                    

                    WebPaymentMethodDetail webPaymentMethodDetail = new WebPaymentMethodDetail(infojetContext.systemDatabase, webPaymentMethodDetailsDataSet.Tables[0].Rows[j]);

                    webPaymentMethod.applyDetails(webPaymentMethodDetail);
                    webPaymentMethodCollection.Add(webPaymentMethod);

                    j++;
                }
                i++;
            }


            return webPaymentMethodCollection;
        }
	}
}
