using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace VoucherService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://cashjet.navipro.se/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VoucherService : System.Web.Services.WebService
    {

        [WebMethod]
        public int CheckStatus(string voucherTypeCode, string voucherNo)
        {
            return 1;  
        }

        [WebMethod]
        public decimal CalcAvailableBalance(string voucherTypeCode, string voucherNo)
        {
            return 10;
        }

        [WebMethod]
        public void UseVoucher(string voucherTypeCode, string voucherNo, decimal amount, string source, string transactionNo)
        {

        }

        [WebMethod]
        public void AddAmountToVoucher(string voucherTypeCode, string voucherNo, decimal amount, string source, string transactionNo)
        {

        }
    }
}
