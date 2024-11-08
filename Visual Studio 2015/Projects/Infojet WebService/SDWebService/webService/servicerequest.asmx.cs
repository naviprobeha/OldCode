using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Xml;
using System.Web.Services;

namespace Navipro.Infojet.WebService
{
	/// <summary>
	/// Summary description for Service1.
	/// </summary>
	[WebService(Namespace="http://navipro.se/infojet/webservices")]
	public class ServiceRequest : System.Web.Services.WebService
	{
		public ServiceRequest()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		// WEB SERVICE EXAMPLE
		// The HelloWorld() example service returns the string Hello World
		// To build, uncomment the following lines then save and build the project
		// To test this web service, press F5

		[WebMethod]
		public string performservice(string xmlDoc)
		{
			ServiceDelegator serviceDelegator = new ServiceDelegator();

			XmlDocument xmlDocIn = new XmlDocument();
			xmlDocIn.LoadXml(xmlDoc);
			XmlDocument xmlDocOut = serviceDelegator.transport(xmlDocIn);			
			if (xmlDocOut != null) return xmlDocOut.OuterXml;

			return null;
		}
	}
}
