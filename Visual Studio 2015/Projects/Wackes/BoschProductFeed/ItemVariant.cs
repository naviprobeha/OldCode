using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoschProductFeed
{
    public class ItemVariant
    {
        public string code { get; set; }
        public string description { get; set; }
        public string colorCode { get; set; }
        public string sizeCode { get; set; }
        public int stock { get; set; }

        public ItemVariant(SqlDataReader dataReader)
        {
            code = dataReader.GetValue(1).ToString();
            description = dataReader.GetValue(2).ToString();
            colorCode = dataReader.GetValue(3).ToString();
            sizeCode = dataReader.GetValue(4).ToString();
        }
    }
}
