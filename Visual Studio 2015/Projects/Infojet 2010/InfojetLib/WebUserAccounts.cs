using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPageLines.
	/// </summary>
	public class WebUserAccounts
	{
		private Database database;

		public WebUserAccounts(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public WebUserAccount getUserAccount(string userId, string password)
		{
			WebUserAccount webUserAccount = null;


            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [User-ID], [Password], [Closed], [Contact No_], [Customer No_], [Company Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Bill-to Company Name], [Bill-to Address], [Bill-to Address 2], [Bill-to Post Code], [Bill-to City], [Bill-to Country Code], [Registration No_], [E-Mail], [Phone No_], [Last Logged On Date], [Last Logged On Time], [History Date], [History Time], [Max Buy Type], [Max Buy Limit _ Order], [Company Role], [Cell Phone No_], [Name], [Language Code] FROM [" + database.getTableName("Web User Account") + "] WHERE UPPER([User-ID]) = @userId AND [Password] = @password AND [User-ID] <> '' AND [Closed] = 0 AND [Type] = 1");
            databaseQuery.addStringParameter("userId", userId.ToUpper(), 100);
            databaseQuery.addStringParameter("password", password, 30);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                webUserAccount = new WebUserAccount(database, dataReader);
            }

            dataReader.Close();


			return webUserAccount;
		}

		public WebUserAccount getUserAccount(string userId)
		{
			WebUserAccount webUserAccount = null;

            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [User-ID], [Password], [Closed], [Contact No_], [Customer No_], [Company Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Bill-to Company Name], [Bill-to Address], [Bill-to Address 2], [Bill-to Post Code], [Bill-to City], [Bill-to Country Code], [Registration No_], [E-Mail], [Phone No_], [Last Logged On Date], [Last Logged On Time], [History Date], [History Time], [Max Buy Type], [Max Buy Limit _ Order], [Company Role], [Cell Phone No_], [Name], [Language Code] FROM [" + database.getTableName("Web User Account") + "] WHERE UPPER([User-ID]) = @userId AND [Type] = 1");
            databaseQuery.addStringParameter("userId", userId.ToUpper(), 100);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                webUserAccount = new WebUserAccount(database, dataReader);
            }

			dataReader.Close();


			return webUserAccount;
		}

        public WebUserAccount findUserAccount(string email)
        {
            WebUserAccount webUserAccount = null;

            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [User-ID], [Password], [Closed], [Contact No_], [Customer No_], [Company Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Bill-to Company Name], [Bill-to Address], [Bill-to Address 2], [Bill-to Post Code], [Bill-to City], [Bill-to Country Code], [Registration No_], [E-Mail], [Phone No_], [Last Logged On Date], [Last Logged On Time], [History Date], [History Time], [Max Buy Type], [Max Buy Limit _ Order], [Company Role], [Cell Phone No_], [Name], [Language Code] FROM [" + database.getTableName("Web User Account") + "] WHERE UPPER([E-Mail]) = @email AND [Type] = 1");
            databaseQuery.addStringParameter("email", email.ToUpper(), 100);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                webUserAccount = new WebUserAccount(database, dataReader);
            }

            dataReader.Close();


            return webUserAccount;
        }

	}
}
