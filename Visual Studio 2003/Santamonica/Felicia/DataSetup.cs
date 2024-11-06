using System;
using System.Xml;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataSetup.
	/// </summary>
	public class DataSetup
	{
		private string hostValue;
		private string agentIdValue;
		private int synchIntervalValue;
		private int gpsComPortValue;
		private int gpsBaudRateValue;
		private int shipmentIdentitySeedValue;
		private int invoiceIdentitySeedValue;
		private int defaultCustomerSearchTypeValue;
		private int applicationModeValue;
		private string navigatorPathValue;
		private bool hangUpConnectionsValue;
		private string databasePathValue;

		private bool createShipOrderValue;

		private string mapServerUrlValue;

		private string printerValue;
		
		private SmartDatabase smartDatabase;
		
		public DataSetup()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSetup(SmartDatabase smartDatabase)
		{
			hostValue = "";
			agentIdValue = "";

			this.smartDatabase = smartDatabase;
			refresh();

		}

		public void refresh()
		{

			getConfiguration();

		}


		public void save()
		{
			try
			{
				SqlCeDataReader dataReader = smartDatabase.query("SELECT * FROM setup");

				if (!dataReader.Read())
				{
					smartDatabase.nonQuery("INSERT INTO setup (primaryKey, host, agentId, synchInterval) VALUES (1, '"+host+"','"+agentId+"', '"+synchInterval+"')");
				}
				else
				{
					smartDatabase.nonQuery("UPDATE setup SET host = '"+host+"', agentId = '"+agentId+"', synchInterval = '"+synchInterval+"' WHERE primaryKey = "+dataReader.GetValue(0));
					dataReader.Close();
				}
				dataReader.Dispose();

			}
			catch (SqlCeException e) 
			{
				smartDatabase.ShowErrors(e);
			}

		}

		public string host
		{
			set
			{
				hostValue = value;
			}
			get
			{
				return hostValue;
			}
		}

		public string agentId
		{
			set
			{
				agentIdValue = value;
			}
			get
			{
				return agentIdValue;
			}
		}

		public int synchInterval
		{
			set
			{
				synchIntervalValue = value;
			}
			get
			{
				return synchIntervalValue;
			}
		}

		public bool createShipOrder
		{
			get
			{
				return createShipOrderValue;
			}
		}

		public int gpsComPort
		{
			get
			{
				return gpsComPortValue;
			}
		}

		public int gpsBaudRate
		{
			get
			{
				return gpsBaudRateValue;
			}
		}

		public int shipmentIdentitySeed
		{
			get
			{
				return shipmentIdentitySeedValue;
			}
		}

		public int defaultCustomerSearchType
		{
			get
			{
				return defaultCustomerSearchTypeValue;
			}
		}

		public string mapServerUrl
		{
			get
			{
				return mapServerUrlValue;
			}
		}

		public int applicationMode
		{
			get
			{
				return applicationModeValue;
			}
		}

		public string navigatorPath
		{
			get
			{
				return navigatorPathValue;
			}

		}

		public string printer
		{
			get
			{
				return printerValue;
			}

		}

		public bool hangUpConnections
		{
			get
			{
				return hangUpConnectionsValue;
			}
		}

		public string databasePath
		{
			get
			{
				return databasePathValue;
			}
		}

		public int invoiceIdentitySeed
		{
			get
			{
				return invoiceIdentitySeedValue;
			}
		}

		private bool getConfiguration()
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load("\\Program Files\\Felicia\\config.xml");

				XmlElement docElement = xmlDoc.DocumentElement;

				this.host = docElement.GetElementsByTagName("url").Item(0).FirstChild.Value;
				this.agentId = docElement.GetElementsByTagName("agentId").Item(0).FirstChild.Value;
				this.synchInterval = int.Parse(docElement.GetElementsByTagName("synchInterval").Item(0).FirstChild.Value);
				this.gpsComPortValue = int.Parse(docElement.GetElementsByTagName("gpsComPort").Item(0).FirstChild.Value);
				this.gpsBaudRateValue = int.Parse(docElement.GetElementsByTagName("gpsBaudRate").Item(0).FirstChild.Value);
				

				//ShipOrder
				XmlNodeList nodeList = docElement.GetElementsByTagName("createShipOrder");
				if (nodeList.Count > 0)
				{
					if (nodeList.Item(0).FirstChild.Value == "true") this.createShipOrderValue = true;
				}

				//ShipmentIdentitySeed
				nodeList = docElement.GetElementsByTagName("shipmentIdentitySeed");
				if (nodeList.Count > 0)
				{
					this.shipmentIdentitySeedValue = int.Parse(docElement.GetElementsByTagName("shipmentIdentitySeed").Item(0).FirstChild.Value);
				}

				//InvoiceIdentitySeed
				nodeList = docElement.GetElementsByTagName("invoiceIdentitySeed");
				if (nodeList.Count > 0)
				{
					this.invoiceIdentitySeedValue = int.Parse(docElement.GetElementsByTagName("invoiceIdentitySeed").Item(0).FirstChild.Value);
				}

				//CustomerSearchType
				nodeList = docElement.GetElementsByTagName("customerSearchType");
				if (nodeList.Count > 0)
				{
					this.defaultCustomerSearchTypeValue = int.Parse(docElement.GetElementsByTagName("customerSearchType").Item(0).FirstChild.Value);
				}

				//CustomerSearchType
				nodeList = docElement.GetElementsByTagName("mapServer");
				if (nodeList.Count > 0)
				{
					this.mapServerUrlValue = docElement.GetElementsByTagName("mapServer").Item(0).FirstChild.Value;
				}

				//ApplicationMode
				this.applicationModeValue = 0;
				nodeList = docElement.GetElementsByTagName("applicationMode");
				if (nodeList.Count > 0)
				{
					this.applicationModeValue = int.Parse(docElement.GetElementsByTagName("applicationMode").Item(0).FirstChild.Value);
				}

				//NavigatorPath
				nodeList = docElement.GetElementsByTagName("navigatorPath");
				if (nodeList.Count > 0)
				{
					this.navigatorPathValue = docElement.GetElementsByTagName("navigatorPath").Item(0).FirstChild.Value;
				}

				//Printer
				nodeList = docElement.GetElementsByTagName("printer");
				if (nodeList.Count > 0)
				{
					this.printerValue = docElement.GetElementsByTagName("printer").Item(0).FirstChild.Value;
				}

				//HangUp Connections
				this.hangUpConnectionsValue = true;
				nodeList = docElement.GetElementsByTagName("hangUpConnections");
				if (nodeList.Count > 0)
				{

					if (docElement.GetElementsByTagName("hangUpConnections").Item(0).FirstChild.Value == "0") this.hangUpConnectionsValue = false;
				}

				//Database path
				this.databasePathValue = "";
				nodeList = docElement.GetElementsByTagName("databasePath");
				if (nodeList.Count > 0)
				{

					databasePathValue = docElement.GetElementsByTagName("databasePath").Item(0).FirstChild.Value;
				}

				return true;
			}
			catch(Exception e)
			{
				if (e.Message != "") {}

			}

			return false;
		}

	}
}
