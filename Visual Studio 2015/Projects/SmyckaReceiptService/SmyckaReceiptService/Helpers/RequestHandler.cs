using System;
using System.Net;
using System.Xml;
using System.Collections.Generic;
using SmyckaReceiptService.Models;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace SmyckaReceiptService.Handlers
{
    public class RequestHandler
    {
        public RequestHandler()
        {
        }

        public static POSTransactionHeader GetReceipt(Configuration configuration, string receiptId)
        {
            string response = PerformService(configuration, "getXMLReceipt", receiptId, 30, false);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);
            XmlElement docElement = xmlDoc.DocumentElement;

            if (docElement != null)
            {
                POSTransactionHeader posTransHeader = new POSTransactionHeader();

                XmlElement receiptElement = (XmlElement)docElement.SelectSingleNode("data/Receipt");
                if (receiptElement != null)
                {
                    XmlElement headerElement = (XmlElement)receiptElement.SelectSingleNode("Header");

                    posTransHeader.newCustomer = (XMLHelper.GetFieldText(headerElement, "newCustomer") == "1");

                    posTransHeader.no = XMLHelper.GetFieldText(headerElement, "transactionNo");
                    posTransHeader.registeredTransactionNo = XMLHelper.GetFieldText(headerElement, "registeredReceiptNo");
                    posTransHeader.salesPersonName = XMLHelper.GetFieldText(headerElement, "salesPersonName");
                    posTransHeader.registeredDateTime = DateTime.Parse(XMLHelper.GetFieldText(headerElement, "registeredDateTime"));
                    posTransHeader.unitId = XMLHelper.GetFieldText(headerElement, "controlUnitId");
                    posTransHeader.posDeviceID = XMLHelper.GetFieldText(headerElement, "posDeviceId");

                    posTransHeader.store = new Store();
                    posTransHeader.store.name = XMLHelper.GetFieldText(headerElement, "storeName");
                    posTransHeader.store.address = XMLHelper.GetFieldText(headerElement, "storeAddress");
                    posTransHeader.store.postalAddress = XMLHelper.GetFieldText(headerElement, "storePostalAddress");
                    posTransHeader.store.registrationNo = XMLHelper.GetFieldText(headerElement, "registrationNo");
                    posTransHeader.store.vatRegistrationNo = XMLHelper.GetFieldText(headerElement, "vatRegistrationNo");
                    posTransHeader.store.phoneNo = XMLHelper.GetFieldText(headerElement, "phoneNo");
                    posTransHeader.store.email = XMLHelper.GetFieldText(headerElement, "email");
                    posTransHeader.store.homePage = XMLHelper.GetFieldText(headerElement, "homePage");

                    posTransHeader.lines = new List<POSTransactionLine>();

                    XmlNodeList linesList = receiptElement.SelectNodes("Lines/Line");

                    foreach(XmlElement lineElement in linesList)
                    {
                        POSTransactionLine line = new POSTransactionLine();
                        line.lineType = XMLHelper.GetFieldInt(lineElement, "lineType");
                        line.salesType = XMLHelper.GetFieldInt(lineElement, "salesType");
                        line.salesNo = XMLHelper.GetFieldText(lineElement, "no");
                        line.variantCode = XMLHelper.GetFieldText(lineElement, "variantCode");
                        line.description = XMLHelper.GetFieldText(lineElement, "description");
                        line.description2 = XMLHelper.GetFieldText(lineElement, "description2");
                        line.quantity = Decimal.Parse(XMLHelper.GetFieldText(lineElement, "quantity"));
                        line.unitPriceInclVAT = Decimal.Parse(XMLHelper.GetFieldText(lineElement, "unitPrice"), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture);
                        line.presentationAmountInclVAT = Decimal.Parse(XMLHelper.GetFieldText(lineElement, "presentationAmount"), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture);
                        line.returnReasonCode = XMLHelper.GetFieldText(lineElement, "returnReasonCode");
                        line.returnReasonDescription = XMLHelper.GetFieldText(lineElement, "returnReasonDescription");
                        line.discountText = XMLHelper.GetFieldText(lineElement, "discount");
                        line.vatProc = Decimal.Parse(XMLHelper.GetFieldText(lineElement, "vatProc"), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture);
                        line.vatAmount = Decimal.Parse(XMLHelper.GetFieldText(lineElement, "vatAmount"), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture);

                        line.amountInclVAT = line.presentationAmountInclVAT;
                        line.amount = line.amountInclVAT - line.vatAmount;
                        

                        //if (line.lineType == 0) line.salesType = 2;
                        posTransHeader.lines.Add(line);
                    }



                    XmlElement commentsElement = (XmlElement)receiptElement.SelectSingleNode("Comments");

                    posTransHeader.store.receiptTextLine1 = XMLHelper.GetFieldText(commentsElement, "Comment1");
                    posTransHeader.store.receiptTextLine2 = XMLHelper.GetFieldText(commentsElement, "Comment2");
                    posTransHeader.store.receiptTextLine3 = XMLHelper.GetFieldText(commentsElement, "Comment3");
                    posTransHeader.store.receiptTextLine4 = XMLHelper.GetFieldText(commentsElement, "Comment4");
                    posTransHeader.store.receiptTextLine5 = XMLHelper.GetFieldText(commentsElement, "Comment5");
                    posTransHeader.store.receiptTextLine6 = XMLHelper.GetFieldText(commentsElement, "Comment6");
                    posTransHeader.store.receiptTextLine7 = XMLHelper.GetFieldText(commentsElement, "Comment7");
                    posTransHeader.store.receiptTextLine8 = XMLHelper.GetFieldText(commentsElement, "Comment8");
                    posTransHeader.store.receiptTextLine9 = XMLHelper.GetFieldText(commentsElement, "Comment9");
                    posTransHeader.store.receiptTextLine10 = XMLHelper.GetFieldText(commentsElement, "Comment10");





                    string base64 = receiptElement.GetAttribute("LoggoAsBase64");

                    posTransHeader.store.logo = Convert.FromBase64String(base64);


                    SmyckaReceiptService.Reports.ReceiptReport report = new SmyckaReceiptService.Reports.ReceiptReport();


                    var image = report.createLayout(posTransHeader, posTransHeader.store, "");

                    image.Save("C:\\temp\\receipts\\" + receiptId + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    return posTransHeader;
                }
            }

            return null;
        }

        public static void CreateCustomer(Configuration configuration, string receiptId, string name, string phoneNo, string email)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<customer/>");
            XmlElement docElement = xmlDoc.DocumentElement;
            XmlElement xmlElement = null;

            XMLHelper.AddElement(ref docElement, "name", name, "", ref xmlElement);
            XMLHelper.AddElement(ref docElement, "phoneNo", phoneNo, "", ref xmlElement);
            XMLHelper.AddElement(ref docElement, "email", email, "", ref xmlElement);

            docElement.SetAttribute("uniqueId", receiptId);

            string response = PerformService(configuration, "createCustomer", docElement.OuterXml, 30, false);

        }


        public static string PerformService(Configuration configuration, string method, string xmlData, int timeout, bool ignoreErrors)
        {
            string url = "https://" + configuration.serverName + ":" + configuration.port + "/" + configuration.serverInstance + "/WS/" + configuration.companyName + "/Codeunit/POSServerWebService"; ;
            string username = configuration.userName;
            string password = configuration.password;

            ServicePointManager.ServerCertificateValidationCallback = delegate (
                      Object obj, X509Certificate certificate, X509Chain chain,
                      SslPolicyErrors errors)
            {
                return (true);
            };


            HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(url);
            NetworkCredential netCredentials = new NetworkCredential(username, password);
            httpWebRequest.Credentials = netCredentials;
            httpWebRequest.ContentType = "text/xml; charset=utf-8";
            httpWebRequest.Method = "POST";


            httpWebRequest.Timeout = timeout * 1000;

            WebHeaderCollection webHeaderCollection = httpWebRequest.Headers;
            webHeaderCollection.Add("SOAPAction", "performService");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement bodyElement = null;
            XmlElement methodElement = null;
            XmlElement element = null;

            XMLHelper.AddElement(ref docElement, "soap:Body", "", "http://schemas.xmlsoap.org/soap/envelope/", ref bodyElement);
            XMLHelper.AddElement(ref bodyElement, "PerformService", "", "urn:microsoft-dynamics-schemas/codeunit/POSServerWebService", ref methodElement);
            XMLHelper.AddElement(ref methodElement, "method", method, "urn:microsoft-dynamics-schemas/codeunit/POSServerWebService", ref element);
            XMLHelper.AddElement(ref methodElement, "xmlRecordData", xmlData, "urn:microsoft-dynamics-schemas/codeunit/POSServerWebService", ref element);




            try
            {

                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(httpWebRequest.GetRequestStream());
                streamWriter.Write(xmlDoc.OuterXml);
                streamWriter.Close();


                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                System.IO.StreamReader streamReader = new System.IO.StreamReader(httpWebResponse.GetResponseStream());

                string responseString = streamReader.ReadToEnd();

                XmlDocument responseSOAPDoc = new XmlDocument();
                responseSOAPDoc.LoadXml(responseString);

                httpWebResponse.Close();



                XmlNodeList nodeList = responseSOAPDoc.GetElementsByTagName("PerformService_Result");
                if (nodeList.Count > 0)
                {

                    element = (XmlElement)nodeList.Item(0);


                    if (element != null)
                    {

                        XmlDocument responseDoc = new XmlDocument();
                        responseDoc.LoadXml(element.InnerText);
                        docElement = responseDoc.DocumentElement;

                        if (docElement.Name == "errorMessage") throw new Exception("NAV-Error: " + docElement.InnerText);

                        return docElement.OuterXml;

                    }

                }

            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    using (var stream = webException.Response.GetResponseStream())
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        if (!ignoreErrors)
                        {

                            //XmlDocument errorDocument = new XmlDocument();
                            //errorDocument.LoadXml(reader.ReadToEnd());
                            //XmlNodeList nodeList = errorDocument.GetElementsByTagName("faultstring");
                            //if (nodeList.Count > 0)
                            //{
                                //XmlElement errorMessage = (XmlElement)nodeList[0];

                                throw new Exception("RequestHandler Error: (" + configuration.serverName + ", "+username+", "+password+"): First:" + webException.Message+". Second:"+reader.ReadToEnd());

                            //}

                        }
                    }
                }
                else
                {
                    if (!ignoreErrors)
                    {
                        throw new Exception("RequestHandler Error: (" + configuration.serverName + "): Network error");
                    }

                }
            }
            catch (Exception exception)
            {
                if (!ignoreErrors)
                {
                    throw new Exception("RequestHandler Error: (" + configuration.serverName + "): " + exception.Message);
                }
            }

            return "";
        }



    }
}
