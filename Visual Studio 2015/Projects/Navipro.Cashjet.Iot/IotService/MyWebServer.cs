using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.ApplicationModel.Background;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace IotService
{
    internal class MyWebserver
    {
        private const uint BufferSize = 8192;

        public bool Start()
        {
            var listener = new StreamSocketListener();
            var currentSetting = listener.Control.QualityOfService;
            listener.Control.QualityOfService = SocketQualityOfService.LowLatency;

            listener.ConnectionReceived += connectionReceived;
 
            listener.BindServiceNameAsync("8081").AsTask();

            return true;
        }

        private async void connectionReceived(object sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            try
            {
                var request = new StringBuilder();

                
                using (var input = args.Socket.InputStream)
                {
                    var data = new byte[BufferSize];
                    IBuffer buffer = data.AsBuffer();
                    var dataRead = BufferSize;
                    
                    while (dataRead == BufferSize)
                    {
                        await input.ReadAsync(buffer, BufferSize, InputStreamOptions.Partial);
                        request.Append(Encoding.UTF8.GetString(data, 0, data.Length));
                        dataRead = buffer.Length;
                    }
                    
                }
                

                string query = GetQuery(request);
                string method = getParameter(query, "method");

                if ((method == "") || (method == null)) statusPage(args.Socket, request);
                if (method == "checkStatus") checkStatus(args.Socket, request);
                if (method == "register") register(args.Socket, request);


                statusPage(args.Socket, request);

                args.Socket.Dispose();
            }
            catch (Exception)
            { }

        }

        private static string GetQuery(StringBuilder request)
        {
            var requestLines = request.ToString().Split(' ');

            var url = requestLines.Length > 1 ? requestLines[1] : string.Empty;

            var uri = new Uri("http://localhost" + url);
            var query = uri.Query;

                       
            return query;
        }

        private void statusPage(StreamSocket socket, StringBuilder request)
        {
                 using (var output = socket.OutputStream)
                {
                    using (var response = output.AsStreamForWrite())
                    {
                        string contentString = "<html><head><title>Cashjet Iot Control Unit</title></head><body>";

                        contentString = contentString + "<h1>Cashjet Iot Control Unit: Status</h1>";
                        contentString = contentString + "<p>Query: " + GetQuery(request) + ", "+ getParameter(GetQuery(request), "method")+"</p>";

                        DeviceInformation serialPort = FTDIHelper.getFTDISerialPort().Result;
                        if (serialPort != null)
                        {
                            contentString = contentString + "<p>USB serial device found at location " + serialPort.Id + " (" + serialPort.Name + ").</p>";
                            try
                            {
                                contentString = contentString + "<p>Checking status... Model ID: " + ControlUnit.checkStatus(serialPort).ConfigureAwait(true).GetAwaiter().GetResult() + "</p>";
                            }
                            catch (Exception e)
                            {
                                contentString = contentString + "<p>Checking status... Error: " + e.Message + "</p>";
                            }
                        }
                        else
                        {
                            contentString = contentString + "<p>No connected USB serial device found.</p>";
                        }



                        contentString = contentString + "</body></html>";

                        var html = Encoding.UTF8.GetBytes(contentString);
                        using (var bodyStream = new MemoryStream(html))
                        {
                            var header = $"HTTP/1.1 200 OK\r\nContent-Length: {bodyStream.Length}\r\nConnection: close\r\n\r\n";
                            var headerArray = Encoding.UTF8.GetBytes(header);
                            response.WriteAsync(headerArray, 0, headerArray.Length).Wait();
                            bodyStream.CopyToAsync(response).Wait();
                            response.FlushAsync().Wait();

                        }

                    }

            }

        }

        private void checkStatus(StreamSocket socket, StringBuilder request)
        {
            using (var output = socket.OutputStream)
            {
                using (var response = output.AsStreamForWrite())
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><status/>");
                    XmlElement docElement = xmlDoc.DocumentElement;
                    

                    DeviceInformation serialPort = FTDIHelper.getFTDISerialPort().Result;
                    if (serialPort != null)
                    {
                        addElement(docElement, "serialDevice", serialPort.Id);
                        try
                        {
                            addElement(docElement, "modelId", ControlUnit.checkStatus(serialPort).ConfigureAwait(true).GetAwaiter().GetResult());
                            addElement(docElement, "status", "OK");
                        }
                        catch (Exception e)
                        {
                            addElement(docElement, "error", "Fel: "+e.Message);
                        }
                    }
                    else
                    {
                        addElement(docElement, "error", "Ingen ansluten kontrollenhet.");
                    }


                    var html = Encoding.UTF8.GetBytes(xmlDoc.OuterXml);
                    using (var bodyStream = new MemoryStream(html))
                    {
                        var header = $"HTTP/1.1 200 OK\r\nContent-Length: {bodyStream.Length}\r\nConnection: close\r\n\r\n";
                        var headerArray = Encoding.UTF8.GetBytes(header);
                        response.WriteAsync(headerArray, 0, headerArray.Length).Wait();
                        bodyStream.CopyToAsync(response).Wait();
                        response.FlushAsync().Wait();

                    }

                }

            }

        }

        private void register(StreamSocket socket, StringBuilder request)
        {
            using (var output = socket.OutputStream)
            {
                using (var response = output.AsStreamForWrite())
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><register/>");
                    XmlElement docElement = xmlDoc.DocumentElement;

                    string query = GetQuery(request);
                    string registeredTransactionNo = getParameter(query, "registeredTransactionNo");
                    string posDeviceId = getParameter(query, "posDeviceId");
                    string userId = getParameter(query, "userId");
                    string registrationNo = getParameter(query, "registrationNo");
                    string totalAmount = getParameter(query, "totalAmount");
                    string returnAmount = getParameter(query, "returnAmount");
                    string receiptType = getParameter(query, "receiptType");
                    string vatText1 = getParameter(query, "vatText1");
                    string vatText2 = getParameter(query, "vatText2");
                    string vatText3 = getParameter(query, "vatText3");
                    string vatText4 = getParameter(query, "vatText4");

                    if (vatText1 == "") vatText1 = "0,00;0,00";
                    if (vatText2 == "") vatText2 = "0,00;0,00";
                    if (vatText3 == "") vatText3 = "0,00;0,00";
                    if (vatText4 == "") vatText4 = "0,00;0,00";


                    DeviceInformation serialPort = FTDIHelper.getFTDISerialPort().Result;
                    if (serialPort != null)
                    {
                        addElement(docElement, "serialDevice", serialPort.Id);
                        try
                        {
                            
                            string signatureRaw = ControlUnit.registerReceipt(serialPort, registeredTransactionNo, posDeviceId, userId, registrationNo, totalAmount, returnAmount, receiptType, vatText1, vatText2, vatText3, vatText4).ConfigureAwait(true).GetAwaiter().GetResult();
                            string modelId = signatureRaw.Substring(0, signatureRaw.IndexOf(";"));
                            string signature = signatureRaw.Substring(signatureRaw.IndexOf(";")+1);

                            addElement(docElement, "response", signatureRaw);
                            addElement(docElement, "modelId", modelId);
                            addElement(docElement, "signature", signature);
                            addElement(docElement, "status", "OK");
                        }
                        catch (Exception e)
                        {
                            addElement(docElement, "error", "Fel: " + e.Message);
                        }
                    }
                    else
                    {
                        addElement(docElement, "error", "Ingen ansluten kontrollenhet.");
                    }


                    var html = Encoding.UTF8.GetBytes(xmlDoc.OuterXml);
                    using (var bodyStream = new MemoryStream(html))
                    {
                        var header = $"HTTP/1.1 200 OK\r\nContent-Length: {bodyStream.Length}\r\nConnection: close\r\n\r\n";
                        var headerArray = Encoding.UTF8.GetBytes(header);
                        response.WriteAsync(headerArray, 0, headerArray.Length).Wait();
                        bodyStream.CopyToAsync(response).Wait();
                        response.FlushAsync().Wait();

                    }

                }

            }

        }

        private string getParameter(string query, string name)
        {
            if (query.IndexOf(name) > -1)
            {
                string value = query.Substring(query.IndexOf(name));
                if (value.IndexOf('&') > -1)
                {
                    value = value.Substring(0, value.IndexOf('&'));
                }
                value = value.Substring(value.IndexOf('=')+1);
                return value;
            }

            return "";
        }

        private XmlElement addElement(XmlElement parentElement, string name, string value)
        {
            XmlElement xmlElement = parentElement.OwnerDocument.CreateElement(name);
            XmlText xmlText = parentElement.OwnerDocument.CreateTextNode(value);
            xmlElement.AppendChild(xmlText);
            parentElement.AppendChild(xmlElement);

            return xmlElement;
        }
    }

}
