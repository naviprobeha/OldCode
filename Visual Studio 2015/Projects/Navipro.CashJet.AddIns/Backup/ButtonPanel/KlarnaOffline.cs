using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.CashJet.AddIns
{
    public class KlarnaOfflineHandler
    {
        private KlarnaTransLineCollection lineCollection;
        private string merchantId;
        private string username;
        private string password;
        private string url;
        

        public KlarnaOfflineHandler(string url, string merchantId, string username, string password)
        {
            lineCollection = new KlarnaTransLineCollection();
            this.merchantId = merchantId;
            this.username = username;
            this.password = password;
            this.url = url;
        }

        public void addTransactionLine(string itemNo, string description, int quantity, int unitPrice, int taxRate)
        {
            KlarnaTransLine line = new KlarnaTransLine();
            line.name = description;
            line.quantity = quantity;
            line.reference = itemNo;
            line.unit_price = unitPrice;
            line.tax_rate = taxRate;
            lineCollection.Add(line);
        }

        public string createTransaction(string posDeviceId, string cellPhoneNo, string transactionNo, string currencyCode, string countryCode, string locale)
        {
            KlarnaTrans transaction = new KlarnaTrans();            
            transaction.terminal_id = posDeviceId;
            transaction.mobile_no = cellPhoneNo;
            transaction.merchant_reference1 = transactionNo;
            transaction.purchase_currency = currencyCode;
            transaction.purchase_country = countryCode;
            transaction.locale = locale;
            transaction.order_lines = lineCollection;

            var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(transaction);

            //throw new Exception("Json: "+json);
            System.Net.WebRequest webRequest = System.Net.HttpWebRequest.Create(url+merchantId+"/orders");
            webRequest.Credentials = new System.Net.NetworkCredential(username, password);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";

            string credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(username+":"+password));
            webRequest.Headers[System.Net.HttpRequestHeader.Authorization] = "Basic " + credentials;

            Log(json, url + merchantId + "/orders", "Basic " + credentials);

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] bytes = encoding.GetBytes(json);

            webRequest.ContentLength = bytes.Length;

            using (System.IO.Stream requestStream = webRequest.GetRequestStream())
            {
                // Send the data.
                requestStream.Write(bytes, 0, bytes.Length);
            }

            try
            {
                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
                string response = streamReader.ReadToEnd();

                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                KlarnaTransResponse transactionResponse = js.Deserialize<KlarnaTransResponse>(response);
                return transactionResponse.status_uri;

            }
            catch(Exception)
            { }

            return "";
        }

        public void cancelTransaction(string transactionNo)
        {
            System.Net.WebRequest webRequest = System.Net.HttpWebRequest.Create(url + merchantId + "/orders/"+transactionNo+"/cancel");
            webRequest.Credentials = new System.Net.NetworkCredential(username, password);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";

            string credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(username + ":" + password));
            webRequest.Headers[System.Net.HttpRequestHeader.Authorization] = "Basic " + credentials;

            Log("", url + merchantId + "/orders/" + transactionNo + "/cancel", "Basic " + credentials);

            try
            {
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();

                System.IO.StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
                string response = streamReader.ReadToEnd();

                //if ((int)webResponse.StatusCode == 204) return true;
            }
            catch (Exception)
            { }


        }

        public KlarnaTransStatus checkTransaction(string transactionUrl)
        {
            System.Net.WebRequest webRequest = System.Net.HttpWebRequest.Create(transactionUrl);
            webRequest.Credentials = new System.Net.NetworkCredential(username, password);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/json";
            webRequest.Timeout = 50000;
           
            string credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(username + ":" + password));
            webRequest.Headers[System.Net.HttpRequestHeader.Authorization] = "Basic " + credentials;

            Log("", transactionUrl, "Basic " + credentials);

            try
            {
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();

                System.IO.StreamReader streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());
                string response = streamReader.ReadToEnd();

                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                KlarnaTransStatus transactionStatus = js.Deserialize<KlarnaTransStatus>(response);

                return transactionStatus;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public bool refundTransaction(string merchantId, string invoiceId, string posDeviceId, string description, decimal amount, decimal taxRate)
        {
            KlarnaRefund refund = new KlarnaRefund();
            refund.terminal_id = posDeviceId;
            refund.refunded_amount = (int)(amount*100);
            refund.description = description;
            refund.tax_rate = (int)(taxRate*100);

            var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(refund);

            System.Net.WebRequest webRequest = System.Net.HttpWebRequest.Create(url + merchantId + "/invoices/" + invoiceId + "/refund");
            webRequest.Credentials = new System.Net.NetworkCredential(username, password);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";

            string credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(username + ":" + password));
            webRequest.Headers[System.Net.HttpRequestHeader.Authorization] = "Basic " + credentials;

            Log(json, url + merchantId + "/invoices/" + invoiceId + "/refund", "Basic " + credentials);

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] bytes = encoding.GetBytes(json);

            webRequest.ContentLength = bytes.Length;

            using (System.IO.Stream requestStream = webRequest.GetRequestStream())
            {
                // Send the data.
                requestStream.Write(bytes, 0, bytes.Length);
            }

            System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();

            if ((int)webResponse.StatusCode == 200) return true;

            return false;

        }

        private void Log(string logMessage, string url, string authentication)
        {
            try
            {
                System.IO.StreamWriter w = System.IO.File.AppendText("c:\\temp\\klarna.log");

                w.Write("\r\nRequest to url {0}: ", url);
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine("Authentication : {0}", authentication);
                w.WriteLine("\r\n{0}", logMessage);
                w.WriteLine("-------------------------------");

                w.Flush();
                w.Close();
            }
            catch (Exception) { }
        }


    }

    public class KlarnaRefund
    {
        public string terminal_id { get; set; }
        public int refunded_amount { get; set; }
        public int tax_rate { get; set; }
        public string description { get; set; }
    }

    public class KlarnaTrans
    {
        public string terminal_id { get; set; }
        public string mobile_no { get; set; }
        public string merchant_reference1 { get; set; }
        public string purchase_currency { get; set; }
        public string purchase_country { get; set; }
        public string locale { get; set; }
        public KlarnaTransLineCollection order_lines { get; set; }
    }

    public class KlarnaTransLine
    {
        public int unit_price { get; set; }
        public int quantity { get; set; }
        public string reference { get; set; }
        public int tax_rate { get; set; }
        public string name { get; set; }

    }

    public class KlarnaTransStatus
    {
        public string id { get; set; }
        public string message_code { get; set; }
        public string message { get; set; }
        public string invoice_id { get; set; }
        public KlarnaCustomer customer { get; set; }
    }

    public class KlarnaTransResponse
    {
        public string id { get; set; }
        public string status_uri { get; set; }
    }

    public class KlarnaCustomer
    {
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string street_address { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }

    public class KlarnaTransLineCollection : CollectionBase
    {
        public KlarnaTransLine this[int index]
        {
            get { return (KlarnaTransLine)List[index]; }
            set { List[index] = value; }
        }
        public int Add(KlarnaTransLine klarnaTransLine)
        {
            return (List.Add(klarnaTransLine));
        }
        public int IndexOf(KlarnaTransLine klarnaTransLine)
        {
            return (List.IndexOf(klarnaTransLine));
        }
        public void Insert(int index, KlarnaTransLine klarnaTransLine)
        {
            List.Insert(index, klarnaTransLine);
        }
        public void Remove(KlarnaTransLine klarnaTransLine)
        {
            List.Remove(klarnaTransLine);
        }
        public bool Contains(KlarnaTransLine klarnaTransLine)
        {
            return (List.Contains(klarnaTransLine ));
        }


    }


}
