namespace Swedbank.Pay.Api.Services
{
    using System;
    using System.IO;
    using System.Net;
    using Models.Capture;
    using Models.PaymentResource;
    using Models.Transaction;
    using Newtonsoft.Json;

    public class SwedbankService : ISwedbankService
    {
        public string ServiceUrl { get; set; }
        public string ServiceToken { get; set; }
        public SwedbankService(string serviceUrl, string serviceToken)
        {
            ServiceUrl = serviceUrl;
            ServiceToken = serviceToken;
        }


        public PaymentResource GetPaymentResource(string id)
        {
            var url = this.ServiceUrl;
            url += $"/psp/card/payments/{id}/";

            var responseData = this.DoRequest(url, "GET", string.Empty);
            if (!string.IsNullOrEmpty(responseData))
            {
                var response = JsonConvert.DeserializeObject<PaymentResource>(responseData);
                return response;
            }

            return null;
        }

        public CaptureResponse Capture(CaptureRequest request)
        {
            string url = this.ServiceUrl;
            url += $"/psp/creditcard/payments/{request.OrderId}/captures";
            var payload = JsonConvert.SerializeObject(request);

            var responseData = this.DoRequest(url, "POST", payload);
            if (!string.IsNullOrEmpty(responseData))
            {
                var response = JsonConvert.DeserializeObject<CaptureResponse>(responseData);
                return response;
            }

            return null;
        }

        public Transaction GetTransactions(string id)
        {
            string url = this.ServiceUrl;
            url += $"/psp/creditcard/payments/{id}/transactions/";

            var responseData = this.DoRequest(url, "GET", string.Empty);
            if (!string.IsNullOrEmpty(responseData))
            {
                var response = JsonConvert.DeserializeObject<Transaction>(responseData);
                return response;
            }

            return null;
        }

        private string DoRequest(string url, string method, string payload)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "application/json";
            webRequest.Method = method;
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + this.ServiceToken);

            if (!string.IsNullOrEmpty(payload))
            {
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            StreamWriter logger = new StreamWriter("C:\\temp\\swedbank.log");
            logger.WriteLine(payload);
            logger.Flush();
            logger.Close();

            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                if (webResponse.StatusCode == HttpStatusCode.OK || webResponse.StatusCode == HttpStatusCode.Accepted || webResponse.StatusCode == HttpStatusCode.Created)
                {
                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream != null)
                    {
                        using (var streamReader = new StreamReader(responseStream))
                        {
                            var responseText = streamReader.ReadToEnd();
                            return responseText;
                        }
                    }
                }
                else
                {
                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream != null)
                    {
                        using (var streamReader = new StreamReader(responseStream))
                        {
                            var responseText = streamReader.ReadToEnd();
                            // TODO: Add logging
                            throw new Exception(responseText);
                        }
                    }
                }
            }
            catch (WebException exception)
            {
                // TODO: Add logging
                //this.log.Error(exception);
                using (var stream = exception.Response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            // TODO: Add logging
                            //this.log.Error(reader.ReadToEnd());
                            throw new Exception(reader.ReadToEnd());
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                // TODO: Add logging
                //this.log.Error(exception);
                throw new Exception(exception.Message);
            }

            return null;
        }
    }
}
