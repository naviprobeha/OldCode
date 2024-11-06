using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebSite.
    /// </summary>
    public class WebStyle
    {
        public string code;
        public string description;
        public string location;

        private Infojet infojetContext;

        public WebStyle(Infojet infojetContext, string code)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;
            this.code = code;

            getFromDatabase();
        }


        private void getFromDatabase()
        {

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code], [Description], [Location] FROM [" + infojetContext.systemDatabase.getTableName("Web Style") + "] WHERE [Code] = @code");
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                code = dataReader.GetValue(0).ToString();
                description = dataReader.GetValue(1).ToString();
                location = dataReader.GetValue(2).ToString();


            }

            dataReader.Close();

        }

        public string getUrl(WebSite webSite, WebTemplate webTemplate, WebPage webPage)
        {
            return webSite.location + "styles/"+ location + "/" + webTemplate.filename + "?pageCode=" + webPage.code;

        }
    }
}
