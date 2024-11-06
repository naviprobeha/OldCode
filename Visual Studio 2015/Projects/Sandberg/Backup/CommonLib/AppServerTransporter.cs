using System;
using System.Xml;
using System.Messaging;

namespace Navipro.Sandberg.Common
{
    /// <summary>
    /// Summary description for TransporterMSMQ.
    /// </summary>
    public class AppServerTransporter
    {
        private string inQueue;
        private string outQueue;
        private int timeout;
        private string errorMessage;

        public AppServerTransporter(string inQueue, string outQueue, int timeout)
        {
            //
            // TODO: Add constructor logic here
            //
            this.inQueue = inQueue;
            this.outQueue = outQueue;
            this.timeout = timeout;
        }
        #region Transporter Members

        public ServiceResponse transport(ServiceRequest serviceRequest)
        {
            // TODO:  Add TransporterMSMQ.transport implementation

            Guid guid = Guid.NewGuid();

            XmlDocument xmlDocument = serviceRequest.getDocument();

            XmlElement guidElement = xmlDocument.CreateElement("guid");
            XmlText guidText = xmlDocument.CreateTextNode(guid.ToString());
            guidElement.AppendChild(guidText);
            xmlDocument.DocumentElement.AppendChild(guidElement);

            try
            {
                MessageQueue msmqRequest = new MessageQueue("FORMATNAME:DIRECT=OS:" + inQueue);

                msmqRequest.Send(xmlDocument, "Navision MSMQ-BA");

                MessageQueue msmqResponse = new MessageQueue("FORMATNAME:DIRECT=OS:" + outQueue);

                DateTime currentTime = DateTime.Now;
                while (System.DateTime.Now < currentTime.AddMilliseconds(timeout))
                {
                    Message message = msmqResponse.Peek(System.TimeSpan.FromMilliseconds(timeout));
                    Message[] messages = msmqResponse.GetAllMessages();

                    int i = 0;
                    while (i < messages.Length)
                    {
                        message = messages[i];

                        if (message != null)
                        {
                            System.IO.StreamReader streamReader = new System.IO.StreamReader(message.BodyStream);
                            string documentString = streamReader.ReadToEnd();
                            XmlDocument responseDocument = new XmlDocument();
                            responseDocument.LoadXml(documentString);

                            XmlElement documentElement = responseDocument.DocumentElement;
                            if (documentElement != null)
                            {
                                guidElement = (XmlElement)documentElement.SelectSingleNode("guid");
                                if (guidElement != null)
                                {
                                    if (guidElement.FirstChild != null)
                                    {
                                        if (guidElement.FirstChild.Value == guid.ToString())
                                        {
                                            msmqResponse.ReceiveById(message.Id, System.TimeSpan.FromMilliseconds(timeout));
                                            ServiceResponse serviceResponse = new ServiceResponse(responseDocument);
                                            return serviceResponse;
                                        }
                                    }
                                    else
                                    {
                                        msmqResponse.ReceiveById(message.Id, System.TimeSpan.FromMilliseconds(timeout));
                                    }
                                }
                                else
                                {
                                    msmqResponse.ReceiveById(message.Id, System.TimeSpan.FromMilliseconds(timeout));
                                }
                            }
                            else
                            {
                                msmqResponse.ReceiveById(message.Id, System.TimeSpan.FromMilliseconds(timeout));
                            }
                        }

                        i++;
                    }
                }
                errorMessage = "ERROR NO RESPONSE";
                return null;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return null;
        }

        public string getErrorMessage()
        {
            return errorMessage;
        }

        #endregion
    }
}
