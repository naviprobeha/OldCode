using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPageLines.
	/// </summary>
	public class WebUserAccountRelations
	{
		private Database database;

		public WebUserAccountRelations(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public WebUserAccountRelation getUserAccountRelation(string no, string webSiteCode)
		{
			WebUserAccountRelation webUserAccountRelation = null;

            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Web Site Code], [Customer No_] FROM ["+database.getTableName("Web User Account Relation")+"] WHERE [No_] = @no AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("no", no, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				webUserAccountRelation = new WebUserAccountRelation(database, dataReader);
			}

			dataReader.Close();

			return webUserAccountRelation;
		}

        public WebUserAccountRelation getUserAccountRelationFromWebSiteGroup(string no, string webSiteLocation)
        {
            WebUserAccountRelation webUserAccountRelation = null;

            DatabaseQuery databaseQuery = database.prepare("SELECT r.[No_], r.[Web Site Code], r.[Customer No_] FROM [" + database.getTableName("Web User Account Relation") + "] r, [" + database.getTableName("Web Site") + "] s WHERE r.[No_] = @no AND r.[Web Site Code] = s.[Code] AND s.[Location] = @webSiteLocation");
            databaseQuery.addStringParameter("webSiteLocation", webSiteLocation, 20);
            databaseQuery.addStringParameter("no", no, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                webUserAccountRelation = new WebUserAccountRelation(database, dataReader);
            }

            dataReader.Close();

            return webUserAccountRelation;
        }

        public WebUserAccountRelation findFirstRelation(string userId)
        {
            WebUserAccountRelation webUserAccountRelation = null;

            DatabaseQuery databaseQuery = database.prepare("SELECT r.[No_], r.[Web Site Code], r.[Customer No_] FROM [" + database.getTableName("Web User Account Relation") + "] r, [" + database.getTableName("Web User Account") + "] u WHERE u.[User-ID] = @userId AND u.[No_] = r.[No_] AND u.[Type] = 1");
            databaseQuery.addStringParameter("userId", userId.ToUpper(), 50);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                webUserAccountRelation = new WebUserAccountRelation(database, dataReader);
            }

            dataReader.Close();

            return webUserAccountRelation;
        }

	}
}
