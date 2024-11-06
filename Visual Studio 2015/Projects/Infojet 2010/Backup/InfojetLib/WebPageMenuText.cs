using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebSite.
    /// </summary>
    public class WebPageMenuText
    {
        public string webSiteCode;
        public string webPageCode;
        public string languageCode;
        public string menuText;
        public string helpText;

        private Infojet infojetContext;

        public WebPageMenuText(Infojet infojetContext, string webSiteCode, string webPageCode, string languageCode)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;
            this.webSiteCode = infojetContext.webSite.code;
            this.webPageCode = webPageCode;
            this.languageCode = languageCode;

            getFromDatabase();
        }


        private void getFromDatabase()
        {

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Page Code], [Language Code], [Menu Text], [Help Text] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Menu Text") + "] WHERE [Web Page Code] = @webPageCode AND [Web Site Code] = @webSiteCode AND [Language Code] = @languageCode");
            databaseQuery.addStringParameter("webPageCode", webPageCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                webSiteCode = dataReader.GetValue(0).ToString();
                webPageCode = dataReader.GetValue(1).ToString();
                languageCode = dataReader.GetValue(2).ToString();
                menuText = dataReader.GetValue(3).ToString();
                helpText = dataReader.GetValue(4).ToString();

            }

            dataReader.Close();

        }


    }
}