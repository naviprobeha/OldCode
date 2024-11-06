using System;
using System.Collections;
using Microsoft.Win32;

namespace Navipro.SantaMonica.NavisionConnector
{
	/// <summary>
	/// Summary description for Configuration.
	/// </summary>
	public class Configuration
	{
		public ArrayList connectionList;
		public string msmqShipmentQueue;
		public string msmqScaleQueue;
		public string msmqFactoryOrderQueue;
		public string webServiceUrl;
		public string scaleOrganizationNo;

		public string errorMessage;

		public Configuration()
		{
			//
			// TODO: Add constructor logic here
			//
			connectionList = new ArrayList();
		}

		public bool init()
		{
			try
			{
				RegistryKey regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Navipro\\SantaMonica\\NavisionConnector");
				msmqShipmentQueue = regKey.GetValue("MSMQShipmentQueue").ToString();
				msmqScaleQueue = regKey.GetValue("MSMQScaleQueue").ToString();
				msmqFactoryOrderQueue = regKey.GetValue("MSMQFactoryOrderQueue").ToString();
				scaleOrganizationNo = regKey.GetValue("ScaleOrganizationNo").ToString();
				webServiceUrl = regKey.GetValue("WebServiceUrl").ToString();

				string[] subKeys = regKey.GetSubKeyNames();
				int i = 0;
				while (i < subKeys.Length)
				{
					Connection connection = new Connection();
					connection.code = subKeys[i].ToString();

					RegistryKey subKey = regKey.OpenSubKey(subKeys[i].ToString());
					connection.msmqInQueue = subKey.GetValue("MSMQInQueue").ToString();
					connection.msmqOutQueue = subKey.GetValue("MSMQOutQueue").ToString();

					this.connectionList.Add(connection);

					i++;
				}

				return true;
			}
			catch(Exception e)
			{
				errorMessage = e.Message + " ("+e.StackTrace+")";
				return false;
			}
		}
	}
}
