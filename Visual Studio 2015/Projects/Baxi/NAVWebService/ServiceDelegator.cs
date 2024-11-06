using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Messaging;

namespace Navipro.Baxi.NAVWebService
{
    public class ServiceDelegator
    {
        private Configuration configuration;

        public ServiceDelegator()
        {
            //
            // TODO: Add constructor logic here
            //
            this.configuration = new Configuration();
            configuration.init();
        }

        public XmlDocument transport(XmlDocument xmlDocument)
        {
            // TODO:  Add TransporterMSMQ.transport implementation
            int timeout = configuration.msmqTimeout;

            Guid guid = Guid.NewGuid();

            XmlElement guidElement = xmlDocument.CreateElement("guid");
            XmlText guidText = xmlDocument.CreateTextNode(guid.ToString());
            guidElement.AppendChild(guidText);
            xmlDocument.DocumentElement.AppendChild(guidElement);

            MessageQueue msmqRequest = new MessageQueue("FORMATNAME:DIRECT=OS:" + configuration.msmqInQueue);

            msmqRequest.Send(xmlDocument, "Navision MSMQ-BA");

            MessageQueue msmqResponse = new MessageQueue("FORMATNAME:DIRECT=OS:" + configuration.msmqOutQueue);

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
                                        return responseDocument;
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
            return null;
        }

    }
}
