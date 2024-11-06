using System;
using System.Xml;
using System.Messaging;

namespace NavisionMobile
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
            //if (configuration.version == "2013") return transportWS(xmlDocument);

            //throw new Exception("Exception: hEPP");
            
            return transportMSMQ(xmlDocument);
        }

		public XmlDocument transportMSMQ(XmlDocument xmlDocument)
		{
			// TODO:  Add TransporterMSMQ.transport implementation
			int timeout = configuration.msmqTimeout;

			Guid guid = Guid.NewGuid();

			XmlElement guidElement = xmlDocument.CreateElement("GUID");
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

							guidElement = (XmlElement)documentElement.SelectSingleNode("GUID");
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
			
			return generateError("001", "");
			

		}

 

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public XmlDocument generateError(string errorCode, string errorDesc)
        {
            XmlDocument errorDocument = new XmlDocument();
            XmlElement xmlPerformService = errorDocument.CreateElement("PERFORM_SERVICE");
            XmlElement xmlServiceResponse = errorDocument.CreateElement("SERVICE_RESPONSE");
            XmlElement xmlError = errorDocument.CreateElement("ERROR");

            if (errorCode.Equals("001"))
            {
                XmlElement xmlCode = errorDocument.CreateElement("CODE");
                XmlText xmlValue = errorDocument.CreateTextNode(errorCode);
                xmlCode.AppendChild(xmlValue);

                XmlElement xmlStatus = errorDocument.CreateElement("STATUS");
                xmlValue = errorDocument.CreateTextNode("Synchronization failed");
                xmlStatus.AppendChild(xmlValue);

                XmlElement xmlDescription = errorDocument.CreateElement("DESCRIPTION");
                xmlValue = errorDocument.CreateTextNode("Could not establish contact with Navision.");
                xmlDescription.AppendChild(xmlValue);

                xmlError.AppendChild(xmlCode);
                xmlError.AppendChild(xmlStatus);
                xmlError.AppendChild(xmlDescription);
            }

            if (errorCode.Equals("002"))
            {
                XmlElement xmlCode = errorDocument.CreateElement("CODE");
                XmlText xmlValue = errorDocument.CreateTextNode(errorCode);
                xmlCode.AppendChild(xmlValue);

                XmlElement xmlStatus = errorDocument.CreateElement("STATUS");
                xmlValue = errorDocument.CreateTextNode("Synchronization failed");
                xmlStatus.AppendChild(xmlValue);

                XmlElement xmlDescription = errorDocument.CreateElement("DESCRIPTION");
                xmlValue = errorDocument.CreateTextNode("Message Queue Exception: " + errorDesc);
                xmlDescription.AppendChild(xmlValue);

                xmlError.AppendChild(xmlCode);
                xmlError.AppendChild(xmlStatus);
                xmlError.AppendChild(xmlDescription);
            }

            if (errorCode.Equals("003"))
            {
                XmlElement xmlCode = errorDocument.CreateElement("CODE");
                XmlText xmlValue = errorDocument.CreateTextNode(errorCode);
                xmlCode.AppendChild(xmlValue);

                XmlElement xmlStatus = errorDocument.CreateElement("STATUS");
                xmlValue = errorDocument.CreateTextNode("Synchronization failed");
                xmlStatus.AppendChild(xmlValue);

                XmlElement xmlDescription = errorDocument.CreateElement("DESCRIPTION");
                xmlValue = errorDocument.CreateTextNode(errorDesc);
                xmlDescription.AppendChild(xmlValue);

                xmlError.AppendChild(xmlCode);
                xmlError.AppendChild(xmlStatus);
                xmlError.AppendChild(xmlDescription);
            }

            if (errorCode.Equals("004"))
            {
                XmlElement xmlCode = errorDocument.CreateElement("CODE");
                XmlText xmlValue = errorDocument.CreateTextNode(errorCode);
                xmlCode.AppendChild(xmlValue);

                XmlElement xmlStatus = errorDocument.CreateElement("STATUS");
                xmlValue = errorDocument.CreateTextNode("Synchronization failed");
                xmlStatus.AppendChild(xmlValue);

                XmlElement xmlDescription = errorDocument.CreateElement("DESCRIPTION");
                xmlValue = errorDocument.CreateTextNode("IP Exception: " + errorDesc);
                xmlDescription.AppendChild(xmlValue);

                xmlError.AppendChild(xmlCode);
                xmlError.AppendChild(xmlStatus);
                xmlError.AppendChild(xmlDescription);
            }

            xmlServiceResponse.AppendChild(xmlError);
            xmlPerformService.AppendChild(xmlServiceResponse);

            errorDocument.AppendChild(xmlPerformService);

            return errorDocument;
        }



	}
}
