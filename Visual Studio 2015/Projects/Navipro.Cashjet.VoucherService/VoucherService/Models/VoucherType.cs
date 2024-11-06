using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VoucherService.Models
{
    public class VoucherType
    {
        public string code { get; set; }
        public string accountNo { get; set; }
        public int type { get; set; }
        public int dueDateMonth { get; set; }

        public VoucherType(SqlDataReader dataReader)
        {

            code = dataReader["Code"].ToString();
            accountNo = dataReader["G_L Account No_"].ToString();
            type = dataReader.GetInt32(dataReader.GetOrdinal("Voucher Type"));

            dueDateMonth = 24;
        }

    }
}