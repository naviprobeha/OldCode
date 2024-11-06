using Swedbank.Pay.Api.Models.Capture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Swedbank.Pay.Api.Services.SwedbankService swedbankService = new Swedbank.Pay.Api.Services.SwedbankService("https://api.externalintegration.payex.com", "7d2e645c233f5ef37a4e488d98498e8129f8919d801ba09764c66864b968b8aa");

            var respone = swedbankService.Capture(new CaptureRequest { OrderId = "59AA709C-E9F8-4BD8-AFD6-08D81BEDB293".ToLower(), Transaction = { Amount = 120500, VatAmount = 25000, PayeeReference = Guid.NewGuid().ToString(), Description = "Capture" } });
            if (!respone.Capture.Transaction.State.Equals("Completed"))
            {
                throw new Exception("Did not succeed.");
            }

        }
    }
}
