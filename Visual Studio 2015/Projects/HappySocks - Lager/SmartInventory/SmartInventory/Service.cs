using System;
using System.Xml;
using System.Net;
using System.IO;


namespace Navipro.SmartInventory
{
	/// <summary>
	/// Summary description for Service.
	/// </summary>
	public class Service
	{
		private ServiceRequest serviceRequestObject;

		private Logger logger;

        private Configuration configuration;
		private SmartDatabase smartDatabase;

		public Service(string serviceName, SmartDatabase smartDatabase, Configuration configuration)
		{
			this.smartDatabase = smartDatabase;

			serviceRequestObject = new ServiceRequest(serviceName);

			this.configuration = configuration;
			
		}


        public ServiceResponse performService()
        {
            XmlDocument requestDocument = new XmlDocument();
            XmlDocument responseDocument = new XmlDocument();


            XmlElement documentElement = this.toDOM(requestDocument);
            requestDocument.AppendChild(documentElement);

            XmlDeclaration xmlDecl = requestDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            requestDocument.InsertBefore(xmlDecl, documentElement);


            log(Translation.translate(configuration.languageCode, "Ansluter till ") + configuration.webServiceUrl + "...");

            //System.Windows.Forms.MessageBox.Show(encoding.GetString(data, 0, data.Length));

            
            try
            {
                navWebService.InfojetServiceRequest webService = new Navipro.SmartInventory.navWebService.InfojetServiceRequest();
                webService.Url = configuration.webServiceUrl + "/ServiceRequest.asmx";

                log(Translation.translate(configuration.languageCode, "Skickar data..."));

                string responseData = webService.performservice(requestDocument.OuterXml);

                log(Translation.translate(configuration.languageCode, "Tar emot data..."));
                log(Translation.translate(configuration.languageCode, "Kopplar ifrån..."));

                responseDocument.LoadXml(responseData);

                XmlElement serviceContent = responseDocument.DocumentElement;

                return new ServiceResponse(serviceContent, smartDatabase, logger);

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
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


		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			XmlElement performServiceElement = xmlDocumentContext.CreateElement("nav");
			XmlElement serviceRequestElement = serviceRequestObject.toDOM(xmlDocumentContext);

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
