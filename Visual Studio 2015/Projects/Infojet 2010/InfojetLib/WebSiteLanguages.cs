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
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Language Code], [Description], [Language Text], [Currency Code], [Rec_ Price Group Code], [Culture Value], [Specific Culture Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Site Language") + "] WHERE [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

		public LanguageCollection getPublishedLanguages(string webSiteCode, string currentLanguageCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Language Code], [Description], [Language Text], [Currency Code], [Rec_ Price Group Code], [Culture Value], [Specific Culture Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Site Language") + "] WHERE [Web Site Code] = @webSiteCode AND [Language Text] <> '' AND [Code] <> @currentLanguageCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("currentLanguageCode", currentLanguageCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

            LanguageCollection languageCollection = new LanguageCollection();

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebSiteLanguage webSiteLanguage = new WebSiteLanguage(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);

                Language language = new Language(webSiteLanguage);
                language.changeUrl = infojetContext.webPage.getUrl() + "&languageCode=" + language.languageCode;
                languageCollection.Add(language);

                i++;
            }

            return languageCollection;
		}

	}
}
