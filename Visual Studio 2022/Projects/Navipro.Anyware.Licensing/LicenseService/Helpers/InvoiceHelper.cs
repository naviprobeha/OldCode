using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseService.Helpers
{
    public class InvoiceHelper
    {
        private StripeHelper stripeHelper;

        public InvoiceHelper()
        {
            stripeHelper = new StripeHelper();
        }

        public string getInvoices(string customerId)
        {
            return stripeHelper.makeGetRequest("invoices?customer=" + customerId);
        }
    }
}
