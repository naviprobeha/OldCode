using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Customer
    {
        public string no { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string post_code { get; set; }
        public string city { get; set; }
        public string country_code { get; set; }
        public string shipping_agent_code { get; set; }
        public string shipping_agent_service_code { get; set; }
        public string currency_code { get; set; }

        public string customer_price_group { get; set; }

        public string customer_disc_group { get; set; }
        public string payment_terms { get; set; }

        public bool blocked { get; set; }
        public string customer_type_code { get; set; }     
        
        public string partner_code { get; set; }   

        public bool allow_custom_ship_to_address { get; set; }    
        
        public string store_chain { get; set; }
        public string bill_to_customer_no { get; set; }

        public bool consolidated_shipping { get; set; }
        public decimal credit_limit_lcy { get; set; }
        public decimal outstanding_order_amount_lcy { get; set; }
        public string vat_bus_posting_group { get; set; }
        public string shipment_method_code { get; set; }

        public string county { get; set; }
        public string phone_no { get; set; }
        public string email { get; set; }
        public string vat_registration_no { get; set; }

        public List<ShipToAddress> delivery_addresses { get; set; }

        public Customer()
        {

        }
        public Customer(SqlDataReader dataReader)
        {
            no = dataReader["No_"].ToString();
            name = dataReader["Name"].ToString();
            address = dataReader["Address"].ToString();
            address2 = dataReader["Address 2"].ToString();
            post_code = dataReader["Post Code"].ToString();
            city = dataReader["City"].ToString();
            country_code = dataReader["Country_Region Code"].ToString();
            shipping_agent_code = dataReader["Shipping Agent Code"].ToString();
            shipping_agent_service_code = dataReader["Shipping Agent Service Code"].ToString();
            currency_code = dataReader["Currency Code"].ToString();
            customer_price_group = dataReader["Customer Price Group"].ToString();
            customer_type_code = dataReader["Kundkategori"].ToString();
            if (dataReader["Blocked"].ToString() != "0") blocked = true;
            customer_disc_group = dataReader["Customer Disc_ Group"].ToString();
            payment_terms = dataReader["Payment Terms Code"].ToString();
            allow_custom_ship_to_address = (dataReader["Web Allow Modify Ship-to Addr_"].ToString() == "1");
            consolidated_shipping = (dataReader["Consolidated Shipping"].ToString() == "1");
            store_chain = dataReader["Corporate Chain Code"].ToString();
            credit_limit_lcy = dataReader.GetDecimal(dataReader.GetOrdinal("Credit Limit (LCY)"));
            bill_to_customer_no = dataReader["Bill-to Customer No_"].ToString();

            try
            {
                if (!dataReader.IsDBNull(dataReader.GetOrdinal("outstandingOrderAmountLcy")))
                {
                    outstanding_order_amount_lcy = dataReader.GetDecimal(dataReader.GetOrdinal("outstandingOrderAmountLcy"));
                }
            }
            catch (Exception)
            { }

            try
            {

                if (!dataReader.IsDBNull(dataReader.GetOrdinal("partnerCode")))
                {
                    partner_code = dataReader["partnerCode"].ToString();
                }
            }
            catch (Exception)
            { }

            delivery_addresses = new List<ShipToAddress>();

            if ((currency_code == "") || (currency_code == null)) currency_code = "SEK";

            vat_bus_posting_group = dataReader["VAT Bus_ Posting Group"].ToString();
            shipment_method_code = dataReader["Shipment Method Code"].ToString();
            email = dataReader["E-Mail"].ToString();
            phone_no = dataReader["Phone No_"].ToString();
            county = dataReader["County"].ToString();
            vat_registration_no = dataReader["VAT Registration No_"].ToString();
            
        }
    }
}

