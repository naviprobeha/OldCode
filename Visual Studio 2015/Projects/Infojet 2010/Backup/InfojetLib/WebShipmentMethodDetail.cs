using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebShipmentMethod.
    /// </summary>
    public class WebShipmentMethodDetail
    {

        public string webSiteCode;
        public string webShipmentMethodCode;
        public string languageCode;
        public string description;
        public string text;
        public float from;
        public float to;
        public float amount;
        public string glAccountNo;
        public string vatProdPostingGroup;
        public string currencyCode;

        private Database database;

        public WebShipmentMethodDetail(Database database, string webSiteCode, string webShipmentMethodCode, string languageCode, float from, float to, string currencyCode)
        {
            //
            // TODO: Add constructor logic here
            //
            this.webSiteCode = webSiteCode;
            this.webShipmentMethodCode = webShipmentMethodCode;
            this.languageCode = languageCode;
            this.currencyCode = currencyCode;

            this.database = database;

            getFromDatabase();
        }


        public WebShipmentMethodDetail(Database database, DataRow dataRow)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = database;

            this.webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
            this.webShipmentMethodCode = dataRow.ItemArray.GetValue(1).ToString();
            this.languageCode = dataRow.ItemArray.GetValue(2).ToString();
            this.description = dataRow.ItemArray.GetValue(3).ToString();
            this.text = dataRow.ItemArray.GetValue(4).ToString();
            this.from = float.Parse(dataRow.ItemArray.GetValue(5).ToString());
            this.to = float.Parse(dataRow.ItemArray.GetValue(6).ToString());
            this.amount = float.Parse(dataRow.ItemArray.GetValue(7).ToString());
            this.glAccountNo = dataRow.ItemArray.GetValue(8).ToString();
            this.vatProdPostingGroup = dataRow.ItemArray.GetValue(9).ToString();
            this.currencyCode = dataRow.ItemArray.GetValue(10).ToString();

        }

        private void getFromDatabase()
        {

            SqlDataReader dataReader = database.query("SELECT [Web Site Code], [Web Shipment Method Code], [Language Code], [Description], [Text], [From], [To], [Amount], [G_L Account No_], [VAT Prod_ Posting Group], [Currency Code] FROM [" + database.getTableName("Web Shipment Method Transl") + "] WHERE [Web Site Code] = '" + this.webSiteCode + "' AND [Web Shipment Method Code] = '" + this.webShipmentMethodCode + "' AND [Language Code] = '"+languageCode+"' AND [From] = '"+from+"' AND [To] = '"+to+"' AND [Currency Code] = '"+this.currencyCode+"'");
            if (dataReader.Read())
            {
                webSiteCode = dataReader.GetValue(0).ToString();
                webShipmentMethodCode = dataReader.GetValue(1).ToString();
                languageCode = dataReader.GetValue(2).ToString();
                description = dataReader.GetValue(3).ToString();
                text = dataReader.GetValue(4).ToString();
                from = float.Parse(dataReader.GetValue(5).ToString());
                to = float.Parse(dataReader.GetValue(6).ToString());
                amount = float.Parse(dataReader.GetValue(7).ToString());
                glAccountNo = dataReader.GetValue(8).ToString();
                vatProdPostingGroup = dataReader.GetValue(9).ToString();
                currencyCode = dataReader.GetValue(10).ToString();
            }

            dataReader.Close();
        }

        public float getVatFactor(Customer customer)
        {
            float vatFactor = 0;

            SqlDataReader dataReader = database.query("SELECT [VAT %] FROM [" + database.getTableName("VAT Posting Setup") + "] WHERE [VAT Bus_ Posting Group] = '" + customer.vatBusPostingGroup + "' AND [VAT Prod_ Posting Group] = '" + this.vatProdPostingGroup + "'");
            if (dataReader.Read())
            {
                vatFactor = float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            vatFactor = (vatFactor / 100) + 1;

            return vatFactor;
        }


    }
}
