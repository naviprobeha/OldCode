using Api.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace Api.Library.ShippingAgents
{
    public class UPSHelper
    {
        private string fromCity;
        private string fromPostCode;
        private string fromCountryCode;
        private string shipperNumber;
        private decimal schablon = 0;
        private int quantityInCarton = 0;
        private decimal factor = 0.1m;
        private string accessLicenseNo;
        private string userId;
        private string password;

        public UPSHelper()
        {
            fromCity = "Borås";
            fromPostCode = "50335";
            fromCountryCode = "SE";
            schablon = 1;
            quantityInCarton = 16;

            shipperNumber = "1w0219";

            accessLicenseNo = "5C3EF30EA2C43544";
            userId = "sandbergab";
            password = "1W0219ups";
        }

        public List<ShippingAgentService> freightRequest(OrderHistory orderHeader)
        {
            decimal grossWeight = 0;
            int noOfPackages = 0;

            XmlDocument titDoc = createTITRequest(orderHeader, ref grossWeight, ref noOfPackages);
            
            XmlDocument serviceListDoc = makeTITRequest(titDoc);

            List<ShippingAgentService> serviceList = parseTITServiceDoc(serviceListDoc);


            List<ShippingAgentService> fullList = ShippingAgentHelper.GetServices();

            bool serviceExists = false;
            int i = 0;
            while (i < serviceList.Count)
            {
                ShippingAgentService service = serviceList[i];

                XmlDocument rssDoc = createRSSRequest(orderHeader, service, noOfPackages, Decimal.Divide(grossWeight, (decimal)noOfPackages));
                XmlDocument serviceFeeDoc = makeRSSRequest(rssDoc, i+1);

                if (parseRSSServiceDoc(serviceFeeDoc, ref service) == true) serviceExists = true;

                foreach(ShippingAgentService fullService in fullList)
                {
                    if (fullService.external_service_code.Contains(service.external_service_code))
                    {
                        service.service = fullService.service;
                        service.code = fullService.code;
                    }
                }

                serviceList[i] = service;
                i++;
                
            }

            //Remove all Standard-services for GB
            if (orderHeader.ship_to_country_code == "GB")
            {
                List<ShippingAgentService> serviceList2 = new List<ShippingAgentService>();
                foreach (ShippingAgentService service in serviceList)
                {
                    if (service.code != "ST") serviceList2.Add(service);
                }
                return serviceList2;
            }

            if (!serviceExists) return new List<ShippingAgentService>();

            return serviceList;
        }


        public List<ShippingAgentService> freightRequestJson(OrderHistory orderHeader)
        {

            decimal grossWeight = 0;
            decimal totalQty = 0;
            foreach (OrderHistoryLine line in orderHeader.lines)
            {
                if (line.type == 2)
                {
                    Item item = ItemHelper.GetItem(orderHeader.customer_no, line.no);
                    if (item != null)
                    {
                        if (item.gross_weight == 0) item.gross_weight = schablon;
                        grossWeight += (item.gross_weight * line.quantity);
                    }
                    totalQty += line.quantity;
                }
            }
            if (grossWeight < schablon) grossWeight = schablon;



            JObject jsonObject = createRateRequestJson(orderHeader);


        
            string jsonResponse = makeRateRequest(jsonObject.ToString());

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonResponse);
            xmlDoc.Save("C:\\temp\\jsonresponse.xml");
                      

            List<ShippingAgentService> serviceList = parseServicesJson(xmlDoc);

            //Remove all Standard-services for GB
            if (orderHeader.ship_to_country_code == "GB")
            {
                List<ShippingAgentService> serviceList2 = new List<ShippingAgentService>();
                foreach (ShippingAgentService service in serviceList)
                {
                    if (service.code != "ST") serviceList2.Add(service);
                }
                return serviceList2;
            }


            return serviceList;
        }

        private XmlDocument createAccessHeader()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"WINDOWS-1252\" ?><AccessRequest/>");

            XmlElement docElement = xmlDoc.DocumentElement;
            docElement.SetAttribute("xml:lang", "en-US");

            AddElement(docElement, "AccessLicenseNumber", accessLicenseNo);
            AddElement(docElement, "UserId", userId);
            AddElement(docElement, "Password", password);

            return xmlDoc;
        }

        private XmlDocument createTITRequest(OrderHistory orderHistory, ref decimal grossWeight, ref int noOfPackages)
        {
            if ((orderHistory.currency_code == "") || (orderHistory.currency_code == null)) orderHistory.currency_code = "SEK";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"WINDOWS-1252\" ?><TimeInTransitRequest/>");

            XmlElement docElement = xmlDoc.DocumentElement;
            docElement.SetAttribute("xml:lang", "en-US");

            XmlElement requestElement = AddElement(docElement, "Request", "");

            XmlElement refElement = AddElement(requestElement, "TransactionReference", "");
            AddElement(refElement, "CustomerContext", "TNT_D Origin Country code");
            AddElement(refElement, "XpciVersion", "1.0002");

            AddElement(requestElement, "RequestAction", "TimeInTransit");



            XmlElement transitFromElement = AddElement(docElement, "TransitFrom", "");

            XmlElement addressElement = AddElement(transitFromElement, "AddressArtifactFormat", "");
            AddElement(addressElement, "PoliticalDivision2", fromCity);
            AddElement(addressElement, "CountryCode", fromCountryCode);
            AddElement(addressElement, "PostcodePrimaryLow", fromPostCode);

            XmlElement transitToElement = AddElement(docElement, "TransitTo", "");

            addressElement = AddElement(transitToElement, "AddressArtifactFormat", "");
            AddElement(addressElement, "PoliticalDivision2", orderHistory.ship_to_city);
            AddElement(addressElement, "CountryCode", orderHistory.ship_to_country_code);
            AddElement(addressElement, "PostcodePrimaryLow", orderHistory.ship_to_post_code);


            grossWeight = 0;
            decimal totalQty = 0;
            foreach(OrderHistoryLine line in orderHistory.lines)
            {
                if (line.type == 2)
                {
                    Item item = ItemHelper.GetItem(orderHistory.customer_no, line.no);
                    if (item != null)
                    {
                        grossWeight += item.gross_weight;
                    }
                    totalQty += line.quantity;
                }
            }
            if (grossWeight < schablon) grossWeight = schablon;

            XmlElement weightElement = AddElement(docElement, "ShipmentWeight", "");

            XmlElement uomElement = AddElement(weightElement, "UnitOfMeasurement", "");
            AddElement(uomElement, "Code", "KGS");
            AddElement(uomElement, "Description", "Kilograms");

            AddElement(weightElement, "Weight", grossWeight.ToString("N2", CultureInfo.InvariantCulture.NumberFormat));


            noOfPackages = (int)totalQty / quantityInCarton;
            if (((int)totalQty % quantityInCarton) > 0) noOfPackages++;

            AddElement(docElement, "TotalPackagesInShipment", noOfPackages.ToString());


            XmlElement invoiceLineElement = AddElement(docElement, "InvoiceLineTotal", "");

            AddElement(invoiceLineElement, "CurrencyCode", orderHistory.currency_code);
            AddElement(invoiceLineElement, "MonetaryValue", "1.00");

            AddElement(docElement, "PickupDate", orderHistory.order_date.ToString("yyyyMMdd"));




            return xmlDoc;
                    

        }

        private XmlDocument createRSSRequest(OrderHistory orderHistory, ShippingAgentService shippingAgentService, int noOfPackages, decimal grossWeight)
        {
            if ((orderHistory.currency_code == "") || (orderHistory.currency_code == null)) orderHistory.currency_code = "SEK";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"WINDOWS-1252\" ?><RatingServiceSelectionRequest/>");

            XmlElement docElement = xmlDoc.DocumentElement;
            docElement.SetAttribute("xml:lang", "en-US");

            XmlElement requestElement = AddElement(docElement, "Request", "");

            XmlElement refElement = AddElement(requestElement, "TransactionReference", "");
            AddElement(refElement, "CustomerContext", "Bare Bones Rate Request");
            AddElement(refElement, "XpciVersion", "1.0001");

            AddElement(requestElement, "RequestAction", "Rate");
            AddElement(requestElement, "RequestOption", "Rate");


            XmlElement pickUpTypeElement = AddElement(docElement, "PickupType", "");
            AddElement(pickUpTypeElement, "Code", "01");


            XmlElement shipmentElement = AddElement(docElement, "Shipment", "");

            XmlElement shipperElement = AddElement(shipmentElement, "Shipper", "");
            AddElement(shipperElement, "ShipperNumber", shipperNumber);
            XmlElement addressElement = AddElement(shipperElement, "Address", "");
            AddElement(addressElement, "PostalCode", fromPostCode);
            AddElement(addressElement, "CountryCode", fromCountryCode);


            XmlElement shipToElement = AddElement(shipmentElement, "ShipTo", "");

            addressElement = AddElement(shipToElement, "Address", "");
            AddElement(addressElement, "PostalCode", orderHistory.ship_to_post_code);
            AddElement(addressElement, "CountryCode", orderHistory.ship_to_country_code);
            AddElement(addressElement, "StateProvinceCode", "");

            XmlElement shipFromElement = AddElement(shipmentElement, "ShipFrom", "");

            addressElement = AddElement(shipFromElement, "Address", "");
            AddElement(addressElement, "PostalCode", fromPostCode);
            AddElement(addressElement, "CountryCode", fromCountryCode);
            AddElement(addressElement, "StateProvinceCode", "");

            XmlElement serviceCodeElement = AddElement(shipmentElement, "Service", "");
            AddElement(serviceCodeElement, "Code", translateRSSService(shippingAgentService.external_service_code));


            int i = 0;
            while (i < noOfPackages)
            {
                i++;

                XmlElement packageElement = AddElement(shipmentElement, "Package", "");

                XmlElement packageTypeElement = AddElement(packageElement, "PackagingType", "");
                AddElement(packageTypeElement, "Code", "02");

                XmlElement dimensionElement = AddElement(packageElement, "Dimensions", "");
                XmlElement uomElement = AddElement(dimensionElement, "UnitOfMeasurement", "");
                AddElement(uomElement, "Code", "CM");
                AddElement(uomElement, "Code", "KGS");
                AddElement(dimensionElement, "Length", "0");
                AddElement(dimensionElement, "Width", "0");
                AddElement(dimensionElement, "Height", "0");

                XmlElement packageHeightElement = AddElement(packageElement, "PackageWeight", "");
                AddElement(packageHeightElement, "UnitOfMeasurement", "");
                AddElement(packageHeightElement, "Weight", grossWeight.ToString("N2", CultureInfo.InvariantCulture.NumberFormat));


            }

            XmlElement rateInfoElement = AddElement(shipmentElement, "RateInformation", "");
            AddElement(rateInfoElement, "NegotiatedRatesIndicator", "");





            return xmlDoc;


        }

        private XmlDocument makeTITRequest(XmlDocument titRequestDoc)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            titRequestDoc.Save("C:\\temp\\ups_titrequest.xml");

            System.Net.HttpWebRequest httpRequest = System.Net.WebRequest.CreateHttp("https://www.ups.com/ups.app/xml/TimeInTransit");
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";

            StreamWriter streamWriter = new StreamWriter(httpRequest.GetRequestStream());

            streamWriter.Write(createAccessHeader().OuterXml+titRequestDoc.OuterXml);
            streamWriter.Flush();
            streamWriter.Close();

            System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpRequest.GetResponse();

            XmlDocument responseDoc = new XmlDocument();
            responseDoc.Load(httpResponse.GetResponseStream());


            responseDoc.Save("C:\\temp\\ups_titresponse.xml");

            return responseDoc;
        }

        private XmlDocument makeRSSRequest(XmlDocument rssRequestDoc, int no)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            rssRequestDoc.Save("C:\\temp\\ups_rssrequest_"+no+".xml");

            System.Net.HttpWebRequest httpRequest = System.Net.WebRequest.CreateHttp("https://www.ups.com/ups.app/xml/Rate");
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";

            StreamWriter streamWriter = new StreamWriter(httpRequest.GetRequestStream());

            streamWriter.Write(createAccessHeader().OuterXml + rssRequestDoc.OuterXml);
            streamWriter.Flush();
            streamWriter.Close();

            System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpRequest.GetResponse();

            XmlDocument responseDoc = new XmlDocument();
            responseDoc.Load(httpResponse.GetResponseStream());


            responseDoc.Save("C:\\temp\\ups_rssresponse_"+no+".xml");

            return responseDoc;
        }

        private List<ShippingAgentService> parseTITServiceDoc(XmlDocument responseDoc)
        {
            List<ShippingAgentService> serviceList = new List<ShippingAgentService>();

            XmlElement docElement = responseDoc.DocumentElement;
            XmlNodeList serviceNodeList = docElement.SelectNodes("TransitResponse/ServiceSummary");
            foreach(XmlElement serviceNode in serviceNodeList)
            {
                ShippingAgentService service = new ShippingAgentService();
                service.shipping_agent_code = "UPS";
                service.external_service_code = GetNodeValue(serviceNode, "Service/Code");
                service.description = GetNodeValue(serviceNode, "Service/Description");
                string pickUpDate = GetNodeValue(serviceNode, "EstimatedArrival/PickupDate");
                string pickUpTime = GetNodeValue(serviceNode, "EstimatedArrival/PickupTime");
                service.pickup_date_time = DateTime.Parse(pickUpDate + " " + pickUpTime);
                service.day_of_week = GetNodeValue(serviceNode, "EstimatedArrival/DayOfWeek");
                serviceList.Add(service);
            }

            return serviceList;


        }

        private bool parseRSSServiceDoc(XmlDocument responseDoc, ref ShippingAgentService service)
        {
            if (GetNodeValue(responseDoc.DocumentElement, "Response/ResponseStatusDescription").ToUpper() == "FAILURE") return false;

            service.currency_code = GetNodeValue(responseDoc.DocumentElement, "RatedShipment/NegotiatedRates/NetSummaryCharges/GrandTotal/CurrencyCode");
            string amountStr = GetNodeValue(responseDoc.DocumentElement, "RatedShipment/NegotiatedRates/NetSummaryCharges/GrandTotal/MonetaryValue");
            if (amountStr != "") service.freight_fee = Decimal.Parse(amountStr, CultureInfo.InvariantCulture.NumberFormat);


            return true;
        }

        private XmlElement AddElement(XmlElement parentElement, string nodeName, string nodeValue)
        {
            XmlElement childElement = parentElement.OwnerDocument.CreateElement(nodeName);
            if (nodeValue != "")
            {
                XmlText textNode = parentElement.OwnerDocument.CreateTextNode(nodeValue);
                childElement.AppendChild(textNode);
            }
            parentElement.AppendChild(childElement);

            return childElement;
        }

        private string GetNodeValue(XmlElement parentElement, string nodePath)
        {
            XmlElement element = (XmlElement)parentElement.SelectSingleNode(nodePath);
            if (element != null)
            {
                return element.InnerText;
            }
            return "";
        }

        private string translateRSSService(string code)
        {
            if (code == "06") return "07";
            if (code == "09") return "07";
            if (code == "10") return "07";
            if (code == "24") return "07";
            if (code == "05") return "08";
            if (code == "19") return "08";
            if (code == "03") return "11";
            if (code == "08") return "11";
            if (code == "25") return "11";
            if (code == "68") return "11";
            if (code == "18") return "65";
            if (code == "20") return "65";
            if (code == "26") return "65";
            if (code == "28") return "65";
            if (code == "22") return "54";
            if (code == "23") return "54";
            if (code == "21") return "54";

            return "07";
        }

        public JObject createRateRequestJson(OrderHistory orderHistory)
        {
            decimal grossWeight = 0;
            decimal totalQty = 0;
            foreach (OrderHistoryLine line in orderHistory.lines)
            {
                if (line.type == 2)
                {
                    Item item = ItemHelper.GetItem(orderHistory.customer_no, line.no);
                    if (item != null)
                    {
                        if (item.gross_weight == 0) item.gross_weight = schablon;
                        grossWeight += (item.gross_weight * line.quantity);
                    }
                    totalQty += line.quantity;
                }
            }
            if (grossWeight < schablon) grossWeight = schablon;


            int noOfPackages = (int)totalQty / quantityInCarton;
            if (((int)totalQty % quantityInCarton) > 0) noOfPackages++;


            var rateRequestObj = new
            {
                RateRequest = new
                {
                    Request = new
                    {
                        Subversion = "1703",
                        TransactionReference = new
                        {
                            CustomerContext = " "

                        }
                    },
                    Shipment = new
                    {
                        ShipmentRatingOptions = new
                        {
                            NegotiatedRatesIndicator = "true",
                        },

                        Shipper = new
                        {
                            ShipperNumber = shipperNumber,
                            Address = new
                            {
                                PostalCode = fromPostCode,
                                CountryCode = fromCountryCode

                            }
                        },

                        ShipTo = new
                        {
                            Address = new
                            {
                                City = orderHistory.ship_to_city,
                                PostalCode = orderHistory.ship_to_post_code,
                                CountryCode = orderHistory.ship_to_country_code
                            }
                        },
                        ShipFrom = new
                        {
                            Address = new
                            {
                                PostalCode = fromPostCode,
                                CountryCode = fromCountryCode
                            }
                        },
                        ShipmentTotalWeight = new
                        {
                            UnitOfMeasurement = new
                            {
                                Code = "KGS",
                                Description = "Kg"
                            },
                            Weight = ((int)grossWeight).ToString()
                        },

                        Package = new List<object>(),
 
                        DeliveryTimeInformation = new
                        {
                            PackageBillType = "03",
                            Pickup = new
                            {
                                Date = DateTime.Today.AddDays(1).ToString("yyyyMMdd")
                            }
                        },
                        InvoiceLineTotal = new
                        {
                            CurrencyCode = "SEK",
                            MonetaryValue = "1"
                        }



                    }
                }
            };

            int packageNo = 0;
            while (packageNo < noOfPackages)
            {
                var packageObj = new
                {
                    PackagingType = new
                    {
                        Code = "02",
                        Description = "Package"
                    },
                    Dimensions = new
                    {
                        UnitOfMeasurement = new
                        {
                            Code = "CM"
                        },
                        Length = "0",
                        Width = "0",
                        Height = "0"
                    },
                    PackageWeight = new
                    {
                        UnitOfMeasurement = new
                        {
                            Code = "KGS"
                        },
                        Weight = ((int)(grossWeight/noOfPackages)).ToString()
                    }

                };
                rateRequestObj.RateRequest.Shipment.Package.Add(packageObj);

                packageNo++;
            }

            JObject rateRequestObject = JObject.FromObject(rateRequestObj);

            return rateRequestObject;

        }

        private string makeRateRequest(string jsonRequest)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            StreamWriter loggerStreamWriter = new StreamWriter("C:\\temp\\raterequest.json");
            loggerStreamWriter.WriteLine(jsonRequest);
            loggerStreamWriter.Flush();
            loggerStreamWriter.Close();
                        
            System.Net.HttpWebRequest httpRequest = System.Net.WebRequest.CreateHttp("https://onlinetools.ups.com/ship/v1/rating/Shoptimeintransit");
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            httpRequest.Headers.Add("AccessLicenseNumber", accessLicenseNo);
            httpRequest.Headers.Add("Username", userId);
            httpRequest.Headers.Add("Password", password);

            StreamWriter streamWriter = new StreamWriter(httpRequest.GetRequestStream());

            streamWriter.Write(jsonRequest);
            streamWriter.Flush();
            streamWriter.Close();

            System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)httpRequest.GetResponse();

            StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());
            string jsonResponse = streamReader.ReadToEnd();
            streamReader.Close();

            loggerStreamWriter = new StreamWriter("C:\\temp\\raterequest.json", true);
            loggerStreamWriter.WriteLine("\n\nResponse\n"+ jsonResponse);
            loggerStreamWriter.Flush();
            loggerStreamWriter.Close();            

            return jsonResponse;
        }


        private List<ShippingAgentService> parseServicesJson(XmlDocument servicesDocument)
        {
            List<ShippingAgentService> serviceList = new List<ShippingAgentService>();
            List<ShippingAgentService> fullList = ShippingAgentHelper.GetServices();

            XmlNodeList nodeList = servicesDocument.DocumentElement.SelectNodes("RatedShipment");

            foreach (XmlNode serviceNode in nodeList)
            {
                XmlElement serviceElement = (XmlElement)serviceNode;

                foreach (ShippingAgentService fullService in fullList)
                {
                    
                    if (fullService.transport_service_code.Contains(GetNodeValue(serviceElement, "Service/Code")))
                    {
                        ShippingAgentService service = new ShippingAgentService();
                        service.shipping_agent_code = "UPS";
                        service.service = fullService.service;
                        service.code = fullService.code;
                        service.external_service_code = GetNodeValue(serviceElement, "Service/Code");
                        service.description = GetNodeValue(serviceElement, "TimeInTransit/ServiceSummary/Service/Description");
                        string pickUpDate = GetNodeValue(serviceElement, "TimeInTransit/ServiceSummary/EstimatedArrival/Arrival/Date");
                        string pickUpTime = GetNodeValue(serviceElement, "TimeInTransit/ServiceSummary/EstimatedArrival/Arrival/Time");
                        service.pickup_date_time = DateTime.Parse(pickUpDate.Substring(0, 4)+"-" + pickUpDate.Substring(4, 2)+"-"+ pickUpDate.Substring(6, 2)+ " " + pickUpTime.Substring(0, 2)+":"+pickUpTime.Substring(2, 2));
                        service.day_of_week = GetNodeValue(serviceElement, "TimeInTransit/ServiceSummary/EstimatedArrival/DayOfWeek");
                        service.freight_fee = Decimal.Parse(GetNodeValue(serviceElement, "NegotiatedRateCharges/TotalCharge/MonetaryValue"), CultureInfo.InvariantCulture);
                        serviceList.Add(service);
                    }
                    

                }

            }

        

            return serviceList;
        }
    }
}