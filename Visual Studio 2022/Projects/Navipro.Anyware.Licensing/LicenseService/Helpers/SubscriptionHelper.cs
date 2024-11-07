using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LicenseService.Helpers
{
    public class SubscriptionHelper
    {
        private StripeHelper stripeHelper;

        public SubscriptionHelper()
        {
            stripeHelper = new StripeHelper();
        }

        public string getSubscriptions(string customerId)
        {
            ProductHelper productHelper = new ProductHelper();
            dynamic productsObject = JObject.Parse(productHelper.getProducts());


            string jsonContent = stripeHelper.makeGetRequest("subscriptions?customer=" + customerId);
            if (jsonContent == "") return "";

            dynamic subscriptionsObject = JObject.Parse(jsonContent);

            Dictionary<string, List<dynamic>> subscriptionDict = new Dictionary<string, List<dynamic>>();            

            foreach (dynamic subscription in subscriptionsObject.data)
            {
                foreach (dynamic subscriptionItem in subscription.items.data)
                {
                    string productId = subscriptionItem.plan.product;

                    foreach(dynamic product in productsObject.data)
                    {
                        if (product.id == productId)
                        {
                            if (!subscriptionDict.ContainsKey(((string)product.name).ToUpper()))
                            {
                                subscriptionDict.Add(((string)product.name).ToUpper(), new List<dynamic>());
                            }
                            List<dynamic> list = subscriptionDict[((string)product.name).ToUpper()];
                            list.Add(subscription);
                            subscriptionDict[((string)product.name).ToUpper()] = list;
                            break;
                        }
                    }
                }
            }

            List<dynamic> subscriptionList = new List<dynamic>();

            foreach (string productName in subscriptionDict.Keys)
            {
                if ((subscriptionDict[productName]).Count > 0)
                {
                    dynamic subscriptionRecord = new
                    {
                        name = productName,
                        list = subscriptionDict[productName]
                    };
                    subscriptionList.Add(subscriptionRecord);
                }
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(subscriptionList);
        }

        public string createSubscription(string customerId, string planId, string companyName, string countryCode)
        {


            string payload = "items[0][plan]=" + planId +
                "&customer=" + customerId +
                "&metadata[CompanyName]=" + companyName +
                "&collection_method=send_invoice" +
                "&days_until_due=30";

            if (countryCode == "SE")
            {
                payload = payload + "&default_tax_rates[0]=txr_1Fda77CiVLlldKXeyRfPbUFk";
            }

            
            return stripeHelper.makePostRequest("subscriptions", payload);

        }

        public string cancelSubscription(string subscriptionId)
        {
            string payload = "cancel_at_period_end=true";

            return stripeHelper.makePostRequest("subscriptions/"+subscriptionId, payload);

        }

    }
}
