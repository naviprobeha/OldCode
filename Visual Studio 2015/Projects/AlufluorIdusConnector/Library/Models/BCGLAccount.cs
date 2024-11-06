using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviPro.Alufluor.Idus.Library.Models
{
    public class BCGLAccount
    {
        public string no { get; set; }
        public string name { get; set; }


        public IdusAccount toIdusAccount()
        {
            IdusAccount account = new IdusAccount();
            account.type = "uAccount.TAccount";
            account.fields = new IdusAccountMembers
            {
                FAccountType = "1",
                FAccount = no,
                FAccountText = name
            };

            return account;
        }
    }
}
