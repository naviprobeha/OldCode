using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Models
{
    public class CaseTransactionLogEntry
    {
        public CaseTransactionLogEntry()
        {


        }

        public CaseTransactionLogEntry(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            type = dataReader.GetValue(0).ToString();
            description = dataReader.GetValue(1).ToString();
            transactionDateTime = DateTime.Parse(dataReader.GetDateTime(2).ToString("yyyy-MM-dd")+" "+ dataReader.GetDateTime(3).ToString("HH:mm:ss"));
            userId = dataReader.GetValue(4).ToString();
        }

        [Required]
        [Display(Name = "Typ")]
        public String type { get; set; }

        [Required]
        [Display(Name = "Beskrivning")]
        public String description { get; set; }

        [Required]
        [Display(Name = "Datum")]
        public DateTime transactionDateTime { get; set; }

        public string transactionDateTimeText { get { return transactionDateTime.ToString("yyyy-MM-dd HH:mm"); } }

        public string userId { get; set; }

        public static List<CaseTransactionLogEntry> getList(Database database, string caseNo)
        {
            List<CaseTransactionLogEntry> logEntryList = new List<CaseTransactionLogEntry>();

            DatabaseQuery query = database.prepare("SELECT [Case Transaction Type Code], [Description], [Posting Date], [Posting Time], [User-ID] FROM [" + database.getTableName("Case Transaction Log Entry") + "] WITH (NOLOCK) WHERE [Case No_] = @caseNo ORDER BY [Entry No_] DESC");
            query.addStringParameter("caseNo", caseNo, 20);

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                CaseTransactionLogEntry logEntry = new CaseTransactionLogEntry(dataReader);
                logEntryList.Add(logEntry);

            }

            dataReader.Close();

            return logEntryList;
        }



    }



}