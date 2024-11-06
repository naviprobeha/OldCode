using System;
using System.Xml;
using System.Net;
using System.IO;
using System.Net.Sockets;


namespace SmartInventory
{
	/// <summary>
	/// Summary description for Service.
	/// </summary>
	public class Service
	{
		private ServiceRequest serviceRequestObject;
		private Agent agentObject;

		private Logger logger;
		private DataSetup setup;

		private SmartDatabase smartDatabase;

		public Service(string serviceName, SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;

			serviceRequestObject = new ServiceRequest(serviceName);
			agentObject = new Agent(smartDatabase);

			setup = new DataSetup(smartDatabase);
			
		}

		public ServiceRequest serviceRequest
		{
			set
			{
				serviceRequestObject = value;
			}
			get
			{
				return serviceRequestObject;
			}
		}

		public void setLogger(Logger logger)
		{
			this.logger = logger;
		}

		public ServiceResponse performService()
		{
			if (setup.synchMethod == 1)
			{
				return performServiceTcp();
			}
			else
			{
				return performServiceHttp();
			}
		}

		public ServiceResponse performServiceHttp()
		{
			XmlDocument requestDocument = new XmlDocument();
			XmlDocument responseDocument = new XmlDocument();

			System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

			log("Ansluter till "+setup.host+"...");
			byte[] data = encoding.GetBytes("serviceRequest="+this.toDOM(requestDocument).OuterXml);
	
			//System.Windows.Forms.MessageBox.Show(encoding.GetString(data, 0, data.Length));

			try
			{

				//HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(setup.host);

				se.navipro.dev1.SynchService synchService = new se.navipro.dev1.SynchService();
			
				synchService.Url = setup.host;
			
					
				//webRequest.Method = "POST";
				//webRequest.ContentLength = data.Length;
				//webRequest.ContentType = "application/x-www-form-urlencoded";
				//webRequest.Timeout = 360000;
					

				log("Skickar data...");

				/*
					Stream stream = webRequest.GetRequestStream();
				
					int byteLength, i;
					if (data.Length > 200)
						byteLength = 200;
					else
						byteLength = data.Length;

					i = 0;
					while (i < data.Length)
					{
						stream.Write(data, i, byteLength);
						stream.Flush();
					
						i = i+byteLength;
						if (i + byteLength > data.Length) byteLength = data.Length - i;				
					}

					stream.Close();	
				
					log("Tar emot data...");
			
					HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
					StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
					String responseData = streamReader.ReadToEnd();
					streamReader.Close();			

					*/

				string responseData = synchService.PerformServiceEx(this.toDOM(requestDocument).OuterXml);

				log("Kopplar ifrån...");

				responseDocument.LoadXml(responseData);

				XmlElement serviceContent = responseDocument.DocumentElement;
				serviceContent = (XmlElement)serviceContent.GetElementsByTagName("SERVICE_RESPONSE").Item(0);

				serviceRequestObject.postDOM();
				return new ServiceResponse(serviceContent, smartDatabase, logger);

			}
			catch (Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
			}

			
			return null;
		}

		public ServiceResponse performServiceTcp()
		{
			XmlDocument requestDocument = new XmlDocument();
			XmlDocument responseDocument = new XmlDocument();

			System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

			log("Ansluter till "+setup.host+" (TCP)...");
			byte[] data = encoding.GetBytes("serviceRequest="+this.toDOM(requestDocument).OuterXml);
	
			try
			{
				TcpClient tcpClient = new TcpClient(setup.host, 8999);
				
				log("Skickar data...");

				NetworkStream netStream = tcpClient.GetStream();

				int byteLength, i;
				if (data.Length > 200)
					byteLength = 200;
				else
					byteLength = data.Length;

				i = 0;
				while (i < data.Length)
				{
					netStream.Write(data, i, byteLength);
					netStream.Flush();
					
					i = i+byteLength;
					if (i + byteLength > data.Length) byteLength = data.Length - i;				
				}

				log("Tar emot data...");
			
				StreamReader streamReader = new StreamReader(netStream);
				String responseData = streamReader.ReadToEnd();
				streamReader.Close();			

				log("Kopplar ifrån...");
				tcpClient.Close();

				responseDocument.LoadXml(responseData);

				XmlElement serviceContent = responseDocument.DocumentElement;
				serviceContent = (XmlElement)serviceContent.GetElementsByTagName("SERVICE_RESPONSE").Item(0);

				return new ServiceResponse(serviceContent, smartDatabase, logger);

			}
			catch (Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
				return null;
			}

		

		}

		public bool pingServer()
		{

			System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
			int connectCount = 0;
			bool success = false;

			byte[] data = encoding.GetBytes("serviceRequest=<ping/>");

			while ((connectCount < 3) && (success == false))
			{
				if (connectCount > 0) log("Ansluter till "+setup.host+"... ("+connectCount+")");
				try
				{
					HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(setup.host);
			
					webRequest.Timeout = 10000;
					webRequest.Method = "POST";
					webRequest.ContentLength = data.Length;
					webRequest.ContentType = "application/x-www-form-urlencoded";

					Stream stream = webRequest.GetRequestStream();
				
					int byteLength, i;
					if (data.Length > 200)
						byteLength = 200;
					else
						byteLength = data.Length;

					i = 0;
					while (i < data.Length)
					{
						stream.Write(data, i, byteLength);
						stream.Flush();
					
						i = i+byteLength;
						if (i + byteLength > data.Length) byteLength = data.Length - i;				
					}

					stream.Close();	
				
					HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
					StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
					String responseData = streamReader.ReadToEnd();
					streamReader.Close();			
					success = true;
				}
				catch (Exception e)
				{
					string message = e.Message;
					success = false;
				}

				connectCount++;
			}

			return success;
		}


		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			XmlElement performServiceElement = xmlDocumentContext.CreateElement("PERFORM_SERVICE");
			XmlElement agentElement = agentObject.toDOM(xmlDocumentContext);
			XmlElement serviceRequestElement = serviceRequestObject.toDOM(xmlDocumentContext);

			performServiceElement.AppendChild(agentElement);
			performServiceElement.AppendChild(serviceRequestElement);

			return performServiceElement;
			
		}

		private void log(string message)
		{
			if (logger != null)
				logger.write(message);
		}
	}
}
