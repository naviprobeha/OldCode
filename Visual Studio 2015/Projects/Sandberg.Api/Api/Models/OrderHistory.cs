using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class OrderHistory
    {
        public string no { get; set; }
        public string customer_no { get; set; }
        public string currency_code { get; set; }
        public string ship_to_name { get; set; }
        public string ship_to_address { get; set; }
        public string ship_to_address2 { get; set; }
        public string ship_to_post_code { get; set; }
        public string ship_to_city { get; set; }
        public string ship_to_country_code { get; set; }
        public string ship_to_contact { get; set; }
        public string shipping_agent_code { get; set; }
        public string shipping_agent_service_code { get; set; }
        public string payment_method { get; set; }

        public string marking { get; set; }
        public string sales_person_code { get; set; }
        public DateTime order_date { get; set; }
        public decimal total_amount { get; set; }
        public string status { get; set; }

        public string phone_no { get; set; }
        public string email { get; set; }

        public string internal_comment { get; set; }
        public int inventory_status { get; set; }
        public string external_document_no { get; set; }

        public string your_reference { get; set; }
        public decimal freight_fee { get; set; }

        public string action { get; set; }
        public string error_message { get; set; } = "";

        public List<OrderHistoryShipment> shipments { get; set; }

        public List<OrderHistoryLine> lines { get; set; }

        public OrderHistory()
        { }

        public OrderHistory(SqlDataReader dataReader)
        {
            no = dataReader["No_"].ToString();
            customer_no = dataReader["Sell-to Customer No_"].ToString();
            currency_code = dataReader["Currency Code"].ToString();
            ship_to_name = dataReader["Ship-to Name"].ToString();
            ship_to_address = dataReader["Ship-to Address"].ToString();
            ship_to_address2 = dataReader["Ship-to Address 2"].ToString();
            ship_to_post_code = dataReader["Ship-to Post Code"].ToString();
            ship_to_city = dataReader["Ship-to City"].ToString();
            ship_to_country_code = dataReader["Ship-to Country_Region Code"].ToString();
            order_date = dataReader.GetDateTime(dataReader.GetOrdinal("Order Date"));
            shipping_agent_code = dataReader["Shipping Agent Code"].ToString();
            shipping_agent_service_code = dataReader["Shipping Agent Service Code 2"].ToString();
            status = dataReader.GetInt32(dataReader.GetOrdinal("Status")).ToString();
            inventory_status = dataReader.GetInt32(dataReader.GetOrdinal("Invt_ Sys Status"));
            payment_method = dataReader["Payment Method Code"].ToString();
            external_document_no = dataReader["External Document No_"].ToString();
            marking = dataReader["Note of Goods"].ToString();

            if (!dataReader.IsDBNull(dataReader.GetOrdinal("LineAmount")))
            {
                total_amount = dataReader.GetDecimal(dataReader.GetOrdinal("LineAmount"));
            }

            shipments = new List<OrderHistoryShipment>();
            lines = new List<OrderHistoryLine>();

            if ((currency_code == "") || (currency_code == null)) currency_code = "SEK";
            if ((ship_to_country_code == "") || (ship_to_country_code == null)) ship_to_country_code = "SE";
        }
    }
}