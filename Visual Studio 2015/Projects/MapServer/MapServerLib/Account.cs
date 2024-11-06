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
    public class Account
    {

        public string accountNo;
        public string password;
        public string vehicleWebService;
        public string order1WebService;
        public string order2WebService;
        public string customerWebService;
        public string trackingWebService;


        private Database database;

        public Account(Database database, string accountNo)
        {
            //
            // TODO: Add constructor logic here
            //
            this.accountNo = accountNo;
            this.database = database;

            read();
        }

        public Account(Database database, DataRow dataRow)
        {
            this.database = database;

            this.accountNo = dataRow.ItemArray.GetValue(0).ToString();
            this.password = dataRow.ItemArray.GetValue(1).ToString();
            this.vehicleWebService = dataRow.ItemArray.GetValue(2).ToString();
            this.order1WebService = dataRow.ItemArray.GetValue(3).ToString();
            this.order2WebService = dataRow.ItemArray.GetValue(4).ToString();
            this.customerWebService = dataRow.ItemArray.GetValue(5).ToString();
            this.trackingWebService = dataRow.ItemArray.GetValue(6).ToString();
        }

        private bool read()
        {

            SqlDataReader dataReader = database.query("SELECT [Account No], [Password], [Vehicle Web Service], [Order 1 Web Service], [Order 2 Web Service], [Customer Web Service], [Tracking Web Service] FROM [Account] WHERE [Account No] = '" + this.accountNo + "'");

            if (dataReader.Read())
            {
                this.accountNo = dataReader.GetValue(0).ToString();
                this.password = dataReader.GetValue(1).ToString();
                this.vehicleWebService = dataReader.GetValue(2).ToString();
                this.order1WebService = dataReader.GetValue(3).ToString();
                this.order2WebService = dataReader.GetValue(4).ToString();
                this.customerWebService = dataReader.GetValue(5).ToString();
                this.trackingWebService = dataReader.GetValue(6).ToString();

                dataReader.Close();
                return true;
            }
            dataReader.Close();
            return false;


        }

 

    }
}