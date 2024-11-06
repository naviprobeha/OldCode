using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class SampleItem
    {
        public string no { get; set; }
        public string description { get; set; }

        public SampleItem(SqlDataReader dataReader)
        {
            no = dataReader.GetValue(0).ToString();
            description = dataReader.GetValue(1).ToString();
        }
    }
}