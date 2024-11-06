using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebPageLines.
    /// </summary>
    public class WebShipmentMethods
    {
        private Infojet infojetContext;

        public WebShipmentMethods(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;
        }

        public DataSet getWebShipmentMethods(string webSiteCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Description], [Shipment Method Code], [Shipping Agent Code], [Shipping Agent Service Code], [Level Type], [Active], [Specify Checkouts] FROM [" + infojetContext.systemDatabase.getTableName("Web Shipment Method") + "] WHERE [Web Site Code] = @webSiteCode AND [Active] = 1");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);
        }

        public WebShipmentMethodCollection getWebShipmentMethodCollection(string webSiteCode, string countryCode, string postCode, float totalQuantity, float totalAmount, float totalWeight, float totalVolume, string languageCode)
        {
            return getWebShipmentMethodCollection(webSiteCode, countryCode, postCode, totalQuantity, totalAmount, totalWeight, totalVolume, languageCode, null);
        }

        public WebShipmentMethodCollection getWebShipmentMethodCollection(string webSiteCode, string countryCode, string postCode, float totalQuantity, float totalAmount, float totalWeight, float totalVolume, string languageCode, WebCheckout webCheckout)
        {
            postCode = postCode.Replace(" ", "");

            WebShipmentMethodCollection webShipmentMethodCollection = new WebShipmentMethodCollection();

            DataSet dataSet = this.getWebShipmentMethods(webSiteCode);

            int i = 0;
            bool oneSelected = false;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebShipmentMethod webShipmentMethod = new WebShipmentMethod(infojetContext, dataSet.Tables[0].Rows[i]);

                bool connectionExists = true;
                if (webShipmentMethod.specifyCheckouts)
                {
                    if (!webShipmentMethod.checkShipmentMethodConnection(webCheckout)) connectionExists = false;
                }

                if (connectionExists)
                {

                    DataSet webShipmentMethodDetailsDataSet = webShipmentMethod.getDetails(languageCode, countryCode, infojetContext.currencyCode, postCode);
                    if (webShipmentMethodDetailsDataSet.Tables[0].Rows.Count == 0)
                    {
                        webShipmentMethodDetailsDataSet = webShipmentMethod.getDetails(languageCode, infojetContext.currencyCode);
                    }

                    string freightContent = "";

                    int j = 0;
                    while (j < webShipmentMethodDetailsDataSet.Tables[0].Rows.Count)
                    {

                        WebShipmentMethodDetail webShipmentMethodDetail = new WebShipmentMethodDetail(infojetContext.systemDatabase, webShipmentMethodDetailsDataSet.Tables[0].Rows[j]);


                        bool include = false;
                        if ((webShipmentMethod.levelType == 0) && (webShipmentMethodDetail.from <= totalQuantity) && (webShipmentMethodDetail.to >= totalQuantity)) include = true;
                        if ((webShipmentMethod.levelType == 1) && (webShipmentMethodDetail.from <= totalAmount) && (webShipmentMethodDetail.to >= totalAmount)) include = true;
                        if ((webShipmentMethod.levelType == 2) && (webShipmentMethodDetail.from <= totalWeight) && (webShipmentMethodDetail.to >= totalWeight)) include = true;
                        if ((webShipmentMethod.levelType == 3) && (webShipmentMethodDetail.from <= totalVolume) && (webShipmentMethodDetail.to >= totalVolume)) include = true;
                        if ((webShipmentMethodDetail.from == 0) && (webShipmentMethodDetail.to == 0)) include = true;

                        //freightContent = freightContent + "{ Total vol: " + totalVolume + "; Interval: " + webShipmentMethodDetail.from + "-" + webShipmentMethodDetail.to + "; Include: " + include + "; Amount: "+webShipmentMethodDetail.amount+" } ";
                        if (include)
                        {
                            if (webShipmentMethodDetail.from > 0)
                            {
                                webShipmentMethod.defaultValue = true;
                                oneSelected = true;
                            }

                            webShipmentMethod.applyDetails(webShipmentMethodDetail);

                            if (!webShipmentMethodCollection.Contains(webShipmentMethod))
                            {
                                webShipmentMethodCollection.Add(webShipmentMethod);
                            }

                        }
                        j++;
                    }
                    //if (infojetContext.userSession.webUserAccount.email.Contains("hakan")) throw new Exception("Freightcontent: "+freightContent);

                }
                i++;
            }

            if (!oneSelected)
            {
                if (webShipmentMethodCollection.Count > 0)
                {
                    webShipmentMethodCollection[0].defaultValue = true;
                }
            }

            return webShipmentMethodCollection;
        }
    }
}
