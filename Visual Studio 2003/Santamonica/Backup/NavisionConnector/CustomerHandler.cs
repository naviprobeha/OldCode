using System;
using System.Xml;
using System.Data;
using System.Threading;
using Navipro.SantaMonica.Common;
using NavisionConnector.se.navipro.dev1;

namespace Navipro.SantaMonica.NavisionConnector
{
	/// <summary>
	/// Summary description for SynchHandler.
	/// </summary>
	public class CustomerHandler
	{
		private Logger logger;
		private Configuration configuration;
		private Thread thread;
		private bool running;

		public CustomerHandler(Configuration configuration, Logger logger)
		{
			this.logger = logger;
			this.configuration = configuration;
			//
			// TODO: Add constructor logic here
			//

			thread = new Thread(new ThreadStart(run));
			thread.Start();

		}

		public void start()
		{
			running = true;
		}

		public void run()
		{
			Connection connection = new Connection();
			connection.code = "CUSTOMER";
			connection.msmqOutQueue = configuration.msmqShipmentQueue;

			MSMQTransporter msmqTransporter = new MSMQTransporter(connection, logger);

			log("Started...", 0);

		
			int j = 10;

			while(running)
			{
				j++;
				if (j >= 10)
				{
					j = 0;

					try
					{
						Quiksilver quiksilver = new Quiksilver();
						quiksilver.Url = configuration.webServiceUrl;

						DataSet customerDataSet = quiksilver.getUpdatedCustomers();

						int i = 0;
						while (i < customerDataSet.Tables[0].Rows.Count)
						{
							DataRow customerDataRow = customerDataSet.Tables[0].Rows[i];
							Customer customer = new Customer(customerDataRow);
                      

							string userName = "";
							string password = "";

							if (customer.organizationNo != "")
							{
						

								quiksilver.getOrganizationInfo(customer.organizationNo, ref userName, ref password);


								XmlDocument xmlDoc = new XmlDocument();
								xmlDoc.LoadXml("<PERFORM_SERVICE/>");
								XmlElement requestDocElement = xmlDoc.DocumentElement;
						
								XmlElement agentElement = xmlDoc.CreateElement("AGENT");
								XmlElement idElement = xmlDoc.CreateElement("ID");
								idElement.AppendChild(xmlDoc.CreateTextNode(customer.organizationNo));

								XmlElement userNameElement = xmlDoc.CreateElement("USER_NAME");
								userNameElement.AppendChild(xmlDoc.CreateTextNode(userName));

								XmlElement passwordElement = xmlDoc.CreateElement("PASSWORD");
								passwordElement.AppendChild(xmlDoc.CreateTextNode(password));

								agentElement.AppendChild(idElement);
								agentElement.AppendChild(userNameElement);
								agentElement.AppendChild(passwordElement);

								XmlElement serviceRequestElement = xmlDoc.CreateElement("SERVICE_REQUEST");

								XmlElement serviceNameElement = xmlDoc.CreateElement("SERVICE_NAME");
								serviceNameElement.AppendChild(xmlDoc.CreateTextNode("updateCustomer"));

								serviceRequestElement.AppendChild(serviceNameElement);
						
								XmlElement serviceArgumentElement = xmlDoc.CreateElement("SERVICE_ARGUMENT");

								XmlElement customerElement = customer.toDOM(xmlDoc);
                        
								serviceArgumentElement.AppendChild(customerElement);
						
								XmlElement guidElement = xmlDoc.CreateElement("GUID");
								guidElement.AppendChild(xmlDoc.CreateTextNode(System.Guid.NewGuid().ToString()));

								serviceRequestElement.AppendChild(serviceArgumentElement);
								requestDocElement.AppendChild(agentElement);
								requestDocElement.AppendChild(serviceRequestElement);
								requestDocElement.AppendChild(guidElement);


								msmqTransporter.transport(xmlDoc);
						
							}

							quiksilver.setCustomerUpdated(customer.organizationNo, customer.no, false);

							i++;
					
						}
					}
					catch(Exception e)
					{
						log("Web Service Exception: "+e.Message, 2);

					}

				}

				Thread.Sleep(1000);
			}

			log("Stopped...", 0);

		}

		public void stop()
		{
			running = false;
		}

		private void log(string message, int type)
		{
			logger.write("[CustomerHandler] "+message, type);
		}
	}
}
