using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarnaV2Wrapper
{
    public class KlarnaV2Wrapper
    {
        public static string performCredit(string jsonBody, int merchantId, string secret, string invoiceId, string returnOrderNo, string languageCode, string currencyCode, string countryCode)
        {
            JObject jObject = JObject.Parse(jsonBody);

            Navipro.KlarnaAPI.Wrapper.KlarnaHelper klarnaHelper = new Navipro.KlarnaAPI.Wrapper.KlarnaHelper();
            klarnaHelper.setRegion(languageCode, currencyCode);

            JArray linesArray = (JArray)jObject.GetValue("lines");

            foreach(JObject line in linesArray)
            {
                klarnaHelper.addArticle(
                    int.Parse(line.GetValue("quantity").ToString()),
                    line.GetValue("itemNo").ToString(),
                    line.GetValue("description").ToString(),
                    double.Parse(line.GetValue("unitPrice").ToString()),
                    double.Parse(line.GetValue("vat").ToString()),
                    double.Parse(line.GetValue("discount").ToString()));

            }

            klarnaHelper.setBillingAddress("", "", "", "", "", "", "", "", "", countryCode, "", "");

            return klarnaHelper.creditPart(merchantId, secret, invoiceId, returnOrderNo);
        }


    }
}
