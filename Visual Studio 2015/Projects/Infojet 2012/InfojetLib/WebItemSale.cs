using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for PostCode.
    /// </summary>
    public class WebItemSale
    {
        private string _webSiteCode;
        private string _itemNo;
        private string _linkedItemNo;
        private int _noOfOrders;
        private string _description;
        private WebImage _webImage;
        private string _url;
    
        private Infojet infojetContext;

        public WebItemSale(Infojet infojetContext, string webSiteCode, string itemNo, string linkedItemNo)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this._webSiteCode = webSiteCode;
            this._itemNo = itemNo;
            this._linkedItemNo = linkedItemNo;

        }

        public WebItemSale(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this._webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
            this._itemNo = dataRow.ItemArray.GetValue(1).ToString();
            this._linkedItemNo = dataRow.ItemArray.GetValue(2).ToString();
            this._noOfOrders = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this.webImage = new WebImage(infojetContext, "");
        }

        public string webSiteCode { get { return _webSiteCode; } set { _webSiteCode = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string linkedItemNo { get { return _linkedItemNo; } set { _linkedItemNo = value; } }
        public int noOfOrders { get { return _noOfOrders; } set { _noOfOrders = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public WebImage webImage { get { return _webImage; } set { _webImage = value; } }
        public string url { get { return _url; } set { _url = value; } }

        public static WebItemSaleCollection getWebItemSaleCollection(Infojet infojetContext, string itemNo)
        {
            WebItemSaleCollection webItemSaleCollection = new WebItemSaleCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT TOP 5 s.[Web Site Code], s.[Item No_], s.[Linked Item No_], s.[No_ Of Orders], i.[Description], t.[Description], (SELECT TOP 1 i.[Web Image Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Image") + "] i WHERE i.[Item No_] = s.[Linked Item No_] AND i.[Type] = 0 AND i.[Web Site Code] = '" + infojetContext.webSite.code + "') as imageCode, v.[Web Model No_], m.[Description], (SELECT TOP 1 i.[Web Image Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Image") + "] i, [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] mv WHERE i.[Item No_] = mv.[Item No_] AND i.[Type] = 0 AND i.[Web Site Code] = '" + infojetContext.webSite.code + "' AND mv.[Web Model No_] = v.[Web Model No_] AND mv.[Primary] = 1) as modelImageCode FROM [" + infojetContext.systemDatabase.getTableName("Web Item Sale") + "] s, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] t ON i.[No_] = t.[Item No_] AND t.[Language Code] = @languageCode LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] v ON v.[Item No_] = i.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model") + "] m ON m.[No_] = v.[Web Model No_] WHERE s.[Item No_] = @itemNo AND s.[Linked Item No_] = i.[No_] AND s.[Linked Item No_] <> '' ORDER BY s.[No_ Of Orders] DESC");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebItemSale webItemSale = new WebItemSale(infojetContext, dataSet.Tables[0].Rows[i]);
                webItemSale.description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString();

                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() != "")
                {
                    webItemSale.description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString();
                }
                
                //if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString() != "")
                //{
                //    webItemSale.description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(8).ToString();
                //}
                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString() != "")
                {
                    webItemSale.webImage = new WebImage(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(9).ToString());
                }
                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() != "")
                {
                    webItemSale.webImage = new WebImage(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString());
                }

                WebModelVariants webModelVariants = new WebModelVariants(infojetContext);

                Link link = infojetContext.webPage.getUrlLink();
                link.setItem(dataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString(), webItemSale.linkedItemNo, webItemSale.description);               
                webItemSale.url = link.toUrl();

                webItemSaleCollection.Add(webItemSale);

                i++;
            }


            return webItemSaleCollection;
        }
    }
}
