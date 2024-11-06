using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.CashJet.AddIns
{
    public class NAVWebRequest
    {
        public static bool doRequest(ref System.Net.HttpWebRequest WebRequest, ref System.Net.WebException WebException, ref System.Net.WebResponse WebResponse)
        {
            try
            {
                WebResponse = WebRequest.GetResponse();
                return true;
            }
            catch (System.Net.WebException webExcp)
            {
                WebException = webExcp;
                return false;
            }
        }

    }
}
