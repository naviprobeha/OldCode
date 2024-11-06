using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Navipro.Base.Common;
using System.Collections;

/// <summary>
/// Summary description for Map
/// </summary>
/// 
namespace Navipro.MapServer.Lib
{
    public class Ticket
    {

        public string sessionNo;
        public string accountNo;
        public DateTime expireDateTime;
        public string parameter1;
        public string parameter2;


        private Database database;

        public Ticket(Database database, string sessionNo, string accountNo)
        {
            //
            // TODO: Add constructor logic here
            //
            this.sessionNo = sessionNo;
            this.accountNo = accountNo;
            this.database = database;
            this.expireDateTime = new DateTime(1753, 1, 1);

            read();

        }


        private bool read()
        {

            SqlDataReader dataReader = database.query("SELECT [Session No], [Account No], [Expire Date Time], [Parameter 1], [Parameter 2] FROM [Ticket] WHERE [Session No] = '"+sessionNo+"' AND [Account No] = '" + this.accountNo + "'");

            if (dataReader.Read())
            {
                this.sessionNo = dataReader.GetValue(0).ToString();
                this.accountNo = dataReader.GetValue(1).ToString();
                this.expireDateTime = dataReader.GetDateTime(2);
                this.parameter1 = dataReader.GetValue(3).ToString();
                this.parameter2 = dataReader.GetValue(4).ToString();

                dataReader.Close();
                return true;
            }
            dataReader.Close();
            return false;


        }

        public void commit()
        {
            SqlDataReader dataReader = database.query("SELECT [Session No] FROM [Ticket] WHERE [Session No] = '" + this.sessionNo + "' AND [Account No] = '"+accountNo+"'");
            if (dataReader.Read())
            {
                dataReader.Close();

                database.nonQuery("UPDATE [Ticket] SET [Expire Date Time] = '" + this.expireDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "', [Parameter 1] = '" + this.parameter1 + "', [Parameter 2] = '" + this.parameter2 + "' WHERE [Session No] = '" + this.sessionNo + "' AND [Account No] = '"+accountNo+"'");
            }
            else
            {
                dataReader.Close();

                database.nonQuery("INSERT INTO [Ticket] ([Session No], [Account No], [Expire Date Time], [Parameter 1], [Parameter 2]) VALUES ('" + this.sessionNo + "', '" + this.accountNo + "', '" + this.expireDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + parameter1 + "', '" + parameter2 + "')");

            }


        }


    }
}