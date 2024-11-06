using System;
using System.Data;
using System.Data.SqlClient;
using Navipro.Base.Common;

/// <summary>
/// Summary description for Maps
/// </summary>
/// 
namespace Navipro.MapServer.Lib
{

    public class Accounts
    {
        private Database database;

        public Accounts(Database database)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = database;
        }

        public Account selectAccount(string accountNo, string password)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Account No], [Password], [Vehicle Web Service], [Order 1 Web Service], [Order 2 Web Service], [Customer Web Service], [Tracking Web Service] FROM [Account] WHERE [Account No] = '"+accountNo+"' AND [Password] = '"+password+"'");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "account");
            adapter.Dispose();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                Account account = new Account(database, dataSet.Tables[0].Rows[0]);
                return account;
            }

            return null;
        }

        public Account getAccount(string accountNo)
        {
            SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Account No], [Password], [Vehicle Web Service], [Order 1 Web Service], [Order 2 Web Service], [Customer Web Service], [Tracking Web Service] FROM [Account] WHERE [Account No] = '" + accountNo + "'");

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "account");
            adapter.Dispose();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                Account account = new Account(database, dataSet.Tables[0].Rows[0]);
                return account;
            }

            return null;
        }

    }
}