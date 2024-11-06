using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for PostCode.
    /// </summary>
    public class SalesPerson
    {
        private string _code;
        private string _name;
        private string _email;
        private string _phoneNo;
        private Infojet infojetContext;

        public SalesPerson() { }

        public SalesPerson(Infojet infojetContext, string code)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this._code = code;

            getFromDatabase();
        }

        public SalesPerson(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this._code = dataRow.ItemArray.GetValue(0).ToString();
            this._name = dataRow.ItemArray.GetValue(1).ToString();
            this._email = dataRow.ItemArray.GetValue(2).ToString();
            this._phoneNo = dataRow.ItemArray.GetValue(3).ToString();
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code], [Name], [E-Mail], [Phone No_] FROM [" + infojetContext.systemDatabase.getTableName("Salesperson_Purchaser") + "] WHERE [Code] = @code");
            databaseQuery.addStringParameter("code", _code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                _code = dataReader.GetValue(0).ToString();
                _name = dataReader.GetValue(1).ToString();
                _email = dataReader.GetValue(2).ToString();
                _phoneNo = dataReader.GetValue(3).ToString();

            }

            dataReader.Close();


        }


        public string code { get { return _code; } set { _code = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string email { get { return _email; } set { _email = value; } }
        public string phoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
    }
}
