using System;
using System.Xml;
using System.Messaging;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ApplicationServerConnection.
	/// </summary>
	public class ApplicationServerConnection
	{
		private ServiceRequest serviceRequest;
		private Infojet infojetContext;
		private string errorMessage;
		public ServiceResponse serviceResponse;

		public ApplicationServerConnection(Infojet infojetContext, ServiceRequest serviceRequest)
		{
			//
			// TODO: Add constructor logic here
			//
			this.serviceRequest = serviceRequest;
			this.infojetContext = infojetContext;
		}

		public bool processRequest()
		{
			ApplicationServerTransporter transporter = new ApplicationServerTransporter(infojetContext);

			serviceResponse = transporter.transport(serviceRequest);
			errorMessage = transporter.getErrorMessage();
            if (serviceResponse == null)
            {
                return false;
            }

            if (serviceResponse.containsErrors)
            {
                this.errorMessage = serviceResponse.errorMessage;
                return false;
            }
			return true;

		}

		public string getLastError()
		{
			return errorMessage;
		}

	}
}
