using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LicenseService.Helpers
{
    public class CustomerHelper
    {
        private StripeHelper stripeHelper;

        public CustomerHelper()
        {
            stripeHelper = new StripeHelper();
        }

        public string getCustomer(string customerId)
        {
            return stripeHelper.makeGetRequest("customers/" + customerId);
        }

        public string findCustomerByEmail(string email)
        {
            return stripeHelper.makeGetRequest("customers?email=" + email);
        }

        public string createCustomer(string jsonData)
        {
            JObject jObject = JObject.Parse(jsonData);

            string customerPayload = "name=" + jObject.GetValue("name").ToString() +
                "&description=" + jObject.GetValue("description").ToString() +
                "&email=" + jObject.GetValue("email").ToString() +
                "&phone=" + jObject.GetValue("phone").ToString() +
                "&address[line1]=" + jObject.GetValue("address").ToString() +
                "&address[line2]=" + jObject.GetValue("address2").ToString() +
                "&address[country]=" + jObject.GetValue("country").ToString() +
                "&address[postal_code]=" + jObject.GetValue("postCode").ToString() +
                "&address[state]=" + jObject.GetValue("state").ToString() +
                "&address[city]=" + jObject.GetValue("city").ToString();

            string customerJson = stripeHelper.makePostRequest("customers", customerPayload);
            JObject customerObject = JObject.Parse(customerJson);
            string customerId = customerObject.GetValue("id").ToString();


            string vatPayload = "type=" + jObject.GetValue("vatType") +
                       "&value=" + jObject.GetValue("vatRegistrationNo");


            stripeHelper.makePostRequest("customers/" + customerId + "/tax_ids", vatPayload);

            return getCustomer(customerId);

        }

        public string updateCustomer(string customerId, string jsonData)
        {
            JObject jObject = JObject.Parse(jsonData);

            string customerPayload = "name=" + jObject.GetValue("name").ToString() +
                "&description=" + jObject.GetValue("description").ToString() +
                "&email=" + jObject.GetValue("email").ToString() +
                "&phone=" + jObject.GetValue("phone").ToString() +
                "&address[line1]=" + jObject.GetValue("address").ToString() +
                "&address[line2]=" + jObject.GetValue("address2").ToString() +
                "&address[country]=" + jObject.GetValue("country").ToString() +
                "&address[postal_code]=" + jObject.GetValue("postCode").ToString() +
                "&address[state]=" + jObject.GetValue("state").ToString() +
                "&address[city]=" + jObject.GetValue("city").ToString();

            stripeHelper.makePostRequest("customers/" + customerId, customerPayload);

            string vatPayload = "type=" + jObject.GetValue("vatType") +
                         "&value=" + jObject.GetValue("vatRegistrationNo");

            string vatTaxId = jObject.GetValue("vatTaxId").ToString();
            
            if (vatTaxId == "")
            {
                stripeHelper.makePostRequest("customers/" + customerId + "/tax_ids/" + vatTaxId, vatPayload);
            }

            return getCustomer(customerId);
        }

        public void pushUsage(string customerId, int users)
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            dynamic customerObject = JObject.Parse(getCustomer(customerId));

            if (customerObject.subscriptions != null)
            {
                foreach (dynamic subscription in customerObject.subscriptions.data)
                {
                    if (subscription.items != null)
                    {
                        foreach(dynamic subItem in subscription.items.data)
                        {
                            string id = subItem.id;

                            string payload = "quantity="+users+"&timestamp="+unixTimestamp.ToString()+"&action=set";
                            stripeHelper.makePostRequest("subscription_items/"+id+"/usage_records", payload);
                        }
                    }
                }
            }
        }
    }
}
