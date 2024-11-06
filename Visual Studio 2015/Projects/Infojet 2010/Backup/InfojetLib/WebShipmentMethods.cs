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
            SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [Web Site Code], [Code], [Description], [Shipment Method Code], [Shipping Agent Code], [Shipping Agent Service Code], [Level Type], [Active] FROM [" + infojetContext.systemDatabase.getTableName("Web Shipment Method") + "] WHERE [Web Site Code] = '" + webSiteCode + "'  AND [Active] = 1");
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);
        }


        public WebShipmentMethodCollection getWebShipmentMethodCollection(string webSiteCode, float totalQuantity, float totalAmount, float totalWeight, string languageCode)
        {

            WebShipmentMethodCollection webShipmentMethodCollection = new WebShipmentMethodCollection();

            DataSet dataSet = this.getWebShipmentMethods(webSiteCode);

            int i = 0;
            bool oneSelected = false;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebShipmentMethod webShipmentMethod = new WebShipmentMethod(infojetContext, dataSet.Tables[0].Rows[i]);

                DataSet webShipmentMethodDetailsDataSet = webShipmentMethod.getDetails(languageCode, infojetContext.currencyCode);

                int j = 0;
                while (j < webShipmentMethodDetailsDataSet.Tables[0].Rows.Count)
                {
                    
                    WebShipmentMethodDetail webShipmentMethodDetail = new WebShipmentMethodDetail(infojetContext.systemDatabase, webShipmentMethodDetailsDataSet.Tables[0].Rows[j]);

                    bool include = false;
                    if ((webShipmentMethod.levelType == 0) && (webShipmentMethodDetail.from <= totalQuantity) && (webShipmentMethodDetail.to >= totalQuantity)) include = true;
                    if ((webShipmentMethod.levelType == 1) && (webShipmentMethodDetail.from <= totalAmount) && (webShipmentMethodDetail.to >= totalAmount)) include = true;
                    if ((webShipmentMethod.levelType == 2) && (webShipmentMethodDetail.from <= totalWeight) && (webShipmentMethodDetail.to >= totalWeight)) include = true;
                    if ((webShipmentMethodDetail.from == 0) && (webShipmentMethodDetail.to == 0)) include = true;

                    if (include)
                    {
                        if (webShipmentMethodDetail.from > 0)
                        {
                            webShipmentMethod.defaultValue = true;
                            oneSelected = true;
                        }

                        webShipmentMethod.applyDetails(webShipmentMethodDetail);
                        webShipmentMethodCollection.Add(webShipmentMethod);

                    }
                    j++;
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
