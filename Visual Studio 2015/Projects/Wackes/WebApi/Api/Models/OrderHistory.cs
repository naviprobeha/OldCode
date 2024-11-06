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


        public List<OrderHistoryLine> lines { get; set; }

        public OrderHistory()
        { }

        public OrderHistory(SqlDataReader dataReader)
        {
        }
    }
}