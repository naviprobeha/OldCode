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

            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Web Site Code] FROM [" + database.getTableName("Web User Account Relation") + "] WHERE [No_] = @no AND [Web Site Code] = @webSiteCode");
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

	}
}
