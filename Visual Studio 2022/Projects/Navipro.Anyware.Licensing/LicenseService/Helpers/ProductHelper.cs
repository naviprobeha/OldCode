using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LicenseService.Helpers
{
    public class ProductHelper
    {
        private StripeHelper stripeHelper;

        public ProductHelper()
        {
            stripeHelper = new StripeHelper();
        }

        public string getProducts()
        {
            return stripeHelper.makeGetRequest("products");
        }

        public string getProductPlans(string productName)
        {
            dynamic productsObject = JObject.Parse(getProducts());

            string productId = "";
            foreach (dynamic product in productsObject.data)
            {
                if (((string)product.name).ToUpper() == productName.ToUpper()) productId = product.id;
            }

            if (productId == "") return "";

            return stripeHelper.makeGetRequest("plans?product="+productId);
        }
    }
}
