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
        public string countryCode;
        public string description;
        public string text;
        public float from;
        public float to;
        public float amount;
        public string glAccountNo;
        public string vatProdPostingGroup;
        public string currencyCode;

        private Database database;

        public WebShipmentMethodDetail(Database database, string webSiteCode, string webShipmentMethodCode, string languageCode, string countryCode, float from, float to, string currencyCode)
        {
            //
            // TODO: Add constructor logic here
            //
            this.webSiteCode = webSiteCode;
            this.webShipmentMethodCode = webShipmentMethodCode;
            this.languageCode = languageCode;
            this.countryCode = countryCode;
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
            this.countryCode = dataRow.ItemArray.GetValue(3).ToString();
            this.description = dataRow.ItemArray.GetValue(4).ToString();
            this.text = dataRow.ItemArray.GetValue(5).ToString();
            this.from = float.Parse(dataRow.ItemArray.GetValue(6).ToString());
            this.to = float.Parse(dataRow.ItemArray.GetValue(7).ToString());
            this.amount = float.Parse(dataRow.ItemArray.GetValue(8).ToString());
            this.glAccountNo = dataRow.ItemArray.GetValue(9).ToString();
            this.vatProdPostingGroup = dataRow.ItemArray.GetValue(10).ToString();
            this.currencyCode = dataRow.ItemArray.GetValue(11).ToString();

        }

        private void getFromDatabase()
        {

            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Web Shipment Method Code], [Language Code], [Country Code], [Description], [Text], [From], [To], [Amount], [G_L Account No_], [VAT Prod_ Posting Group], [Currency Code] FROM [" + database.getTableName("Web Shipment Method Transl") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Shipment Method Code] = @webShipmentMethodCode AND [Language Code] = @languageCode AND [From] = @from AND [To] = @to AND [Currency Code] = @currencyCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webShipmentMethodCode", webShipmentMethodCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addDecimalParameter("from", from);
            databaseQuery.addDecimalParameter("to", to);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                webSiteCode = dataReader.GetValue(0).ToString();
                webShipmentMethodCode = dataReader.GetValue(1).ToString();
                languageCode = dataReader.GetValue(2).ToString();
                countryCode = dataReader.GetValue(3).ToString();
                description = dataReader.GetValue(4).ToString();
                text = dataReader.GetValue(5).ToString();
                from = float.Parse(dataReader.GetValue(6).ToString());
                to = float.Parse(dataReader.GetValue(7).ToString());
                amount = float.Parse(dataReader.GetValue(8).ToString());
                glAccountNo = dataReader.GetValue(9).ToString();
                vatProdPostingGroup = dataReader.GetValue(10).ToString();
                currencyCode = dataReader.GetValue(11).ToString();
            }

            dataReader.Close();
        }

        public float getVatFactor(Customer customer)
        {
            float vatFactor = 0;

            DatabaseQuery databaseQuery = database.prepare("SELECT "+database.convertField("[VAT %]")+", [VAT Calculation Type] FROM [" + database.getTableName("VAT Posting Setup") + "] WHERE [VAT Bus_ Posting Group] = @vatBusPostingGroup AND [VAT Prod_ Posting Group] = @vatProdPostingGroup");
            databaseQuery.addStringParameter("vatBusPostingGroup", customer.vatBusPostingGroup, 20);
            databaseQuery.addStringParameter("vatProdPostingGroup", vatProdPostingGroup, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                vatFactor = float.Parse(dataReader.GetValue(0).ToString());
                if (dataReader.GetValue(1).ToString() == "1") vatFactor = 0;
            }

            dataReader.Close();

            vatFactor = (vatFactor / 100) + 1;

            return vatFactor;
        }


    }
}
