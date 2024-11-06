using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Navipro.Infojet.Lib;

namespace Navipro.Newbody.PartnerPortal.Library
{
    public class WebInfoMessageHeader
    {
        private int _type;
        private string _code;
        private string _languageCode;
        private string _description;
        private string _header;
        private string _text;

        private Navipro.Infojet.Lib.Infojet infojetContext;

        public WebInfoMessageHeader()
        { }

        public WebInfoMessageHeader(Navipro.Infojet.Lib.Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this._type = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
            this._code = dataRow.ItemArray.GetValue(1).ToString();
            this._languageCode = dataRow.ItemArray.GetValue(2).ToString();
            this._description = dataRow.ItemArray.GetValue(3).ToString();
            
        
        }

        public int type { get { return _type; } set { _type = value; } }
        public string code { get { return _code; } set { _code = value; } }
        public string languageCode { get { return _languageCode; } set { _languageCode = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string header { get { return _header; } set { _header = value; } }
        public string text { get { return _text; } set { _text = value; } }

        public void updateDetails()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Text Type], [Text] FROM [" + infojetContext.systemDatabase.getTableName("Web Info Message Line") + "] WHERE [Type] = @type AND [Web Info Message Code] = @code AND [Language Code] = @languageCode");
            databaseQuery.addIntParameter("@type", _type);
            databaseQuery.addStringParameter("@code", _code, 20);
            databaseQuery.addStringParameter("@languageCode", _languageCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            header = "";
            text = "";

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) == 0)
                {
                    header = header + dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                }
                if (int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) == 1)
                {
                    text = text + dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                }

                i++;
            }

        }


        public static WebInfoMessageHeader[] getMessageArray(Navipro.Infojet.Lib.Infojet infojetContext, int type, SalesID salesId, WebUserAccount webUserAccount, bool includePackageAndDateInterval, string languageCode)
        {

            int role = 1;
            if (salesId.isPrimaryContactPerson(webUserAccount.no)) role = 2;
            if (salesId.isSubContactPerson(webUserAccount.no)) role = 3;

            int daysStart = (DateTime.Today - salesId.startDate).Days;
            int daysEnd = (salesId.closingDate - DateTime.Today).Days;

            SalesIDSalesPerson salesIdSalesPerson = new SalesIDSalesPerson(infojetContext.systemDatabase, salesId.code, webUserAccount.no);
            int soldPackages = 0; // salesIdSalesPerson.soldPackages;

            string packageAndDateQuery = "";
            if (includePackageAndDateInterval) packageAndDateQuery = "AND ((p.[Relative Type] = 0 AND (p.[From Days] = 0 OR p.[From Days] <= @daysStart) AND (p.[To Days] = 0 OR p.[To Days] >= @daysStart)) OR (p.[Relative Type] = 1 AND (p.[From Days] = 0 OR p.[From Days] <= @daysEnd) AND (p.[To Days] = 0 OR p.[To Days] >= @daysEnd))) AND (p.[From Packages] = 0 OR p.[From Packages] <= @soldPackages) AND (p.[To Packages] = 0 OR p.[To Packages] >= @soldPackages)";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT h.[Type], h.[Code], h.[Language Code], h.[Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Info Message Header") + "] h, [" + infojetContext.systemDatabase.getTableName("Web Info Message Publication") + "] p WHERE h.[Type] = @type AND h.[Type] = p.[Type] AND h.[Code] = p.[Web Info Message Code] AND h.[Language Code] = p.[Language Code] AND h.[Language Code] = @languageCode AND (p.[Customer No_] = '' OR p.[Customer No_] = @customerNo) AND (p.[BOM Item No_] = '' OR p.[BOM Item No_] = @bomItemNo) AND (p.[Role] = 0 OR p.[Role] = @role) AND (p.[From Date] = '1753-01-01 00:00:00' OR p.[From Date] >= GETDATE()) AND (p.[To Date] = '1753-01-01 00:00:00' OR p.[To Date] <= GETDATE()) "+packageAndDateQuery+" GROUP BY h.[Type], h.[Code], h.[Language Code], h.[Description]");
            databaseQuery.addIntParameter("@type", type);
            databaseQuery.addStringParameter("@languageCode", languageCode, 20);
            databaseQuery.addStringParameter("@customerNo", webUserAccount.customerNo, 20);
            databaseQuery.addStringParameter("@bomItemNo", salesId.itemSelection, 20);
            databaseQuery.addIntParameter("@role", role);
            databaseQuery.addIntParameter("@daysStart", daysStart);
            databaseQuery.addIntParameter("@daysEnd", daysEnd);
            databaseQuery.addIntParameter("@soldPackages", soldPackages);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            WebInfoMessageHeader[] webInfoMessageArray = new WebInfoMessageHeader[dataSet.Tables[0].Rows.Count];
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebInfoMessageHeader webInfoMessageHeader = new WebInfoMessageHeader(infojetContext, dataSet.Tables[0].Rows[i]);
                webInfoMessageHeader.updateDetails();
                webInfoMessageArray[i] = webInfoMessageHeader;

                i++;
            }

            return webInfoMessageArray;


        }


    }
}
