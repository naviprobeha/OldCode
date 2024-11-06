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
    [RoutePrefix("api/use")]
    public class UseController : ApiController
    {
        // GET api/use
        [HeaderAuthorization]
        public Voucher Get(string voucherType, string no)
        {
            Voucher voucher = VoucherHelper.getVoucher(voucherType, no);

            return voucher;

        }

        // POST api/use
        [HeaderAuthorization]
        public Voucher Post(string voucherType, string no, decimal amount, string currencyCode = "", string orderNo = "")
        {
            if (amount == 0) throw new Exception("401: Illegal amount");

            Voucher voucher = VoucherHelper.getVoucher(voucherType, no);

            if (currencyCode != voucher.currencyCode) throw new Exception("402: Illegal currency code. Must be "+voucher.currencyCode+".");

            if (voucher.balance < amount) throw new Exception("401: Amount is larger than Balance.");

            return VoucherHelper.use(voucherType, no, amount, orderNo, currencyCode);


        }


    }
}
