using VoucherService.Library;
using VoucherService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VoucherService.Controllers
{
    [RoutePrefix("api/voucher")]
    public class VoucherController : ApiController
    {
        // GET api/voucher
        [HeaderAuthorization]
        public Voucher Get(string voucherType, string no)
        {
            Voucher voucher = VoucherHelper.getVoucher(voucherType, no);

            return voucher;

        }

        // POST api/voucher
        [HeaderAuthorization]
        public Voucher Post(string voucherType, string no, string amount = "", string currencyCode = "", string orderNo = "")
        {
            decimal amountDec = 0;
            amountDec = Decimal.Parse(amount);

            if (amountDec == 0) throw new Exception("401: Illegal amount");

            Voucher voucher = VoucherHelper.getVoucher(voucherType, no);

            if (voucher != null)
            {
                if (currencyCode != voucher.currencyCode) throw new Exception("402: Illegal currency code. Must be " + voucher.currencyCode + ".");
            }

            return VoucherHelper.create(voucherType, no, amountDec, orderNo, currencyCode);

            //return voucher;

        }


    }
}
