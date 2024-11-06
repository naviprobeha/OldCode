using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for PostCode.
    /// </summary>
    public class WebItemDocument
    {
        private string _itemNo;
        private int _entryNo;
        private int _type;
        private string _fileName;
        private string _description;
        
        private Infojet infojetContext;

        public WebItemDocument(Infojet infojetContext, string itemNo, int entryNo)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this._itemNo = itemNo;
            this._entryNo = entryNo;

        }

        public WebItemDocument(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this._itemNo = dataRow.ItemArray.GetValue(0).ToString();
            this._entryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            this._type = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
            this._fileName = dataRow.ItemArray.GetValue(3).ToString();
            this._description = dataRow.ItemArray.GetValue(4).ToString();
        }

        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public int type { get { return _type; } set { _type = value; } }
        public string fileName { get { return _fileName; } set { _fileName = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string url { get { return infojetContext.webPage.getUrl() + "&systemCommand=downloadDocument&fileName=" + System.Web.HttpUtility.UrlEncode(fileName); } }
        

        public static WebItemDocumentCollection getWebItemDocumentCollection(Infojet infojetContext, string itemNo)
        {
            WebItemDocumentCollection webItemDocumentCollection = new WebItemDocumentCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT d.[Item No_], d.[Entry No_], f.[Type], d.[Filename], t.[Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Document") + "] d, [" + infojetContext.systemDatabase.getTableName("Web Item Document Translation") + "] t, [" + infojetContext.systemDatabase.getTableName("Web Document") + "] f WHERE d.[Item No_] = @itemNo AND t.[Item No_] = d.[Item No_] AND t.[Document Entry No_] = d.[Entry No_] AND t.[Language Code] = @languageCode AND d.[Filename] = f.[Filename]");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebItemDocument webItemDocument = new WebItemDocument(infojetContext, dataSet.Tables[0].Rows[i]);

                webItemDocumentCollection.Add(webItemDocument);

                i++;
            }


            return webItemDocumentCollection;
        }

    }
}
