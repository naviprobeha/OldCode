using System;
using System.Collections;
using Microsoft.Win32;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Generic;

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

        public List<string> factoryList { get; set; }
        public Dictionary<string, string> factoryCustomerNo { get; set; }
        public Dictionary<string, string> factoryId { get; set; }
        public Dictionary<string, string> customerFactoryCode { get; set; }

        public string errorMessage;

		public Configuration()
		{
            //
            // TODO: Add constructor logic here
            //

            factoryList = new List<string>();
            factoryCustomerNo = new Dictionary<string, string>();
            customerFactoryCode = new Dictionary<string, string>();
            factoryId = new Dictionary<string, string>();
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

                string[] factories = regKey.GetSubKeyNames();
                foreach(string factoryCode in factories)
                {
                    factoryList.Add(factoryCode);

                    RegistryKey factorySubKey = regKey.OpenSubKey(factoryCode);
                    string customerNo = factorySubKey.GetValue("CustomerNo").ToString();

                    string factoryNo = factorySubKey.GetValue("No").ToString();

                    factoryCustomerNo.Add(factoryCode, customerNo);
                    factoryId.Add(factoryCode, factoryNo);
                    customerFactoryCode.Add(customerNo, factoryCode);
                }



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


        public string makeApiCall(string endpoint, string method, string payload)
        {
            WebRequest webRequest = HttpWebRequest.Create("https://smartshipping.workanywhere.se/" + endpoint);
            webRequest.ContentType = "application/json";

            webRequest.Method = method;
            webRequest.Headers.Add("Authorization", "hepphepp");

            if (payload != "")
            {
                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.WriteLine(payload);
                streamWriter.Flush();
                streamWriter.Close();
            }

            ServicePointManager.ServerCertificateValidationCallback = delegate (
                Object obj, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors errors)
            {
                return (true);
            };

            WebResponse response = webRequest.GetResponse();

            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string content = streamReader.ReadToEnd();
            streamReader.Close();

            return content;

        }

        public string makeTestApiCall(string endpoint, string method, string payload)
        {
            WebRequest webRequest = HttpWebRequest.Create("https://test.smartshipping.workanywhere.se/" + endpoint);
            webRequest.ContentType = "application/json";
            webRequest.Method = method;
            webRequest.Headers.Add("Authorization", "hepphepp");

            if (payload != "")
            {
                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.WriteLine(payload);
                streamWriter.Flush();
                streamWriter.Close();
            }

            ServicePointManager.ServerCertificateValidationCallback = delegate (
                Object obj, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors errors)
            {
                return (true);
            };


            WebResponse response = webRequest.GetResponse();

            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string content = streamReader.ReadToEnd();
            streamReader.Close();

            return content;

        }
    }
}
