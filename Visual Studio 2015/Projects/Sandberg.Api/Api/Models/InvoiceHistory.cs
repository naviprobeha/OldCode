using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class InvoiceHistory
    {
        public string no { get; set; }
        public string customer_no { get; set; }
        public string order_no { get; set; }
        public string currency_code { get; set; }
        public string bill_to_name { get; set; }
        public string bill_to_address { get; set; }
        public string bill_to_address2 { get; set; }
        public string bill_to_post_code { get; set; }
        public string bill_to_city { get; set; }
        public string bill_to_country_code { get; set; }
        public string external_document_no { get; set; }
        public DateTime invoice_date { get; set; }
        public decimal total_amount { get; set; }
        public decimal total_amount_incl_vat { get; set; }


        public InvoiceHistory()
        { }

        public InvoiceHistory(SqlDataReader dataReader)
        {
            no = dataReader["No_"].ToString();
            order_no = dataReader["Order No_"].ToString();
            customer_no = dataReader["Bill-to Customer No_"].ToString();
            currency_code = dataReader["Currency Code"].ToString();
            bill_to_name = dataReader["Bill-to Name"].ToString();
            bill_to_address = dataReader["Bill-to Address"].ToString();
            bill_to_address2 = dataReader["Bill-to Address 2"].ToString();
            bill_to_post_code = dataReader["Bill-to Post Code"].ToString();
            bill_to_city = dataReader["Bill-to City"].ToString();
            bill_to_country_code = dataReader["Bill-to Country_Region Code"].ToString();
            external_document_no = dataReader["External Document No_"].ToString();
            invoice_date = dataReader.GetDateTime(dataReader.GetOrdinal("Document Date"));

            if (!dataReader.IsDBNull(dataReader.GetOrdinal("Amount")))
            {
                total_amount = dataReader.GetDecimal(dataReader.GetOrdinal("Amount"));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal("AmountIncludingVAT")))
            {
                total_amount_incl_vat = dataReader.GetDecimal(dataReader.GetOrdinal("AmountIncludingVAT"));
            }

            if ((currency_code == "") || (currency_code == null)) currency_code = "SEK";
            if ((bill_to_country_code == "") || (bill_to_country_code == null)) bill_to_country_code = "SE";

        }
    }
}