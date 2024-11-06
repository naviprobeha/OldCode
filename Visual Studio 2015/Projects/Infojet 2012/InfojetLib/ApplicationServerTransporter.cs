using System;
using System.Net.Sockets;
using System.IO;
using System.Xml;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for TransporterTCP.
	/// </summary>
	public class ApplicationServerTransporter
	{
		private Infojet infojetContext;
		private string errorMessage;

        public ApplicationServerTransporter(Infojet infojetContext)
		{
			//
			// TODO: Add constructor logic here
			//
			this.infojetContext = infojetContext;
		}

		public ServiceResponse transport(ServiceRequest serviceRequest)
		{
            this.errorMessage = "";

            Navipro.Infojet.Lib.InfojetServiceRequest.InfojetServiceRequest webServiceRequest = new Navipro.Infojet.Lib.InfojetServiceRequest.InfojetServiceRequest();

            webServiceRequest.Url = infojetContext.configuration.wsAddress;
            
			string returnDoc = webServiceRequest.performservice(serviceRequest.getDocument().OuterXml);

            
            if (returnDoc == null)
            {
                errorMessage = "GENERAL ERROR";
                return null;
            }

			XmlDocument responseDocument = new XmlDocument();
			responseDocument.LoadXml(returnDoc);

			ServiceResponse serviceResponse = new ServiceResponse(responseDocument);
			return serviceResponse;
                     
		}

		public string getErrorMessage()
		{
			// TODO:  Add TransporterTCP.getErrorMessage implementation
			return errorMessage;
		}

	}
}
