using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Xml;

namespace Navipro.CashJet.AddIns
{
    public class KlarnaV3Handler
    {
        private JArray orderLines;
        private string username;
        private string password;
        private string locale;
        private string countryCode;
        private string baseUrl;

        private string logoUrl;
        private string termsUrl;
        private string policyUrl;
        private string checkoutUrl;
        private string privacyUrl;
        private string confirmationUrl;
        private string pageTitle;

        public KlarnaV3Handler(string username, string password)
        {
            this.username = username;
            this.password = password;
            orderLines = new JArray();

            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            XmlDocument configXml = new XmlDocument();

            configXml.Load("C:\\klarna\\config.xml");
            XmlElement docElement = configXml.DocumentElement;

            baseUrl = GetXmlValue(docElement, "baseUrl");
            locale = GetXmlValue(docElement, "locale");
            countryCode = GetXmlValue(docElement, "countryCode");

            logoUrl = GetXmlValue(docElement, "logoUrl");
            termsUrl = GetXmlValue(docElement, "termsUrl");
            policyUrl = GetXmlValue(docElement, "policyUrl");
            privacyUrl = GetXmlValue(docElement, "privacyUrl");
            checkoutUrl = GetXmlValue(docElement, "checkoutUrl");
            confirmationUrl = GetXmlValue(docElement, "confirmationUrl");
            pageTitle = GetXmlValue(docElement, "pageTitle");
        }

        private string GetXmlValue(XmlElement xmlElement, string elementName)
        {
            XmlElement element = (XmlElement)xmlElement.SelectSingleNode(elementName);
            if (element != null)
            {
                return element.InnerText;
            }
            return "";
        }

        public void AddOrderLine(string itemNo, string name, string ean, int quantity, string unitOfMeasure, decimal taxRate, decimal unitPrice, decimal lineDiscountAmount, decimal totalAmount, decimal totalTaxAmount, int itemType)
        {
            int taxRateInt = (int)(taxRate * 100);
            int unitPriceInt = (int)(unitPrice * 100);
            int lineDiscountAmountInt = (int)(lineDiscountAmount * 100);
            int totalAmountInt = (int)(totalAmount * 100);
            int totalTaxAmountInt = (int)(totalTaxAmount * 100);

            string itemTypeStr = "physical";
            if (itemType == 1) itemTypeStr = "discount";
            if (itemType == 2) itemTypeStr = "shipping_fee";
            if (itemType == 3) itemTypeStr = "sales_tax";
            if (itemType == 4) itemTypeStr = "digital";
            if (itemType == 5) itemTypeStr = "gift_card";
            if (itemType == 6) itemTypeStr = "store_credit";
            if (itemType == 7) itemTypeStr = "surcharge";            

            orderLines.Add(JObject.FromObject(new
            {
                name = name,
                product_identifiers = new
                {
                    global_trade_item_number = ean
                },
                quantity = quantity,
                quantity_unit = unitOfMeasure,
                reference = itemNo,
                tax_rate = taxRateInt,
                total_amount = totalAmountInt,
                total_discount_amount = lineDiscountAmountInt,
                total_tax_amount = totalTaxAmountInt,
                type = itemTypeStr,
                unit_price = unitPriceInt
            }));

        }
        public string CreateSession(string orderNo, string orderReference, decimal orderAmount, decimal orderTaxAmount, string currencyCode)
        {
            int orderAmountInt = (int)(orderAmount * 100);
            int orderTaxAmountInt = (int)(orderTaxAmount * 100);

            JObject sessionDefinition = JObject.FromObject(new
            {
                acquiring_channel = "in_store",
                locale = locale,
                merchant_reference1 = orderNo,
                merchant_reference2 = orderReference,
                order_amount = orderAmountInt,
                order_lines = orderLines,
                order_tax_amount = orderTaxAmountInt,
                purchase_country = countryCode,
                purchase_currency = currencyCode

            });

            string jsonResult = MakeApiRequest("payments/v1/sessions", "POST", sessionDefinition.ToString());
            JObject resultObject = JObject.Parse(jsonResult);

            string sessionId = resultObject["session_id"].ToString();
            return sessionId.Replace("\"", "");
        }


        public string CreateKCOOrder(string orderNo, string orderReference, decimal orderAmount, decimal orderTaxAmount, string currencyCode)
        {
            int orderAmountInt = (int)(orderAmount * 100);
            int orderTaxAmountInt = (int)(orderTaxAmount * 100);

            JObject sessionDefinition = JObject.FromObject(new
            {
                locale = locale,
                merchant_reference1 = orderNo,
                merchant_reference2 = orderReference,
                order_amount = orderAmountInt,
                order_lines = orderLines,
                order_tax_amount = orderTaxAmountInt,
                purchase_country = countryCode,
                purchase_currency = currencyCode,

                merchant_urls = new
                {
                    terms = termsUrl,
                    checkout = checkoutUrl,
                    confirmation = confirmationUrl,
                    privacy_policy = privacyUrl,
                    push = "https://localhost/kco/push.php?sid={checkout.order.id}"
                }

            });



            string jsonResult = MakeApiRequest("checkout/v3/orders", "POST", sessionDefinition.ToString());
            JObject resultObject = JObject.Parse(jsonResult);

            string sessionId = resultObject["order_id"].ToString();
            return sessionId.Replace("\"", "");
        }

        public string CreateHPPSession(int type, string sessionId)
        {
            string paymentUrl = baseUrl + "payments/v1/sessions/" + sessionId;
            if (type == 1) paymentUrl = baseUrl + "checkout/v3/orders/" + sessionId;

            JObject sessionDefinition = JObject.FromObject(new
            {
                merchant_urls = new {
                    privacy_policy = policyUrl,
                    terms = termsUrl
                },
                options = new
                {
                    logo_url = logoUrl,
                    page_title = pageTitle
                },
                payment_session_url = paymentUrl
            });

            string jsonResult = MakeApiRequest("hpp/v1/sessions", "POST", sessionDefinition.ToString());
            JObject resultObject = JObject.Parse(jsonResult);

            string sessionHppId = resultObject["session_id"].ToString();
            return sessionHppId.Replace("\"", "");
        }

        public string CreateHPPCheckoutSession(string orderId)
        {
            
            string paymentUrl = baseUrl + "checkout/v3/orders/" + orderId;

            JObject sessionDefinition = JObject.FromObject(new
            {
                merchant_urls = new
                {
                    privacy_policy = policyUrl,
                    terms = termsUrl
                },
                options = new
                {
                    logo_url = logoUrl,
                    page_title = pageTitle
                },
                payment_session_url = paymentUrl
            });

            string jsonResult = MakeApiRequest("hpp/v1/sessions", "POST", sessionDefinition.ToString());
            JObject resultObject = JObject.Parse(jsonResult);

            string sessionHppId = resultObject["session_id"].ToString();
            return sessionHppId.Replace("\"", "");
        }

        public void DistributeHPPSessionSMS(string hppSessionId, string phoneNo)
        {

            JObject sessionDefinition = JObject.FromObject(new
            {
                contact_information = new
                {
                    phone = phoneNo,
                    phone_country = countryCode
                },
                method = "sms",
                template = "INSTORE_PURCHASE"
            });

            string jsonResult = MakeApiRequest("hpp/v1/sessions/"+hppSessionId+"/distribution", "POST", sessionDefinition.ToString());
            

        }

        public bool CheckHPPSessionStatus(string hppSessionId, out string authorizationToken)
        {
            authorizationToken = "";
            string jsonResult = MakeApiRequest("hpp/v1/sessions/" + hppSessionId, "GET", "");
            JObject resultObject = JObject.Parse(jsonResult);
            if (resultObject != null)
            {
                string status = resultObject["status"].ToString().Replace("\"", "");
                authorizationToken = resultObject["authorization_token"].ToString().Replace("\"", "");
                if (status == "COMPLETED") return true;
            }
            return false;
        }

        public bool CheckHPPCheckoutStatus(string hppSessionId, out string orderId)
        {
            orderId = "";
            string jsonResult = MakeApiRequest("hpp/v1/sessions/" + hppSessionId, "GET", "");
            JObject resultObject = JObject.Parse(jsonResult);
            if (resultObject != null)
            {
                string status = resultObject["status"].ToString().Replace("\"", "");
                //orderId = resultObject["order_id"].ToString().Replace("\"", "");
                if (status == "COMPLETED") return true;
            }
            return false;
        }
        public bool CreateHPPOrder(string authorizationToken, decimal orderAmount, decimal orderTaxAmount, string currencyCode, out string orderToken)
        {
            int orderAmountInt = (int)(orderAmount * 100);
            int orderTaxAmountInt = (int)(orderTaxAmount * 100);

            JObject orderdefinition = JObject.FromObject(new
            {
                auto_capture = "false",
                locale = locale,
                order_amount = orderAmountInt,
                order_lines = orderLines,
                order_tax_amount = orderTaxAmountInt,
                purchase_country = countryCode,
                purchase_currency = currencyCode

            });


            string jsonResult = MakeApiRequest("payments/v1/authorizations/"+authorizationToken+"/order", "POST", orderdefinition.ToString());
            JObject resultObject = JObject.Parse(jsonResult);

            string status = resultObject["fraud_status"].ToString().Replace("\"", "");
            orderToken = resultObject["order_id"].ToString().Replace("\"", "");
            if (status == "ACCEPTED") return true;

            return false;

        }

        public KlarnaAddress GetOrderAddress(string orderToken)
        {

            string jsonResult = MakeApiRequest("ordermanagement/v1/orders/" + orderToken, "GET", "");
            JObject resultObject = JObject.Parse(jsonResult);

            JObject shippingAddress = resultObject["shipping_address"] as JObject;

            KlarnaAddress address = new KlarnaAddress();
            address.given_name = shippingAddress["given_name"].ToString().Replace("\"", "");
            address.family_name = shippingAddress["family_name"].ToString().Replace("\"", "");
            address.street_address = shippingAddress["street_address"].ToString().Replace("\"", "");
            address.street_address2 = shippingAddress["street_address2"].ToString().Replace("\"", "");
            address.postal_code = shippingAddress["postal_code"].ToString().Replace("\"", "");
            address.city = shippingAddress["city"].ToString().Replace("\"", "");
            address.region = shippingAddress["region"].ToString().Replace("\"", "");
            address.country = shippingAddress["country"].ToString().Replace("\"", "");
            address.phone = shippingAddress["phone"].ToString().Replace("\"", "");
            address.email = shippingAddress["email"].ToString().Replace("\"", "");

            return address;

        }

        public void CaptureDelivery(string orderToken, decimal amount, string description, string reference, int shipmentMethod, string shippingAgent, string trackingNo)
        {
            string shipmentMethodStr = "Home";
            if (shipmentMethod == 1) shipmentMethodStr = "PickUpPoint";
            if (shipmentMethod == 2) shipmentMethodStr = "PickUpStore";

            int amountInt = (int)(amount * 100);

            JObject captureDefinition = JObject.FromObject(new
            {
                captured_amount = amountInt,
                description = description,
                reference = reference,
                order_lines = orderLines,
                shipping_info = new[] { new {
                    shipping_method = shipmentMethodStr,
                    shipping_company = shippingAgent,
                    tracking_number = trackingNo
                } }

            });        

            string jsonResult = MakeApiRequest("ordermanagement/v1/orders/"+orderToken+"/captures", "POST", captureDefinition.ToString());
    

        }


        public void Refund(string orderToken, decimal amount, string description, string reference)
        {


            int amountInt = (int)(amount * 100);

            JObject refundDefinition = JObject.FromObject(new
            {
                refunded_amount = amountInt,
                description = description,
                reference = reference,
                order_lines = orderLines              

            });


            string jsonResult = MakeApiRequest("ordermanagement/v1/orders/" + orderToken + "/refunds", "POST", refundDefinition.ToString());


        }

        private string MakeApiRequest(string method, string verb, string json)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = baseUrl + method;
            System.Net.WebRequest webRequest = System.Net.HttpWebRequest.Create(url);
            webRequest.Credentials = new System.Net.NetworkCredential(username, password);
            webRequest.Method = verb;
            webRequest.ContentType = "application/json";


            Log("Reuqest to "+url, json);

            if (json != "")
            {
                //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                //byte[] bytes = encoding.GetBytes(json);

                //webRequest.ContentLength = bytes.Length;

                //using (System.IO.Stream requestStream = webRequest.GetRequestStream())
                //{
                // Send the data.
                //    requestStream.Write(bytes, 0, bytes.Length);
                //}
                StreamWriter writer = new StreamWriter(webRequest.GetRequestStream());
                writer.WriteLine(json);
                writer.Flush();
                writer.Close();
            }
            string responseJson = "";

            try
            {
                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
                responseJson = streamReader.ReadToEnd();

                Log("Response: ", responseJson);
                return responseJson;

            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized) throw new Exception("Authentication failed.");

                StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
                string jsonResult = reader.ReadToEnd();

                string message = "";
                JObject resultObject = JObject.Parse(jsonResult);
                JArray errorMessages = resultObject["error_messages"] as JArray;
                foreach(var errorMessage in errorMessages)
                {
                    message = message + errorMessage;
                }

                throw new Exception("Felmeddelande från Klarna: " + message);
            }

            
        }

        private void Log(string subject, string logMessage)
        {
            try
            {
                System.IO.StreamWriter w = System.IO.File.AppendText("c:\\temp\\klarna_v3.log");

                w.Write("\r\n"+subject);
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine("\r\n{0}", logMessage);
                w.WriteLine("-------------------------------");

                w.Flush();
                w.Close();
            }
            catch (Exception) { }
        }
    }

    public class KlarnaAddress
    {
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string street_address { get; set; }
        public string street_address2 { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }



}
