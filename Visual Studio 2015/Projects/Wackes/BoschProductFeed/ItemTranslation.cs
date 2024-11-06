using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoschProductFeed
{
    public class ItemTranslation
    {
        public string languageCode { get; set; }
        public string translation { get; set; }

        public ItemTranslation(SqlDataReader dataReader)
        {
            languageCode = dataReader.GetValue(1).ToString();
            translation = dataReader.GetValue(2).ToString();
        }
    }
}
