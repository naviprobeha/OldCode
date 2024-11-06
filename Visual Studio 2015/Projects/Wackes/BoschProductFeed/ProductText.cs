using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoschProductFeed
{
    public class ProductText
    {
        public string no { get; set; }
        public string languageCode { get; set; }
        public string text { get; set; }

        public ProductText(SqlDataReader dataReader)
        {
            no = dataReader.GetValue(0).ToString();
            languageCode = dataReader.GetValue(1).ToString();
            text = dataReader.GetValue(2).ToString();
        }
    }
}
