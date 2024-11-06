using System;
using System.Xml;
using System.Net.Sockets;
using System.IO;
using System.Messaging;
using Microsoft.Win32;

namespace NavisionMobile
{
	/// <summary>
	/// Summary description for Synchronization.
	/// </summary>
	public class ServiceDelegator
	{
		private string inQueue;
		private string outQueue;
		private string serverIp;
		private int serverPort;
		private int mode;

		public ServiceDelegator(string outQueue, string inQueue)
		{
			//
			// TODO: Add constructor logic here
			//
			this.outQueue = outQueue;
			this.inQueue = inQueue;
			mode = 0;
		}

		public ServiceDelegator(string serverIp, int serverPort)
		{
			//
			// TODO: Add constructor logic here
			//
			this.serverIp = serverIp;
			this.serverPort = serverPort;
			mode = 1;
		}

		public XmlDocument performService(XmlDocument serviceRequest)
		{
			TcpClient tcpClient = new TcpClient();
			XmlDocument serviceResponse = new XmlDocument();

			System.Guid guid = System.Guid.NewGuid();

			XmlElement documentElement = serviceRequest.DocumentElement;
			XmlElement sessionIdElement = serviceRequest.CreateElement("GUID");
			sessionIdElement.AppendChild(serviceRequest.CreateTextNode(guid.ToString()));
			documentElement.AppendChild(sessionIdElement);

			if (mode == 1)
			{
				try
				{
					tcpClient.Connect(serverIp, serverPort);
					NetworkStream netStream = tcpClient.GetStream();

					serviceRequest.Save(netStream);
					netStream.Flush();

					StreamReader streamReader = new StreamReader(netStream);
					string returnDoc = streamReader.ReadToEnd();

					serviceResponse.LoadXml(returnDoc);

					tcpClient.Close();
				
					return serviceResponse;
				}
				catch(Exception e)
				{
					return generateError("004", e.Message);			
				}
			}
			else
			{
				try
				{

					MSMQ.MSMQQueueInfo msmqInfo = new MSMQ.MSMQQueueInfoClass();
					object msmqTransaction = new MSMQ.MSMQTransactionClass();

					msmqInfo.FormatName = outQueue;
					MSMQ.MSMQQueue msmqQueue = msmqInfo.Open(2,0);

					MSMQ.MSMQMessage message = new MSMQ.MSMQMessageClass();
					message.Label = "NCPXMLREQUEST";
					message.Body = serviceRequest.OuterXml;
					message.Priority = 1;
	    
					message.MaxTimeToReceive = 15000;
   
					message.Send(msmqQueue, ref msmqTransaction);
				
					msmqQueue.Close();

			
					msmqInfo.FormatName = inQueue;
					msmqQueue = msmqInfo.Open(1,0);

					object transaction = false;
					object wantDestinationQueue = false;
					object wantBody = true;
					object wantConnectionType = false;
					object timeOut = 60000;

				
					MSMQ.MSMQMessage outMessage = msmqQueue.PeekCurrent(ref wantDestinationQueue, ref wantBody, ref timeOut, ref wantConnectionType);
					//MSMQ.MSMQMessage outMessage = msmqQueue.Receive(ref transaction, ref wantDestinationQueue, ref wantBody, ref timeOut, ref wantConnectionType);
					if (outMessage != null)
					{
						while(true)	
						{
							if (outMessage == null)
							{
								break;
							}

							serviceResponse.LoadXml((string)outMessage.Body);
							XmlElement performServiceElement = serviceResponse.DocumentElement;
							XmlElement guidElement = (XmlElement)performServiceElement.GetElementsByTagName("GUID").Item(0);
							if (guidElement.FirstChild.Value == guid.ToString())
							{
								msmqQueue.ReceiveCurrent(ref transaction, ref wantDestinationQueue, ref wantBody, ref timeOut, ref wantConnectionType);
								return serviceResponse;
							}

							outMessage = msmqQueue.PeekNext(ref wantDestinationQueue, ref wantBody, ref timeOut, ref wantConnectionType);		
						}
					}
				}	
				catch(Exception e)
				{
					return generateError("002", e.Message);			
				}
			}
			return generateError("001", "");
			

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
				xmlValue = errorDocument.CreateTextNode("Message Queue Exception: "+errorDesc);
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
				xmlValue = errorDocument.CreateTextNode("IP Exception: "+errorDesc);
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

		private bool readConfig(string profileName)
		{
			new System.Security.Permissions.RegistryPermission(
				System.Security.Permissions.PermissionState.Unrestricted).Assert();
			try
			{
				RegistryKey registryKey = Registry.LocalMachine;
				RegistryKey cpKey = registryKey.OpenSubKey(@"SOFTWARE\Navipro\Navision Mobile\"+profileName);

				outQueue = (string)cpKey.GetValue("OutQueue");
				inQueue = (string)cpKey.GetValue("InQueue");
			}
			catch(Exception e)
			{
				return false;
			}
			finally
			{
				System.Security.Permissions.RegistryPermission.RevertAssert();
			}
			return true;		
		}
	}
}
