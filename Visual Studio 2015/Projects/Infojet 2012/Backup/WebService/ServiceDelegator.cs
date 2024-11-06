using System;
using System.Xml;
using System.Messaging;

namespace Navipro.Infojet.WebService
{
	/// <summary>
	/// Summary description for servicedelegator.
	/// </summary>
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
            if (configuration.version == "2013") return transportWS(xmlDocument);
            return transportMSMQ(xmlDocument);
        }

		public XmlDocument transportMSMQ(XmlDocument xmlDocument)
		{
			// TODO:  Add TransporterMSMQ.transport implementation
			int timeout = configuration.msmqTimeout;

			Guid guid = Guid.NewGuid();

			XmlElement guidElement = xmlDocument.CreateElement("guid");
			XmlText guidText = xmlDocument.CreateTextNode(guid.ToString());
			guidElement.AppendChild(guidText);
			xmlDocument.DocumentElement.AppendChild(guidElement);

			MessageQueue msmqRequest = new MessageQueue("FORMATNAME:DIRECT=OS:"+configuration.msmqInQueue);

            Message messageToSend = new Message(xmlDocument);
            messageToSend.TimeToBeReceived = new TimeSpan(0, 0, 20);
           
            
            msmqRequest.Send(messageToSend, "Navision MSMQ-BA");
				
			MessageQueue msmqResponse = new MessageQueue("FORMATNAME:DIRECT=OS:"+configuration.msmqOutQueue);

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
							}
						}
					}

					i++;
				}
			}
			return null;
		}

        public XmlDocument transportWS(XmlDocument xmlDocument)
        {
            // TODO:  Add TransporterMSMQ.transport implementation
            Guid guid = Guid.NewGuid();

            //XmlElement guidElement = xmlDocument.CreateElement("guid");
            //XmlText guidText = xmlDocument.CreateTextNode(guid.ToString());
            //guidElement.AppendChild(guidText);
            //xmlDocument.DocumentElement.AppendChild(guidElement);

            Navipro.Infojet.Lib.Configuration configuration = new Navipro.Infojet.Lib.Configuration();
            configuration.connectionString = this.configuration.connectionString;
            configuration.companyName = this.configuration.companyName;

            Navipro.Infojet.Lib.Database database = new Navipro.Infojet.Lib.Database(null, configuration);
            

            Navipro.Infojet.Lib.DatabaseQuery databaseQuery = database.prepare("INSERT INTO [" + database.getTableName("Web Request Queue") + "] ([Guid], [Inbound Document], [Outbound Document], [Created DateTime], [Processed DateTime], [Processed]) VALUES ('" + guid.ToString() + "', @xmlDoc, '', GETDATE(), '1754-01-01', 0)");
            databaseQuery.addImageParameter("xmlDoc", System.Text.Encoding.Default.GetBytes(xmlDocument.OuterXml));
            databaseQuery.execute();


            net.workanywhere.infojet.webRequestService serviceRequest = new net.workanywhere.infojet.webRequestService();
            serviceRequest.Url = this.configuration.wsAddress;
            serviceRequest.UseDefaultCredentials = true;

            serviceRequest.ProcessRequest(guid.ToString());


            databaseQuery = database.prepare("SELECT [Outbound Document] FROM [" + database.getTableName("Web Request Queue") + "] WHERE [Guid] = '" + guid.ToString() + "'");

            byte[] byteArray = (byte[])databaseQuery.executeScalar();
            string xmlDoc = System.Text.Encoding.Default.GetString(byteArray);


            XmlDocument outDocument = new XmlDocument();
            outDocument.LoadXml(xmlDoc);
            database.close();
            return outDocument;

        }

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }


	}
}
