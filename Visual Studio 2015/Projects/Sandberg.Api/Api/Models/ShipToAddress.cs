using System.Data.SqlClient;

namespace Api.Models
{
    public class ShipToAddress
    {
        public string code { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string post_code { get; set; }
        public string city { get; set; }
        public string country_code { get; set; }

        public string county { get; set; }
        public string shipping_agent_code { get; set; }
        public string shipping_agent_service_code { get; set; }
        public string contact { get; set; }
        public string phone_no { get; set; }
        public bool consolidated_shipping { get; set; }

        public ShipToAddress(SqlDataReader dataReader)
        {
            code = dataReader["Code"].ToString();
            name = dataReader["Name"].ToString();
            address = dataReader["Address"].ToString();
            address2 = dataReader["Address 2"].ToString();
            post_code = dataReader["Post Code"].ToString();
            city = dataReader["City"].ToString();
            county = dataReader["County"].ToString();
            country_code = dataReader["Country_Region Code"].ToString();
            shipping_agent_code = dataReader["Shipping Agent Code"].ToString();
            shipping_agent_service_code = dataReader["Shipping Agent Service Code"].ToString();
            contact = dataReader["Contact"].ToString();
            phone_no = dataReader["Phone No_"].ToString();
            consolidated_shipping = (dataReader["Allow Consolidation"].ToString() == "1");

        }
    }
}