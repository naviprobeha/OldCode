using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviPro.Alufluor.Idus.Library.Models
{
    public class BCDimensionValue
    {
        public string dimensionCode { get; set; }
        public string code { get; set; }
        public string name { get; set; }


        public IdusAccount toIdusAccount(string accountType)
        {
            IdusAccount account = new IdusAccount();
            account.type = "uAccount.TAccount";
            account.fields = new IdusAccountMembers
            {
                FAccountType = accountType,
                FAccount = code,
                FAccountText = name
            };

            return account;
        }
    }
}
