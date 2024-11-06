using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for PostCode.
    /// </summary>
    public class WebItemRelation
    {
        private string _itemNo;
        private string _relatedItemNo;
        private float _relationQuantity;
        private string _description;
        private string _url;
        private WebImage _webImage;

        private Infojet infojetContext;

        public WebItemRelation(Infojet infojetContext, string itemNo, string relatedItemNo)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this._itemNo = itemNo;
            this._relatedItemNo = relatedItemNo;
            this.webImage = new WebImage(infojetContext, "");

        }

        public WebItemRelation(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this._itemNo = dataRow.ItemArray.GetValue(0).ToString();
            this._relatedItemNo = dataRow.ItemArray.GetValue(1).ToString();
            this._relationQuantity = float.Parse(dataRow.ItemArray.GetValue(2).ToString());
            this.webImage = new WebImage(infojetContext, "");

        }

        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string relatedItemNo { get { return _relatedItemNo; } set { _relatedItemNo = value; } }
        public float relationQuantity { get { return _relationQuantity; } set { _relationQuantity = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string url { get { return _url; } set { _url = value; } }
        public WebImage webImage { get { return _webImage; } set { _webImage = value; } }

        public static WebItemRelationCollection getWebItemRelationCollection(Infojet infojetContext, string itemNo)
        {
            WebItemRelationCollection webItemRelationCollection = new WebItemRelationCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT s.[Item No_], s.[Related Item No_], s.[Related Quantity], i.[Description], t.[Description], (SELECT TOP 1 i.[Web Image Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Image") + "] i WHERE i.[Item No_] = s.[Related Item No_] AND i.[Type] = 0 AND i.[Web Site Code] = '" + infojetContext.webSite.code + "') as imageCode, v.[Web Model No_], m.[Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Relation") + "] s, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] t ON i.[No_] = t.[Item No_] AND t.[Language Code] = @languageCode LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] v ON v.[Item No_] = i.[No_] AND v.[Primary] = 1 LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model") + "] m ON m.[No_] = v.[Web Model No_] WHERE s.[Item No_] = @itemNo AND s.[Related Item No_] = i.[No_]");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebItemRelation itemRelation = new WebItemRelation(infojetContext, dataSet.Tables[0].Rows[i]);
                itemRelation.description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();

                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString() != "")
                {
                    itemRelation.description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString();
                }
                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString() != "")
                {
                    itemRelation.description = dataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString();
                }
                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString() != "")
                {
                    itemRelation.webImage = new WebImage(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString());
                }
                Link link = infojetContext.webPage.getUrlLink();
                link.setItem("", itemRelation.relatedItemNo, itemRelation.description);

                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString() != "")
                {
                    link.setItem(dataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString(), itemRelation.relatedItemNo, dataSet.Tables[0].Rows[i].ItemArray.GetValue(7).ToString());
                }

                itemRelation.url = link.toUrl();   

                webItemRelationCollection.Add(itemRelation);

                i++;
            }


            return webItemRelationCollection;
        }
    }
}
