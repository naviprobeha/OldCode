using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPageLines.
	/// </summary>
	public class WebSiteLanguages
	{
		private Infojet infojetContext;

		public WebSiteLanguages(Infojet infojetContext)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
		}

		public DataSet getWebSiteLanguages(string webSiteCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Language Code], [Description], [Language Text], [Currency Code], [Rec_ Price Group Code], [Culture Value], [Specific Culture Value], [Market Country Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Site Language") + "] WHERE [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

		public LanguageCollection getPublishedLanguages(string webSiteCode, string currentMarketCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Language Code], [Description], [Language Text], [Currency Code], [Rec_ Price Group Code], [Culture Value], [Specific Culture Value], [Market Country Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Site Language") + "] WHERE [Web Site Code] = @webSiteCode AND [Language Text] <> '' AND [Code] <> @currentMarketCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("currentMarketCode", currentMarketCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

            LanguageCollection languageCollection = new LanguageCollection();

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebSiteLanguage webSiteLanguage = new WebSiteLanguage(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);

                Language language = new Language(webSiteLanguage);
                Link link = infojetContext.webPage.getUrlLink();
                link.setLanguageCulture(language.cultureValue, language.code);

                language.changeUrl = link.toUrl();
                languageCollection.Add(language);

                i++;
            }

            return languageCollection;
		}

        public WebSiteLanguage getHostnameLanguage(string webSiteCode, string hostname)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Language Code], [Description], [Language Text], [Currency Code], [Rec_ Price Group Code], [Culture Value], [Specific Culture Value], [Market Country Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Site Language") + "] WHERE [Web Site Code] = @webSiteCode AND [Host Name] = @hostname");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("hostname", hostname, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            int i = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                return new WebSiteLanguage(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);

            }

            return null;
        }



	}
}
