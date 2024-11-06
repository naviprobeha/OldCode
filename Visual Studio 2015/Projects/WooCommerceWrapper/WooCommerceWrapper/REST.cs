using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace WooCommerceWrapper
{
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("CB4307C0-FA8D-4129-A3F2-CB2E8F5CB56C")]
    public interface IRest
    {
        [DispId(1)]
        bool Execute(ref XmlDocument requestDoc, ref string errorMessage, ref XmlDocument responseDoc);
    }

    [ClassInterface(ClassInterfaceType.None)]
    [Guid("00DA3303-A259-4641-8459-4A5D7F3D286F")]
    [ProgId("WooCommerceWrapper.Rest")]
    public class Rest : IRest
    {
        public Rest()
        {
        }

        //http://james.newtonking.com/json/help/index.html?topic=html/ConvertingJSONandXML.htm
        //http://stackoverflow.com/questions/814001/convert-json-string-to-xml-or-xml-to-json-string

        //http://docs.woocommercev2.apiary.io/introduction/requirements
        //http://woothemes.github.io/woocommerce/rest-api/#introduction


        /// <summary>
        /// Filter values for Orderstatuses: pending, on-hold, processing, completed, refunded, failed, cancelled.
        /// </summary>
        /// <param name="apiUrl">The URL to the Rest API : Use only HTTPS adress</param>
        /// <param name="userName">Username to use</param>
        /// <param name="password">Password to use</param>
        public Rest(string apiUrl, string userName, string password)
        {
            RestUrl = apiUrl;
            ConsumerKey = userName;
            ConsumerSecret = password;
        }

        public static string RestUrl { get; set; }
        public static string ConsumerKey { get; set; }
        public static string ConsumerSecret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestDoc">XML doc with request data </param>
        /// <param name="errorMessage">Empty string for exception details</param>
        /// <param name="responseDoc">XML doc for response data</param>
        /// <returns></returns> 
        public bool Execute(ref XmlDocument requestDoc, ref string errorMessage, ref XmlDocument responseDoc)
        {
            var method = string.Empty;
            var objectType = string.Empty;
            var filter = string.Empty;
            var id = string.Empty;

            XmlNode dataNode;
            try
            {
                #region Check Method
                XmlNode methodNode = requestDoc.SelectSingleNode("/nav/method");
                if (methodNode != null)
                {
                    method = methodNode.InnerText.ToUpper();
                }
                else
                {
                    errorMessage = "Node [method] is missing or not in [CREATE,UPDATE,GET,DELETE]!";
                    return false;
                }
                #endregion

                #region Check MessageType
                XmlNode typeNode = requestDoc.SelectSingleNode("/nav/type");
                if (typeNode != null)
                {
                    objectType = typeNode.InnerText.ToLower();
                }
                else
                {
                    errorMessage = "Node [type] is missing or not in [customers,orders,products,coupons,reports]";
                    return false;
                }
                #endregion

                #region Check Filter
                XmlNode filterNode = requestDoc.SelectSingleNode("/nav/filter");
                if (filterNode != null)
                {
                    filter = filterNode.InnerText.ToLower();
                }
                #endregion

                #region Check ID
                XmlNode idNode = requestDoc.SelectSingleNode("/nav/id");
                if (idNode != null)
                {
                    id = idNode.InnerText.ToLower();
                }

                #endregion

                #region Check Data
                dataNode = requestDoc.SelectSingleNode("/nav/data");
                if (dataNode == null && method != "GET")
                {
                    errorMessage = "Node [Data] is missing  and is required on CREATE,UPDATE,DELETE!";
                    return false;
                }

                if (dataNode != null && method != "GET" && dataNode.InnerXml.Length == 0)
                {
                    errorMessage = "Node [Data] is empty. Nothing to " + method;
                    return false;
                }

                #endregion
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return false;
            }

            try
            {
                switch (method)
                {
                    case "GET":
                        {
                            responseDoc = Get(objectType, filter, id);
                            break;
                        }
                    case "CREATE":
                        {
                            responseDoc = Create(objectType, dataNode);
                            break;
                        }
                    case "UPDATE":
                        {
                            if (String.IsNullOrEmpty(id))
                            {
                                errorMessage = "Node [Id] is missing  and is required on UPDATE,DELETE!";
                                return false;
                            }
                            responseDoc = Update(objectType, dataNode, id);
                            break;
                        }
                    case "DELETE":
                        {
                            if (String.IsNullOrEmpty(id))
                            {
                                errorMessage = "Node [Id] is missing  and is required on UPDATE,DELETE!";
                                return false;
                            }
                            responseDoc = Delete(objectType, dataNode, id);
                            break;
                        }
                }

                if (objectType.Contains("refunds"))
                {
                    objectType = "order_refund";
                }

                var selectSingleNode = responseDoc.SelectSingleNode(objectType + "/errors");
                if (selectSingleNode != null)
                {
                    errorMessage = selectSingleNode.LastChild.InnerText;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// GET metod till WooCommerce site
        /// </summary>
        /// <param name="objectType">customers,orders,products,coupons,reports (Required)</param>
        /// <param name="filter">node with filter values</param>
        /// <param name="id">node with ID value</param>
        /// <returns></returns>
        internal static XmlDocument Get(string objectType, string filter, string id)
        {
            //ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls; //kan variera mellan kunderna, ska kanske parameterstyras??

            var url = "";

            if (!string.IsNullOrEmpty(id))
            {
                url = RestUrl + "/" + objectType + "/" + id + "?consumer_key=" + ConsumerKey + "&consumer_secret=" + ConsumerSecret;
            }
            else
            {
                if (string.IsNullOrEmpty(filter.Trim()))
                    url = RestUrl + "/" + objectType + "?consumer_key=" + ConsumerKey + "&consumer_secret=" + ConsumerSecret;
                else
                    url = RestUrl + "/" + objectType + "?consumer_key=" + ConsumerKey + "&consumer_secret=" + ConsumerSecret + "&" + filter;
            }

            byte[] data;
            var client = new WebClient();

            //var myCreds = new NetworkCredential(ConsumerKey, ConsumerSecret);
            //client.Credentials = myCreds;

            //string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(ConsumerKey + ":" + ConsumerSecret));
            //client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            //client.Headers["Content-type"] = "application/json";
            //client.Headers["Accept-Encoding"] = "gzip,deflate";
            //client.Headers["User-Agent"] = "Apache-HttpClient/4.1.1";


            //var data1 = client.DownloadString(url);
            //JObject o = JObject.Parse(data1);
            //var t = o;

            data = client.DownloadData(url);


            return ConvertResponseToXml(data, objectType);
        }

        internal static XmlDocument Create(string objectType, XmlNode dataNode)
        {
            //ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var doc = new XmlDocument();
            var url = RestUrl + "/" + objectType + "?consumer_key=" + ConsumerKey + "&consumer_secret=" + ConsumerSecret;

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(ConsumerKey + ":" + ConsumerSecret));

            var request = WebRequest.Create(url) as HttpWebRequest;
            request.KeepAlive = false;
            request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            request.Method = "POST";
            request.ContentType = "application/json";

            string s = string.Empty;

            switch (objectType)
            {
                case "addresses": { s = objectType.Remove(objectType.Length - 2); break; }
                default:
                    {
                        if (objectType.Contains("refunds"))
                        {
                            s = "order_refund";
                            objectType = "order_refund";
                        }
                        else
                            s = objectType.Remove(objectType.Length - 1); break;
                    }
            }

            XmlNodeList nodes = dataNode.SelectNodes(s);

            if (nodes == null) return doc;

            try
            {
                // serialize the object data in json format                
                string json = JsonConvert.SerializeXmlNode(nodes[0]);
                var str1 = json.Replace("\"false\"", "false");
                var str2 = str1.Replace("\"true\"", "true");

                byte[] byteArray = Encoding.UTF8.GetBytes(str2);

                request.ContentLength = byteArray.Length;

                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                    var response = request.GetResponse();
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseContent = reader.ReadToEnd();
                        XmlNode eNode = JsonConvert.DeserializeXmlNode(responseContent, objectType);
                        XmlNode exceptionNode = doc.ImportNode(eNode.FirstChild, true);
                        doc.AppendChild(exceptionNode);
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string responseContent = reader.ReadToEnd();
                        XmlNode eNode = JsonConvert.DeserializeXmlNode(responseContent, objectType);
                        XmlNode exceptionNode = doc.ImportNode(eNode.FirstChild, true);
                        doc.AppendChild(exceptionNode);
                    }
                }
                else
                {
                    throw;
                }

            }
            return doc;
        }

        internal static XmlDocument Update(string objectType, XmlNode dataNode, string id)
        {

            //ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var doc = new XmlDocument();
            var url = RestUrl + "/" + objectType;

            if (!string.IsNullOrEmpty(id))
            {
                url = url + "/" + id;//"\\ -u " + ConsumerKey + ":" + ConsumerSecret;                    
            }
            else
            {
                throw new Exception("Node [Id] is missing  and is required on UPDATE");
            }

            url = url + "?consumer_key=" + ConsumerKey + "&consumer_secret=" + ConsumerSecret;

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(ConsumerKey + ":" + ConsumerSecret));

            var request = WebRequest.Create(url) as System.Net.HttpWebRequest;
            request.KeepAlive = false;
            request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            request.Method = "PUT";
            request.ContentType = "application/json";
            string s = string.Empty;
            // serialize the object data in json format            
            switch (objectType)
            {
                case "addresses": { s = objectType.Remove(objectType.Length - 2); break; }
                default:
                    {
                        if (objectType.Contains("refunds"))
                        {
                            s = "order_refund";
                            objectType = "order_refund";
                        }
                        else
                            s = objectType.Remove(objectType.Length - 1); break;
                    }
            }
            XmlNodeList nodes = dataNode.SelectNodes(s);

            if (nodes == null) return doc;

            try
            {
                string json = JsonConvert.SerializeXmlNode(nodes[0]);
                var str1 = json.Replace("\"false\"", "false");
                var str2 = str1.Replace("\"true\"", "true");

                byte[] byteArray = Encoding.UTF8.GetBytes(str2);

                request.ContentLength = byteArray.Length;

                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                    var response = request.GetResponse();
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseContent = reader.ReadToEnd();
                        XmlNode eNode = JsonConvert.DeserializeXmlNode(responseContent, objectType);
                        XmlNode exceptionNode = doc.ImportNode(eNode.FirstChild, true);
                        doc.AppendChild(exceptionNode);
                    }
                }
            }
            catch (WebException ex)
            {
                using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string responseContent = reader.ReadToEnd();
                    XmlNode eNode = JsonConvert.DeserializeXmlNode(responseContent, objectType);
                    XmlNode exceptionNode = doc.ImportNode(eNode.FirstChild, true);
                    doc.AppendChild(exceptionNode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return doc;
        }

        //internal static XmlDocument Update2(string objectType, XmlNode dataNode)
        //{
        //    ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

        //    var doc = new XmlDocument();

        //    var s = objectType.Remove(objectType.Length - 1);
        //    XmlNodeList nodes = dataNode.SelectNodes(s);

        //    if (nodes != null)


        //        try
        //        {
        //            string productId;
        //            XmlNode skuNode = node.SelectSingleNode("/product/id");
        //            if (skuNode != null)
        //            {
        //                productId = skuNode.InnerText.ToUpper();
        //                if (string.IsNullOrEmpty(productId))
        //                    throw new Exception("ID node is empty !");
        //            }
        //            else
        //            {
        //                throw new Exception("ID node is missing !");
        //            }

        //             serialize the object data in json format
        //            string json = JsonConvert.SerializeXmlNode(node);
        //            byte[] byteArray = Encoding.UTF8.GetBytes(json);

        //            var url = RestUrl + "/" + objectType + "/#" + productId;

        //            var request = WebRequest.Create(url) as HttpWebRequest;
        //            request.KeepAlive = false;

        //            string credentials =
        //                Convert.ToBase64String(Encoding.ASCII.GetBytes(ConsumerKey + ":" + ConsumerSecret));
        //            request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
        //            request.Method = "PUT";

        //            request.ContentType = "application/json";
        //            request.ContentLength = byteArray.Length;

        //            using (var writer = request.GetRequestStream())
        //            {
        //                writer.Write(byteArray, 0, byteArray.Length);
        //                request.GetResponse();
        //            }
        //        }
        //        catch (WebException ex)
        //        {
        //            using (var reader = new StreamReader(ex.Response.GetResponseStream()))
        //            {
        //                string responseContent = reader.ReadToEnd();

        //                XmlNode eNode = JsonConvert.DeserializeXmlNode(responseContent, objectType);
        //                XmlNode exceptionNode = doc.ImportNode(eNode.FirstChild, true);

        //                XmlNode importNode = doc.ImportNode(node, true);
        //                exceptionNode.AppendChild(importNode);

        //                doc.AppendChild(exceptionNode);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            XmlNode exceptionNode = doc.CreateNode(XmlNodeType.Text, "errors", "");
        //            exceptionNode.InnerText = ex.Message;

        //            XmlNode importNode = doc.ImportNode(node, true);
        //            exceptionNode.AppendChild(importNode);
        //            doc.AppendChild(exceptionNode);
        //        }


        //    return doc;
        //}

        internal static XmlDocument Delete(string objectType, XmlNode dataNode, string id)
        {
            string productId;
            byte[] data;
            XmlNode skuNode = dataNode.SelectSingleNode("/product/id");
            if (skuNode != null)
            {
                productId = skuNode.InnerText.ToUpper();
                if (string.IsNullOrEmpty(productId))
                    throw new Exception("ID node is empty !");
            }
            else
            {
                throw new Exception("ID node is missing !");
            }

            var url = RestUrl + "/" + objectType + "#{" + productId + "}" + "?consumer_key=" + ConsumerKey + "&consumer_secret=" + ConsumerSecret; ;

            ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            var client = new WebClient();
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(ConsumerKey + ":" + ConsumerSecret));
            client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            client.Headers["Content-type"] = "application/json";

            data = client.DownloadData(url);

            return ConvertResponseToXml(data, objectType);
        }

        internal static XmlDocument ConvertResponseToXml(byte[] data, string objectType)
        {
            // put the downloaded data in a memory stream
            var ms = new MemoryStream(data);

            using (var sr = new StreamReader(ms))
            {
                string jsonString = sr.ReadToEnd();

                XmlDocument doc = new XmlDocument();


                //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var jsonObject = serializer.DeserializeObject(sr.ReadToEnd());

                //var str = JsonConvert.SerializeObject(jsonString, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                //var obj = JsonConvert.DeserializeObject(jsonString, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                try
                {
                    var jObj = (JToken)JsonConvert.DeserializeObject(jsonString);
                    string[] fieldsToRemove = new[] { "customer_meta" };
                    removeFields(jObj, fieldsToRemove);

                    doc = JsonConvert.DeserializeXmlNode(jObj.ToString(), "wooCom");

                    Assembly assembly = Assembly.GetExecutingAssembly();
                    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                    string version = fileVersionInfo.ProductVersion;
                    XmlNode newNode = doc.CreateElement("DLLVersion");
                    XmlNode errorNode = doc.CreateNode(XmlNodeType.Element, "DLLVersion", "");
                    errorNode.InnerText = version;

                    XmlElement root = doc.DocumentElement;
                    root.AppendChild(errorNode);
                }
                catch (Exception e)
                {
                    doc = new XmlDocument();
                    doc.LoadXml("<" + objectType + "></" + objectType + ">");

                    XmlNode errorNode = doc.CreateNode(XmlNodeType.Element, "errors", "");
                    errorNode.InnerText = jsonString;

                    XmlElement root = doc.DocumentElement;
                    root.AppendChild(errorNode);

                }

              
                //try
                //{

                //    // doc = JsonConvert.DeserializeXmlNode(ClearJSonString(jsonString));

                //    doc = JsonConvert.DeserializeXmlNode(jsonString, "wooCom");


                //    XmlNodeList nodes = doc.SelectNodes("//customer_meta");
                //    if (nodes != null)
                //        for (int i = nodes.Count - 1; i >= 0; i--)
                //        {
                //            var parentNode = nodes[i].ParentNode;
                //            if (parentNode != null) parentNode.RemoveChild(nodes[i]);
                //        }

                //    nodes = doc.SelectNodes("//metaboxhidden_shop_order");
                //    if (nodes != null)
                //        for (int i = nodes.Count - 1; i >= 0; i--)
                //        {
                //            var parentNode = nodes[i].ParentNode;
                //            if (parentNode != null) parentNode.RemoveChild(nodes[i]);
                //        }

                //    nodes = doc.SelectNodes("//session_tokens");
                //    if (nodes != null)
                //        for (int i = nodes.Count - 1; i >= 0; i--)
                //        {
                //            var parentNode = nodes[i].ParentNode;
                //            if (parentNode != null) parentNode.RemoveChild(nodes[i]);
                //        }

                //    Assembly assembly = Assembly.GetExecutingAssembly();
                //    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                //    string version = fileVersionInfo.ProductVersion;
                //    XmlNode newNode = doc.CreateElement("DLLVersion");
                //    XmlNode errorNode = doc.CreateNode(XmlNodeType.Element, "DLLVersion", "");
                //    errorNode.InnerText = version;

                //    XmlElement root = doc.DocumentElement;
                //    root.AppendChild(errorNode);

                //}
                //catch (Exception ex)
                //{
                //    doc = new XmlDocument();
                //    doc.LoadXml("<" + objectType + "></" + objectType + ">");

                //    XmlNode errorNode = doc.CreateNode(XmlNodeType.Element, "errors", "");
                //    errorNode.InnerText = jsonString;

                //    XmlElement root = doc.DocumentElement;
                //    root.AppendChild(errorNode);
                //}


                //XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonString);
                return doc;
            }

        }

        /// <summary>
        /// Certificate validation callback.
        /// </summary>
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (error == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            Console.WriteLine("X509Certificate [{0}] Policy Error: '{1}'",
                cert.Subject,
                error.ToString());

            return false;
        }

        private static void removeFields(JToken token, string[] fields)
        {
            JContainer container = token as JContainer;
            if (container == null) return;

            List<JToken> removeList = new List<JToken>();
            foreach (JToken el in container.Children())
            {
                JProperty p = el as JProperty;
                if (p != null && fields.Contains(p.Name))
                {
                    removeList.Add(el);
                }
                removeFields(el, fields);
            }

            foreach (JToken el in removeList)
            {
                el.Remove();
            }
        }
        private static string ClearJSonString(string jsonIn)
        {
            var parsed = (JContainer)JsonConvert.DeserializeObject(jsonIn);
            var nodesToDelete = new List<JToken>();

            do
            {
                nodesToDelete.Clear();

                ClearEmpty(parsed, nodesToDelete);

                foreach (var token in nodesToDelete)
                {
                    token.Remove();
                }
            } while (nodesToDelete.Count > 0);

            return parsed.ToString();
        }

        private static void ClearEmpty(JContainer container, List<JToken> nodesToDelete)
        {
            if (container == null) return;

            foreach (var child in container.Children())
            {
                var cont = child as JContainer;

                if (child.Type == JTokenType.Property ||
                    child.Type == JTokenType.Object ||
                    child.Type == JTokenType.Array)
                {
                    if (child.HasValues)
                    {
                        ClearEmpty(cont, nodesToDelete);
                    }
                    else
                    {
                        nodesToDelete.Add(child.Parent);
                    }
                }
            }
        }

        //public static string Base64Encode(string plainText)
        //{
        //    var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        //    return Convert.ToBase64String(plainTextBytes);
        //}

        //internal static XmlDocument Get2(string objectType, XmlNode dataNode, string filter)
        //{
        //    var url = "";
        //    string json = string.Empty;
        //    if (dataNode != null && string.IsNullOrEmpty(filter))
        //    {
        //        var selectSingleNode = dataNode.SelectSingleNode("id");

        //        if (selectSingleNode != null)
        //        {
        //            var doc = new XmlDocument();
        //            doc.AppendChild(dataNode);
        //            json = JsonConvert.SerializeXmlNode(doc);
        //            url = RestUrl + "/" + objectType + "#{" + selectSingleNode.InnerText + "} ";//"\\ -u " + ConsumerKey + ":" + ConsumerSecret;                   
        //        }
        //        else
        //            throw new Exception("ID node is missing or empty");
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(filter.Trim()))
        //            url = RestUrl + "/" + objectType;//+ @"\ -u " + ConsumerKey + ":" + ConsumerSecret;
        //        else
        //            url = RestUrl + "/" + objectType + "?" + filter;// + @"\ -u " + ConsumerKey + ":" + ConsumerSecret;                                                
        //    }


        //    var request = WebRequest.Create(url) as System.Net.HttpWebRequest;

        //    if (request != null)
        //    {
        //        var myCreds = new NetworkCredential(ConsumerKey, ConsumerSecret);
        //        request.Credentials = myCreds;

        //        request.Method = "GET";
        //        request.KeepAlive = false;
        //        //request.ContentType = "application/json";

        //        using (WebResponse response = request.GetResponse())
        //        {
        //            using (var streamReader = new MemoryStream())
        //            {
        //                var responseStream = response.GetResponseStream();
        //                if (responseStream != null) responseStream.CopyTo(streamReader);
        //                byte[] data = streamReader.ToArray();
        //                return ConvertResponseToXml(data, objectType);
        //            }

        //        }

        //    }


        //    return new XmlDocument();
        //}

    }

}
