using System.Data.SqlClient;

namespace Api.Models
{
    public class ItemBatch
    {
        public string no { get; set; }
        public decimal stock_level { get; set; }

        public ItemBatch()
        { }

        public ItemBatch(SqlDataReader dataReader)
        {
            no = dataReader["Item Lot No"].ToString();
            stock_level = dataReader.GetDecimal(dataReader.GetOrdinal("Qty"));
        }
    }
}
