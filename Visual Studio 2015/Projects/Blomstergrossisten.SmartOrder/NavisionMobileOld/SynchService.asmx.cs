using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.IO;
using System.Configuration;

namespace NavisionMobile
{
	/// <summary>
	/// Summary description for Service1.
	/// </summary>
	/// 
	[WebService(Namespace="http://www.navipro.se/navisionmobile/")]
	public class SynchService : System.Web.Services.WebService
	{
		private ServiceDelegator serviceDelegator;

		private string logOutFile;
		private string logInFile;

		public SynchService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
			string method = ConfigurationSettings.AppSettings["Method"];

			logOutFile = ConfigurationSettings.AppSettings["LogFileIn"];
			logInFile = ConfigurationSettings.AppSettings["LogFileOut"];

			if (method.Equals("MSMQ"))
			{
				string outQueue = ConfigurationSettings.AppSettings["OutQueue"];
				string inQueue = ConfigurationSettings.AppSettings["InQueue"];
				serviceDelegator = new ServiceDelegator(outQueue, inQueue);
			}
			else
			{
				string serverIp = ConfigurationSettings.AppSettings["ServerIp"];
				string serverPort = ConfigurationSettings.AppSettings["ServerPort"];
				serviceDelegator = new ServiceDelegator(serverIp, int.Parse(serverPort));
			}
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

		[WebMethod]
		public void PerformService(string serviceRequest)
		{
			if (logInFile != null)
			{
				System.IO.TextWriter textWriter = File.CreateText(logInFile);
				textWriter.WriteLine(serviceRequest);
				textWriter.Flush();
				textWriter.Close();
			}

			XmlDocument xmlServiceRequest = new XmlDocument();

			xmlServiceRequest.LoadXml(serviceRequest);
			//if (logInFile != null) xmlServiceRequest.Save(logInFile);

			XmlDocument xmlServiceResponse = serviceDelegator.performService(xmlServiceRequest);

			HttpContext currentContext = System.Web.HttpContext.Current;

			HttpResponse currentResponse = currentContext.Response;

			XmlTextWriter xmlTextWriter = new XmlTextWriter(currentResponse.Output);
			xmlServiceResponse.Save(xmlTextWriter);

			if (logOutFile != null) xmlServiceResponse.Save(logOutFile);

			xmlTextWriter.Flush();		

		}

		[WebMethod]
		public string PerformServiceEx(string serviceRequest)
		{
			if (logInFile != null)
			{
				System.IO.TextWriter textWriter = File.CreateText(logInFile);
				textWriter.WriteLine(serviceRequest);
				textWriter.Flush();
				textWriter.Close();
			}

			XmlDocument xmlServiceRequest = new XmlDocument();

			xmlServiceRequest.LoadXml(serviceRequest);
			//if (logInFile != null) xmlServiceRequest.Save(logInFile);

			XmlDocument xmlServiceResponse = serviceDelegator.performService(xmlServiceRequest);

			if (logOutFile != null) xmlServiceResponse.Save(logOutFile);

			return xmlServiceResponse.OuterXml;

		}
	}
}
