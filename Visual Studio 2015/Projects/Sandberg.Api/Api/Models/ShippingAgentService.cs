using System;
using System.Data.SqlClient;

namespace Api.Models
{
    public class ShippingAgentService
    {
        public string shipping_agent_code { get; set; }
        public string service { get; set; }
        public string code { get; set; }
        public string description { get; set; }

        public string shipment_opt_additional { get; set; }
        public DateTime pickup_date_time { get; set; }
        public decimal freight_fee { get; set; }
        public string currency_code { get; set; }
        public string day_of_week { get; set; }
        public string external_service_codes { get; set; }
        public string external_service_code { get; set; }
        public string transport_service_code { get; set; }
        public ShippingAgentService()
        { }

        public ShippingAgentService(SqlDataReader dataReader)
        {
            shipping_agent_code = dataReader["Shipping Agent Code"].ToString();
            service = dataReader["Service"].ToString();
            code = dataReader["Code"].ToString();
            description = dataReader["Description"].ToString();
            shipment_opt_additional = dataReader["Shipment Opt__Additional Ser_"].ToString();
            external_service_code = dataReader["Shipping Agent Service Code"].ToString();
            transport_service_code = dataReader["Transport Service Code"].ToString();
        }
    }
}
