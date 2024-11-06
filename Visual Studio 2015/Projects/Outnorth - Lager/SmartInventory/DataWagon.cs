using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataWagon
    {
        private string _code;
        private int _noOfOrders;

        public DataWagon(string code, int noOfOrders)
        {

            //
            // TODO: Add constructor logic here
            //
            this._code = code;
            this._noOfOrders = noOfOrders;
        }

        public string code { get { return _code; } set { _code = value; } }
        public int noOfOrders { get { return _noOfOrders; } set { _noOfOrders = value; } }

    }

}
