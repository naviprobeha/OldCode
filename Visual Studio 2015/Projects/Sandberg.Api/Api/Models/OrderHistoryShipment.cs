using System;
using System.Data.SqlClient;

namespace Api.Models
{
    public class OrderHistoryShipment
    {
        public string no { get; set; }
        public DateTime shipment_date { get; set; }
        public string tracking_no { get; set; }

        public OrderHistoryShipment()
        { }

        public OrderHistoryShipment(SqlDataReader dataReader)
        {
            no = dataReader["No_"].ToString();
            tracking_no = dataReader["Package Tracking No_"].ToString();
            shipment_date = dataReader.GetDateTime(dataReader.GetOrdinal("Posting Date"));

        }
    }
}