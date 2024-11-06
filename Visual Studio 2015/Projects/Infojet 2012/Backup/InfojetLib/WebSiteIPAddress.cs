using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebSite.
    /// </summary>
    public class WebSiteIPAddress
    {

        public static bool checkAccess(Infojet infojetContext, string webSiteCode, string ipAddress)
        {
            bool allowed = false;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [IP Address] FROM [" + infojetContext.systemDatabase.getTableName("Web Site IP Address") + "] WHERE [Web Site Code] = @webSiteCode AND [IP Address] = @ipAddress");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("ipAddress", ipAddress, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read()) allowed = true;
            dataReader.Close();

            return allowed;
        }

    }
}
