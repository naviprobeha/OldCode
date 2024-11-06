using System;
using System.Xml;
using System.Net;
using System.IO;


namespace SmartShipment
{
	/// <summary>
	/// Summary description for Service.
	/// </summary>
	public class Service
	{
		private ServiceRequest serviceRequestObject;
		private Agent agentObject;

		private Logger logger;
		private DataSetup dataSetup;

		private SmartDatabase smartDatabase;

		public Service(string serviceName, SmartDatabase smartDatabase, DataSetup dataSetup)
		{
			this.smartDatabase = smartDatabase;

			serviceRequestObject = new ServiceRequest(serviceName);

			this.dataSetup = dataSetup;
			agentObject = dataSetup.getAgent();
			
		}


		public ServiceResponse performService()
		{
			XmlDocument requestDocument = new XmlDocument();
			XmlDocument responseDocument = new XmlDocument();

			XmlElement documentElement = this.toDOM(requestDocument);
			requestDocument.AppendChild(documentElement);

			XmlDeclaration xmlDecl = requestDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
			requestDocument.InsertBefore(xmlDecl, documentElement);

			log("Ansluter till "+dataSetup.host+"...");
	
			//System.Windows.Forms.MessageBox.Show(encoding.GetString(data, 0, data.Length));


			if (pingServer())
			{
				try
				{
					se.navipro.dev1.SynchService synchService = new SmartShipment.se.navipro.dev1.SynchService();
					synchService.Url = dataSetup.host+"/NavisionMobile/SynchService.asmx";

					log("Skickar data...");

					string responseData = synchService.PerformService(requestDocument.OuterXml);
				
					log("Tar emot data...");		
					log("Kopplar ifrån...");

					responseDocument.LoadXml(responseData);

					XmlElement serviceContent = responseDocument.DocumentElement;
					serviceContent = (XmlElement)serviceContent.GetElementsByTagName("SERVICE_RESPONSE").Item(0);

					return new ServiceResponse(serviceContent, smartDatabase, logger);

				}
				catch (Exception e)
				{
					System.Windows.Forms.MessageBox.Show(e.Message);
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Servern hittades inte.");
			}

			return null;
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

		public ServiceResponse performService_old()
		{
			XmlDocument requestDocument = new XmlDocument();
			XmlDocument responseDocument = new XmlDocument();

			XmlElement documentElement = this.toDOM(requestDocument);
			requestDocument.AppendChild(documentElement);

			XmlDeclaration xmlDecl = requestDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
			requestDocument.InsertBefore(xmlDecl, documentElement);

			System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

			log("Ansluter till "+dataSetup.host+"...");
			byte[] data = encoding.GetBytes("serviceRequest="+requestDocument.OuterXml);
	
			//System.Windows.Forms.MessageBox.Show(encoding.GetString(data, 0, data.Length));


			if (pingServer())
			{
				try
				{

					HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(dataSetup.host+"/NavisionMobile/SynchService.asmx/PerformService");
			
					webRequest.Method = "POST";
					webRequest.ContentLength = data.Length;
					webRequest.ContentType = "application/x-www-form-urlencoded";
					webRequest.Timeout = 600000;

					log("Skickar data...");

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

					log("Kopplar ifrån...");

					responseDocument.LoadXml(responseData);

					XmlElement serviceContent = responseDocument.DocumentElement;
					serviceContent = (XmlElement)serviceContent.GetElementsByTagName("SERVICE_RESPONSE").Item(0);

					return new ServiceResponse(serviceContent, smartDatabase, logger);

				}
				catch (Exception e)
				{
					System.Windows.Forms.MessageBox.Show(e.Message);
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Servern hittades inte.");
			}

			return null;
		}

		public bool pingServer()
		{

			int connectCount = 0;
			bool success = false;


			while ((connectCount < 3) && (success == false))
			{
				if (connectCount > 0) log("Ansluter till "+dataSetup.host+"... ("+connectCount+")");
				try
				{
					se.navipro.dev1.SynchService synchService = new SmartShipment.se.navipro.dev1.SynchService();
					synchService.Url = dataSetup.host+"/NavisionMobile/SynchService.asmx";

					string responseData = synchService.PerformService("<ping/>");
				
					success = true;
				}
				catch (Exception e)
				{
					//System.Windows.Forms.MessageBox.Show(e.Message);
					success = false;
				}

				connectCount++;
			}

			return success;
		}

		public bool pingServer_old()
		{

			System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
			int connectCount = 0;
			bool success = false;

			byte[] data = encoding.GetBytes("serviceRequest=<ping/>");

			while ((connectCount < 3) && (success == false))
			{
				if (connectCount > 0) log("Ansluter till "+dataSetup.host+"... ("+connectCount+")");
				try
				{
					HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(dataSetup.host+"/NavisionMobile/SynchService.asmx/PerformService");
			
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
					//System.Windows.Forms.MessageBox.Show(e.Message);
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
