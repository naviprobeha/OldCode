using System;
using System.Collections;
using Microsoft.Win32;

namespace Navipro.SantaMonica.ScaleControl
{
	/// <summary>
	/// Summary description for Configuration.
	/// </summary>
	public class Configuration
	{
		private string webServiceUrlValue;
		private string serverNameValue;
		private string dataSourceValue;
		private string userNameValue;
		private string passwordValue;
		private string factoryCodeValue;
		private string customerNoValue;
		private string printValue;
		private string printCommandValue;
		private string printDocumentUrlValue;
		private string printerValue;
		private string viktoriaPrefixValue;
		private string combinedContainersValue;

		public string errorMessage;

		public Configuration()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public bool init()
		{
			try
			{
				RegistryKey regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Navipro\\SantaMonica\\Scale Control");
				webServiceUrlValue = regKey.GetValue("WebServiceUrl").ToString();
				factoryCodeValue = regKey.GetValue("FactoryCode").ToString();
				customerNoValue = regKey.GetValue("CustomerNo").ToString();
				printValue = regKey.GetValue("Print").ToString();
				printCommandValue = regKey.GetValue("PrintCommand").ToString();
				printDocumentUrlValue = regKey.GetValue("PrintDocumentUrl").ToString();
				printerValue = regKey.GetValue("Printer").ToString();
				combinedContainersValue = regKey.GetValue("CombinedContainers").ToString();

				regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Navipro\\SantaMonica\\Viktoria Connection");
				serverNameValue = regKey.GetValue("Servername").ToString();
				dataSourceValue = regKey.GetValue("Datasource").ToString();
				userNameValue = regKey.GetValue("Username").ToString();
				passwordValue = regKey.GetValue("Password").ToString();
				viktoriaPrefixValue = regKey.GetValue("Prefix").ToString();

				return true;
			}
			catch(Exception e)
			{
				errorMessage = e.Message + " ("+e.StackTrace+")";
				return false;
			}
		}

		public string webServiceUrl
		{
			get
			{
				return webServiceUrlValue;
			}
		}

		public string serverName
		{
			get
			{
				return serverNameValue;
			}
		}

		public string dataSource
		{
			get
			{
				return dataSourceValue;
			}
		}

		public string userName
		{
			get
			{
				return userNameValue;
			}
		}

		public string password
		{
			get
			{
				return passwordValue;
			}
		}

		public string factoryCode
		{
			get
			{
				return factoryCodeValue;
			}
		}

		public string customerNo
		{
			get
			{
				return customerNoValue;
			}
		}

		public bool print
		{
			get
			{
				if (printValue == "1") return true;
				return false;
			}
		}

		public string printDocumentUrl
		{
			get
			{
				return printDocumentUrlValue;
			}
		}

		public string printer
		{
			get
			{
				return printerValue;
			}
		}

		public string printCommand
		{
			get
			{
				return printCommandValue;
			}
		}

		public string viktoriaPrefix
		{
			get
			{
				return viktoriaPrefixValue;
			}
		}

		public bool combinedContainers
		{
			get
			{
				if (this.combinedContainersValue == "1") return true;
				return false;
			}
		}
	}
}
