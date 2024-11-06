using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using ResursWrapper;

namespace ResursBankWrapper
{
    //[ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("E3AD5E7D-CA10-45CD-8DBF-E523E7329CCD")]
    public interface ISoap
    {
        /// <summary>
        /// Finalizes a payment. When a payment is finalized, the amount will be transferred from the customer's account to that of the representative. NB: For a payment to be finalized, it must be booked and it cannot be frozen.
        /// </summary>
        /// <param name="userName">Required</param>
        /// <param name="password">Required</param>
        /// <param name="paymentId">Required. The identity of the payment</param>
        /// <param name="partPaymentSpec">Required. No row information needs to be supplied, only the amount. If row information is not supplied, then the order data specification will not be shown in the PDF invoice document</param>      
        /// <param name="serviceUrl">Url to API</param>
        /// <param name="preferredTransactionId">Will be printed on the accouting summary. Can be used to track the transaction. If not set it will fallback on paymentId for this value</param>
        /// <param name="orderId">The order number</param>
        /// <param name="createdBy">The username of the person performing the operation</param>
        /// <param name="invoiceDeliveryType">How the invoice should be delivered default: EMAIL. [POSTAL,EMAIL, NONE]</param>
        /// <param name="errorMessage">Ref string to get the error message back to NAV</param>
        [DispId(1)]
        bool FinalizePayment(string userName, string password, string serviceUrl, string paymentId, PartPaymentSpec partPaymentSpec, string preferredTransactionId, string orderId, string createdBy, string invoiceDeliveryType, ref string errorMessage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName">Required</param>
        /// <param name="password">Required</param>
        /// <param name="serviceUrl"></param>
        /// <param name="paymentId">Required. The identity of the payment</param>
        /// <param name="createdBy">The username of the person performing the operation</param>
        /// <param name="errorMessage">Ref string to get the error message back to NAV</param>
        /// <returns></returns>
        [DispId(2)]
        bool AnnulPayment(string userName, string password, string serviceUrl, string paymentId, string createdBy, ref string errorMessage);
    }

    //[ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("E292B800-3926-4692-9500-2710D4E034F4")]
    [ProgId("ResursWrapper.Soap")]
    public class Soap : ISoap
    {
        public Soap()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Finalizes a payment. When a payment is finalized, the amount will be transferred from the customer's account to that of the representative. NB: For a payment to be finalized, it must be booked and it cannot be frozen.
        /// </summary>
        /// <param name="userName">Required</param>
        /// <param name="password">Required</param>
        /// <param name="paymentId">Required. The identity of the payment</param>
        /// <param name="partPaymentSpec">Required. No row information needs to be supplied, only the amount. If row information is not supplied, then the order data specification will not be shown in the PDF invoice document</param>      
        /// <param name="serviceUrl">Url to API</param>
        /// <param name="preferredTransactionId">Will be printed on the accouting summary. Can be used to track the transaction. If not set it will fallback on paymentId for this value</param>
        /// <param name="orderId">The order number</param>
        /// <param name="createdBy">The username of the person performing the operation</param>
        /// <param name="invoiceDeliveryType">How the invoice should be delivered default: EMAIL. [POSTAL,EMAIL, NONE]</param>
        /// <param name="errorMessage">Ref string to get the error message back to NAV</param>       
        public bool FinalizePayment(string userName, string password, string serviceUrl, string paymentId, PartPaymentSpec partPaymentSpec, string preferredTransactionId, string orderId, string createdBy, string invoiceDeliveryType, ref string errorMessage)
        {
            try
            {
                if (String.IsNullOrEmpty(userName)) throw new Exception("userName is empty!");
                if (String.IsNullOrEmpty(password)) throw new Exception("password is empty!");

                if (String.IsNullOrEmpty(paymentId)) throw new Exception("paymentId is empty!");
                if (partPaymentSpec.TotalAmount == "0") throw new Exception("partPaymentSpec TotalAmount is 0 ! ");

                if (String.IsNullOrEmpty(serviceUrl)) serviceUrl = "https://test.resurs.com/ecommerce-test/ws/V4/AfterShopFlowService";

                XmlDocument soapEnvelopeXml = CreateFinalizePaymentEnvelope(paymentId, partPaymentSpec, preferredTransactionId, orderId, createdBy, invoiceDeliveryType);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    soapEnvelopeXml.Save(errorMessage);
                    errorMessage = string.Empty;
                }

                HttpWebRequest webRequest = CreateWebRequest(serviceUrl, "finalizePayment");

                webRequest.Credentials = new NetworkCredential(userName, password);

                InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                WebResponse webResponse = webRequest.GetResponse();
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    var soapResult = rd.ReadToEnd();
                    var soapXml = new XmlDocument();
                    soapXml.LoadXml(soapResult);

                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapXml.NameTable);
                    nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    nsmgr.AddNamespace("ns2", "http://ecommerce.resurs.com/v4/msg/exception");
                    nsmgr.AddNamespace("ns3", "http://ecommerce.resurs.com/v4/msg/shopflow");

                    XmlNode responseTextNode = soapXml.SelectSingleNode("soap:Envelope/soap:Body/soap:Fault/details/ns2:ECommerceError/userErrorMessage", nsmgr);
                    if (responseTextNode != null)
                        errorMessage = responseTextNode.InnerText;

                    XmlNode resultCodeNode = soapXml.SelectSingleNode("soap:Envelope/soap:Body/soap:Fault/faultstring", nsmgr);
                    if (resultCodeNode != null)
                        return (resultCodeNode.InnerText == "101");
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName">Required</param>
        /// <param name="password">Required</param>
        /// <param name="serviceUrl"></param>
        /// <param name="paymentId">Required. The identity of the payment</param>
        /// <param name="createdBy">The username of the person performing the operation</param>
        /// <param name="errorMessage">Ref string to get the error message back to NAV</param>
        /// <returns></returns>       
        public bool AnnulPayment(string userName, string password, string serviceUrl, string paymentId, string createdBy, ref string errorMessage)
        {
            try
            {
                if (String.IsNullOrEmpty(userName)) throw new Exception("userName is empty!");
                if (String.IsNullOrEmpty(password)) throw new Exception("password is empty!");
                if (String.IsNullOrEmpty(paymentId)) throw new Exception("paymentId  is empty!");

                if (String.IsNullOrEmpty(serviceUrl)) serviceUrl = "https://test.resurs.com/ecommerce-test/ws/V4/AfterShopFlowService";


                XmlDocument soapEnvelopeXml = CreateAnnulPaymentEnvelope(paymentId, createdBy);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    soapEnvelopeXml.Save(errorMessage);
                    errorMessage = string.Empty;
                }

                HttpWebRequest webRequest = CreateWebRequest(serviceUrl, "annulPayment");

                webRequest.Credentials = new NetworkCredential(userName, password);

                InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                WebResponse webResponse = webRequest.GetResponse();
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    var soapResult = rd.ReadToEnd();
                    var soapXml = new XmlDocument();
                    soapXml.LoadXml(soapResult);

                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(soapXml.NameTable);
                    nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    nsmgr.AddNamespace("ns2", "http://ecommerce.resurs.com/v4/msg/exception");
                    nsmgr.AddNamespace("ns3", "http://ecommerce.resurs.com/v4/msg/shopflow");

                    XmlNode responseTextNode = soapXml.SelectSingleNode("soap:Envelope/soap:Body/soap:Fault/details/ns2:ECommerceError/userErrorMessage", nsmgr);
                    if (responseTextNode != null)
                        errorMessage = responseTextNode.InnerText;

                    XmlNode resultCodeNode = soapXml.SelectSingleNode("soap:Envelope/soap:Body/soap:Fault/faultstring", nsmgr);
                    if (resultCodeNode != null)
                        return (resultCodeNode.InnerText == "101");
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        private void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        private HttpWebRequest CreateWebRequest(string url, string action)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentId">Kan vara OrderId eller äkta paymentId</param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        private XmlDocument CreateAnnulPaymentEnvelope(string paymentId, string createdBy)
        {
            var sb = new StringBuilder();
            sb.Append(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:aft=""http://ecommerce.resurs.com/v4/msg/aftershopflow"">");
            sb.Append("<soapenv:Header/><soapenv:Body>");
            sb.Append("<aft:annulPayment>");

            sb.Append("<paymentId>" + paymentId + "</paymentId>");
            sb.Append("<createdBy>" + createdBy + "</createdBy>");

            sb.Append("</aft:annulPayment></soapenv:Body></soapenv:Envelope>");

            var soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(sb.ToString());

            return soapEnvelop;
        }

        private XmlDocument CreateFinalizePaymentEnvelope(string paymentId, PartPaymentSpec partPaymentSpec, string preferredTransactionId, string orderId, string createdBy, string invoiceDeliveryType)
        {
            var sb = new StringBuilder();
            sb.Append(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:aft=""http://ecommerce.resurs.com/v4/msg/aftershopflow"">");
            sb.Append("<soapenv:Header/><soapenv:Body>");
            sb.Append("<aft:finalizePayment>");

            sb.Append("<paymentId>" + paymentId + "</paymentId>");

            if (!string.IsNullOrEmpty(preferredTransactionId))
                sb.Append("<preferredTransactionId>" + preferredTransactionId + "</preferredTransactionId>");

            sb.Append("<partPaymentSpec>");

            if (partPaymentSpec.SpecLines.Count > 0)
            {
                for (int i = 0; i < partPaymentSpec.SpecLines.Count; i++)
                {
                    sb.Append("<specLines>");
                    sb.Append("<id>" + i.ToString(CultureInfo.InvariantCulture) + 1 + "</id>");
                    sb.Append("<artNo>" + partPaymentSpec.SpecLines[i].ArtNo + "</artNo>");
                    sb.Append("<description>" + partPaymentSpec.SpecLines[i].Description + "</description>");
                    sb.Append("<quantity>" + partPaymentSpec.SpecLines[i].Quantity + "</quantity>");
                    sb.Append("<unitMeasure>" + partPaymentSpec.SpecLines[i].UnitMeasure + "</unitMeasure>");
                    sb.Append("<unitAmountWithoutVat>" + partPaymentSpec.SpecLines[i].UnitAmountWithoutVat + "</unitAmountWithoutVat>");
                    sb.Append("<vatPct>" + partPaymentSpec.SpecLines[i].VatPct + "</vatPct>");
                    sb.Append("<totalVatAmount>" + partPaymentSpec.SpecLines[i].TotalVatAmount + "</totalVatAmount>");
                    sb.Append("<totalAmount>" + partPaymentSpec.SpecLines[i].TotalAmount + "</totalAmount>");
                    sb.Append("</specLines>");
                }
            }

            sb.Append("<totalAmount>" + partPaymentSpec.TotalAmount + "</totalAmount>");
            sb.Append("<totalVatAmount>" + partPaymentSpec.TotalVatAmount + "</totalVatAmount>");
            sb.Append("<bonusPoints>" + 0 + "</bonusPoints>");

            sb.Append("</partPaymentSpec>");


            if (!string.IsNullOrEmpty(createdBy))
                sb.Append("<createdBy>" + createdBy + "</createdBy>");

            sb.Append("</aft:finalizePayment></soapenv:Body></soapenv:Envelope>");

            var soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(sb.ToString());

            return soapEnvelop;
        }

    }
}
