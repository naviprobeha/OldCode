using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class CustomerLedgerEntry
    {
        public int entry_no { get; set; }
        public int document_type { get; set; }
        public string document_no { get; set; }
        public DateTime posting_date { get; set; }
        public DateTime due_date { get; set; }
        public decimal amount { get; set; }
        public decimal amount_lcy { get; set; }
        public decimal remaining_amount { get; set; }
        public decimal remaining_amount_lcy { get; set; }
        public string currency_code { get; set; }


        public CustomerLedgerEntry()
        {

        }
        public CustomerLedgerEntry(SqlDataReader dataReader, List<CustomerLedgerEntry> amountList, List<CustomerLedgerEntry> remainingAmountList)
        {
            entry_no = dataReader.GetInt32(dataReader.GetOrdinal("Entry No_"));
            document_type = dataReader.GetInt32(dataReader.GetOrdinal("Document Type"));
            document_no = dataReader["Document No_"].ToString();
            posting_date = dataReader.GetDateTime(dataReader.GetOrdinal("Posting Date"));
            due_date = dataReader.GetDateTime(dataReader.GetOrdinal("Due Date"));
            currency_code = dataReader["Currency Code"].ToString();

            CustomerLedgerEntry amountEntry = amountList.FirstOrDefault(t => t.entry_no == entry_no);
            if (amountEntry != null)
            {
                amount = amountEntry.amount;
                amount_lcy = amountEntry.amount_lcy;
            }
            CustomerLedgerEntry remainingAmountEntry = remainingAmountList.FirstOrDefault(t => t.entry_no == entry_no);
            if (remainingAmountEntry != null)
            {             
                remaining_amount = remainingAmountEntry.remaining_amount;
                remaining_amount_lcy = remainingAmountEntry.remaining_amount_lcy;
            }

        }
    }
}

