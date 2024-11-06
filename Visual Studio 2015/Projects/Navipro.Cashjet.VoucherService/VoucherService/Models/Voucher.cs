using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VoucherService.Models
{
    public class Voucher
    {
        public string no { get; set; }
        public string voucherType { get; set; }
        public decimal balance { get; set; } = 0;
        public DateTime expireDate { get; set; }
        public string currencyCode { get; set; }
        public bool valid { get; set; }

        public Voucher(SqlDataReader dataReader)
        {

            no = dataReader["Voucher No_"].ToString();
            voucherType = dataReader["Voucher Type Code"].ToString();
            expireDate = dataReader.GetDateTime(dataReader.GetOrdinal("Due Date"));
            currencyCode = dataReader["Currency Code"].ToString();

            if (expireDate > DateTime.Today) valid = true;
        }

    }
}