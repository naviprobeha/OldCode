using System;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for Item.
    /// </summary>
    public class WebModelTranslation : ProductTranslation
    {
        private Infojet infojetContext;

        private string _webModelNo;
        private string _languageCode;
        private string _description;
        private string _description2;

        public WebModelTranslation(Infojet infojetContext, string webModelNo, string languageCode)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this._webModelNo = webModelNo;
            this._languageCode = languageCode;

            getFromDatabase();
        }

 
        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Model No_], [Language Code], [Description], [Description 2] FROM [" + infojetContext.systemDatabase.getTableName("Web Model Translation") + "] WHERE [Web Model No_] = @webModelNo AND [Language Code] = @languageCode");
            databaseQuery.addStringParameter("webModelNo", webModelNo, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                _webModelNo = dataReader.GetValue(0).ToString();
                _languageCode = dataReader.GetValue(1).ToString();
                _description = dataReader.GetValue(2).ToString();
                _description2 = dataReader.GetValue(3).ToString();

            }

            dataReader.Close();


        }

        public string webModelNo { get { return _webModelNo; } }
        public string languageCode { get { return _languageCode; } }
        public string description { get { return _description; } set { _description = value; } }
        public string description2 { get { return _description2; } set { _description2 = value; } }

    }
}
