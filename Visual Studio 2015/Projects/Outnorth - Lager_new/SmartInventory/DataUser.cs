﻿using System;
using System.Data.SqlServerCe;
using System.Data;
using System.Xml;

namespace Navipro.SmartInventory
{
    /// <summary>
    /// Summary description for DataSalesLine.
    /// </summary>
    public class DataUser
    {
        private string _code;
        private string _name;

        public DataUser(string code, string name)
        {

            //
            // TODO: Add constructor logic here
            //
            this._code = code;
            this._name = name;
        }

        public string code { get { return _code; } set { _code = value; } }
        public string name { get { return _name; } set { _name = value; } }

    }

}