using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.PartnerPortal.Library
{
    /// <summary>
    /// SalesIDSetup motsvarar tabellen med samma namn och innehåller inställningar av olika slag. Styr mestadels vilka webbsidor som skall visas i olika lägen.
    /// </summary>
    public class SalesIDSetup
    {
        private string _webPageCodeCombine;
        private string _webPageCodeSalesPersonDetail;
        private string _webPageCodeSalesPersonProfile;
        private string _webPageCodeShowCase;
        private string _webPageCodeContact;
        private string _webPageCodeContProfile;

        private string _webPageCodeOrder1;
        private string _webPageCodeOrder2;
        private string _webPageCodeOrder3;
        private string _webPageCodeOrder4;

        private string _webPageCodeReOrders;

        private string _webPageCodeOrders1;
        private string _webPageCodeOrders2;
        private string _webPageCodeOrders3;
        private string _webPageCodeOrders4;
        private string _webPageCodeOrders5;

        private string _webPageCodeSPPreSort;

        private float _unitProfitInclVat;
        private float _unitProfitInclVatRea;

        private int _freeFreightPackageLimit;
        private int _freeFreightDaysCount;

        private Database database;

        /// <summary>
        /// Konstruktor som läser upp inställningarna från databasen. 
        /// </summary>
        /// <param name="database">En instans till databas-objektet. Fås ifrån infojet-instansen, egenskapen systemDatabase.</param>
        public SalesIDSetup(Database database)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = database;


            getFromDatabase();
        }



        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Page Code Combine], [Web Page Code SP Detail], [Web Page Code SP Profile], [Web Page Code Order 1], [Web Page Code Order 2], [Web Page Code Order 3], [Web Page Code Order 4], [Web Page Code Orders 1], [Web Page Code Orders 2], [Web Page Code Orders 3], [Web Page Code Orders 4], [Web Page Code Orders 5], [Web Page Code Showcase], [Web Page Code Contact], [Unit Profit Incl_ Vat], [Unit Profit Incl_ Vat Rea], [Web Page Code Cont_ Profile], [Web Page Code Re-Orders], [Web Page Code SP Pre Sort], [Free Freight Package Limit], [Free Freight Days Count] FROM [" + database.getTableName("FörsäljningsID Setup") + "] (NOLOCK)");

            SqlDataReader dataReader = databaseQuery.executeQuery();


            if (dataReader.Read())
            {
                readData(dataReader);
            }

            dataReader.Close();


        }

        private void readData(SqlDataReader dataReader)
        {
            this._webPageCodeCombine = dataReader.GetValue(0).ToString();
            this._webPageCodeSalesPersonDetail = dataReader.GetValue(1).ToString();
            this._webPageCodeSalesPersonProfile = dataReader.GetValue(2).ToString();
            this._webPageCodeOrder1 = dataReader.GetValue(3).ToString();
            this._webPageCodeOrder2 = dataReader.GetValue(4).ToString();
            this._webPageCodeOrder3 = dataReader.GetValue(5).ToString();
            this._webPageCodeOrder4 = dataReader.GetValue(6).ToString();
            this._webPageCodeOrders1 = dataReader.GetValue(7).ToString();
            this._webPageCodeOrders2 = dataReader.GetValue(8).ToString();
            this._webPageCodeOrders3 = dataReader.GetValue(9).ToString();
            this._webPageCodeOrders4 = dataReader.GetValue(10).ToString();
            this._webPageCodeOrders5 = dataReader.GetValue(11).ToString();
            this._webPageCodeShowCase = dataReader.GetValue(12).ToString();
            this._webPageCodeContact = dataReader.GetValue(13).ToString();
            this._unitProfitInclVat = float.Parse(dataReader.GetValue(14).ToString());
            this._unitProfitInclVatRea = float.Parse(dataReader.GetValue(15).ToString());
            this._webPageCodeContProfile = dataReader.GetValue(16).ToString();
            this._webPageCodeReOrders = dataReader.GetValue(17).ToString();
            this._webPageCodeSPPreSort = dataReader.GetValue(18).ToString();

            this._freeFreightPackageLimit = int.Parse(dataReader.GetValue(19).ToString());
            this._freeFreightDaysCount = int.Parse(dataReader.GetValue(20).ToString());
        }

        public string webPageCodeCombine { get { return _webPageCodeCombine; } }
        public string webPageCodeSalesPersonDetail { get { return _webPageCodeSalesPersonDetail; } }
        public string webPageCodeSalesPersonProfile { get { return _webPageCodeSalesPersonProfile; } }
        public string webPageCodeOrder1 { get { return _webPageCodeOrder1; } }
        public string webPageCodeOrder2 { get { return _webPageCodeOrder2; } }
        public string webPageCodeOrder3 { get { return _webPageCodeOrder3; } }
        public string webPageCodeOrder4 { get { return _webPageCodeOrder4; } }
        public string webPageCodeOrders1 { get { return _webPageCodeOrders1; } }
        public string webPageCodeOrders2 { get { return _webPageCodeOrders2; } }
        public string webPageCodeOrders3 { get { return _webPageCodeOrders3; } }
        public string webPageCodeOrders4 { get { return _webPageCodeOrders4; } }
        public string webPageCodeOrders5 { get { return _webPageCodeOrders5; } }
        public string webPageCodeShowCase { get { return _webPageCodeShowCase; } }
        public string webPageCodeContact { get { return _webPageCodeContact; } }
        public string webPageCodeReOrders { get { return _webPageCodeReOrders; } }

        public float unitProfitInclVat { get { return _unitProfitInclVat; } }
        public float unitProfitInclVatRea { get { return _unitProfitInclVatRea; } }
        public string webPageCodeContProfile { get { return _webPageCodeContProfile; } }
        public string webPageCodeSPPreSort { get { return _webPageCodeSPPreSort; } }

        public int freeFreightPackageLimit { get { return _freeFreightPackageLimit; } }
        public int freeFreightDaysCount { get { return _freeFreightDaysCount; } }

    }
}
