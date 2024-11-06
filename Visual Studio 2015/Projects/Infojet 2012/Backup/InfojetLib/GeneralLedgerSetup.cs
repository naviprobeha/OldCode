using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for GeneralLedgerSetup.
	/// </summary>
    /// 
    
	public class GeneralLedgerSetup
	{
		private Database database;

		public string lcyCode;

		public GeneralLedgerSetup(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;	

			getFromDatabase();
		}


        private void getFromDatabase()
        {
            if (System.Web.HttpContext.Current.Session["lcyCode"] != null)
            {
                lcyCode = (string)System.Web.HttpContext.Current.Session["lcyCode"];
                return;
            }


            SqlDataReader dataReader = database.query("SELECT [LCY Code] FROM [" + database.getTableName("General Ledger Setup") + "]");
            if (dataReader.Read())
            {
                lcyCode = dataReader.GetValue(0).ToString();
            }

            dataReader.Close();

            System.Web.HttpContext.Current.Session.Add("lcyCode", lcyCode);
        }

	}
}
