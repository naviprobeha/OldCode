using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Cashjet.Library
{
    /// <summary>
    /// Summary description for PostCode.
    /// </summary>
    public class CashSite
    {
        private string _code;
        private string _name;
        private string _address;
        private string _city;
        private string _locationCode;

        private Database database;

        public CashSite(Database database, string code)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = database;

            this._code = code;
        }

        public CashSite(Database database, DataRow dataRow)
        {
            this.database = database;

            this._code = dataRow.ItemArray.GetValue(0).ToString();
            this._name = dataRow.ItemArray.GetValue(1).ToString();
            this._address = dataRow.ItemArray.GetValue(2).ToString();
            this._city = dataRow.ItemArray.GetValue(3).ToString();
            this._locationCode = dataRow.ItemArray.GetValue(4).ToString();

            if (_locationCode == "") _locationCode = _city;
        }

        public string code { get { return _code; } set { _code = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string address { get { return _address; } set { _address = value; } }
        public string city { get { return _city; } set { _city = value; } }
        public string description { get { return _name + " (" + _locationCode+")"; } }

        public static CashSiteCollection getCollection(Database database)
        {
            CashSiteCollection cashSiteCollection = new CashSiteCollection();

            bool version2013R2 = CashSite.isVersion2013R2(database);

            DatabaseQuery databaseQuery = null;
            if (version2013R2)
            {
                databaseQuery = database.prepare("SELECT [Code], [Name], [Address], [City], [Location Code] FROM [" + database.getTableName("POS Store") + "] ORDER BY [Name]");
            }
            else
            {
                databaseQuery = database.prepare("SELECT [Code], [Name], [Address], [City], '' as locationCode FROM [" + database.getTableName("Cash Site") + "] ORDER BY [Name]");
            }

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                CashSite cashSite = new CashSite(database, dataSet.Tables[0].Rows[i]);

                cashSiteCollection.Add(cashSite);

                i++;
            }

            return cashSiteCollection;
        }

        public static bool isVersion2013R2(Database database)
        {
            try
            {
                DatabaseQuery databaseQuery = database.prepare("SELECT [Code] FROM [" + database.getTableName("POS Store") + "]");
                SqlDataReader dataReader = databaseQuery.executeQuery();
                dataReader.Close();

                return true;
            }
            catch (Exception) { }

            return false;
        }
    }
}
