using System.Data.SqlClient;

namespace Api.Models
{
    public class OrderHistoryLine
    {
        public int line_no { get; set; }
        public int type { get; set; }
        public string no { get; set; }
        public string description { get; set; }
        public string description2 { get; set; }

        public decimal quantity { get; set; }
        public decimal quantity_reserved { get; set; }
        public decimal quantity_shipped { get; set; }
        public decimal unit_price { get; set; }
        public decimal line_discount_amount { get; set; }
        public decimal line_amount { get; set; }

        public decimal available_stock { get; set; }

        public bool stock_item_only { get; set; }        
        public string batch_no { get; set; }

        public string error_message { get; set; } = "";
        public OrderHistoryLine()
        { }

        public OrderHistoryLine(SqlDataReader dataReader)
        {
            line_no = dataReader.GetInt32(dataReader.GetOrdinal("Line No_"));
            type = dataReader.GetInt32(dataReader.GetOrdinal("Type"));
            no = dataReader["No_"].ToString();
            description = dataReader["Description"].ToString();
            description2 = dataReader["Description 2"].ToString();
            quantity = dataReader.GetDecimal(dataReader.GetOrdinal("Quantity"));
            unit_price = dataReader.GetDecimal(dataReader.GetOrdinal("Unit Price"));
            line_amount = dataReader.GetDecimal(dataReader.GetOrdinal("Line Amount"));
            line_discount_amount = dataReader.GetDecimal(dataReader.GetOrdinal("Line Discount Amount"));
            batch_no = dataReader["Item Lot No_"].ToString();

            if (!dataReader.IsDBNull(dataReader.GetOrdinal("quantityReserved")))
            {
                quantity_reserved = dataReader.GetDecimal(dataReader.GetOrdinal("quantityReserved")) * -1;
            }
            quantity_shipped = dataReader.GetDecimal(dataReader.GetOrdinal("Quantity Shipped"));
        }
    }
}