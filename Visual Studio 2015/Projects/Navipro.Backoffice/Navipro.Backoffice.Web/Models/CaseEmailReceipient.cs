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
    public class CaseEmailRecipient
    {
        public CaseEmailRecipient()
        {


        }

        public CaseEmailRecipient(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            caseNo = dataReader.GetValue(0).ToString();
            email = dataReader.GetValue(1).ToString();
            name = dataReader.GetValue(2).ToString();
            type = int.Parse(dataReader.GetValue(3).ToString());

            if (name.Length > 40) name = name.Substring(0, 40);   
        }

        [Required]
        [Display(Name = "Ärendenr")]
        public String caseNo { get; set; }

        [Required]
        [Display(Name = "E-postadress")]
        public String email { get; set; }

        [Required]
        [Display(Name = "Namn")]
        public String name { get; set; }

        public String typeText { get { if (type == 0) return "Avsändare"; if (type == 1) return "Mottagare"; if (type == 2) return "Kopia"; return ""; } }

        [Required]
        [Display(Name = "Typ")]
        public int type { get; set; }

        public static List<CaseEmailRecipient> getList(Database database, string caseNo)
        {
            List<CaseEmailRecipient> caseEmailReceiverList = new List<CaseEmailRecipient>();

            DatabaseQuery query = database.prepare("SELECT [Case No_], [E-mail], [Name], [Type] FROM [" + database.getTableName("Case E-mail Recipient") + "] WITH (NOLOCK) WHERE [Case No_] = @caseNo");
            query.addStringParameter("caseNo", caseNo, 20);

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                CaseEmailRecipient caseEmailReceiver = new CaseEmailRecipient(dataReader);
                caseEmailReceiverList.Add(caseEmailReceiver);

            }

            dataReader.Close();

            return caseEmailReceiverList;
        }





    }



}