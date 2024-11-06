using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    public class NewsletterAddress
    {

        public NewsletterAddress()
        {

        }

        public static bool addressExists(Infojet infojetContext, string emailAddress)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [E-mail] FROM [" + infojetContext.systemDatabase.getTableName("Newsletter Address") + "] WHERE [E-mail] = @emailAddress AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);
            databaseQuery.addStringParameter("emailAddress", emailAddress, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            
            bool found = false;
            if (dataReader.Read())
            {
                found = true;
            }
            dataReader.Close();

            return found;
        }

        public static void addAddress(Infojet infojetContext, string emailAddress)
        {
            if (!addressExists(infojetContext, emailAddress))
            {
                UserProfile userProfile = new UserProfile();
                userProfile.email = emailAddress;
                
                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createNewsletterAddress", userProfile));
                appServerConnection.processRequest();
            }

        }

        public static void removeAddress(Infojet infojetContext, string emailAddress)
        {
            if (addressExists(infojetContext, emailAddress))
            {
                UserProfile userProfile = new UserProfile();
                userProfile.email = emailAddress;

                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "removeNewsletterAddress", userProfile));
                appServerConnection.processRequest();
            }

        }

    }
}
