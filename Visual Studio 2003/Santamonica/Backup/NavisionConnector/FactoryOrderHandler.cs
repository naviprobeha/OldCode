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
	public class FactoryOrderHandler
	{
		private Logger logger;
		private Configuration configuration;
		private Thread thread;
		private bool running;
		
		public FactoryOrderHandler(Configuration configuration, Logger logger)
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
			connection.code = "FACTORYORDER";
			connection.msmqOutQueue = configuration.msmqFactoryOrderQueue;

			MSMQTransporter msmqTransporter = new MSMQTransporter(connection, logger);

			log("Started...", 0);

	
			int j = 60;

			while(running)
			{
				j++;
				if (j >= 60)
				{
					j = 0;

					try
					{
						Quiksilver quiksilver = new Quiksilver();
						quiksilver.Url = configuration.webServiceUrl;

						DataSet factoryOrderDataSet = quiksilver.getFactoryOrderEntries();

						int i = 0;
						while (i < factoryOrderDataSet.Tables[0].Rows.Count)
						{
							FactoryOrder factoryOrder = new FactoryOrder(factoryOrderDataSet.Tables[0].Rows[i]);

							string userName = "";
							string password = "";

							quiksilver.getOrganizationInfo(configuration.scaleOrganizationNo, ref userName, ref password);


							XmlDocument xmlDoc = new XmlDocument();
							xmlDoc.LoadXml("<PERFORM_SERVICE/>");
							XmlElement requestDocElement = xmlDoc.DocumentElement;
						
							XmlElement agentElement = xmlDoc.CreateElement("AGENT");
							XmlElement idElement = xmlDoc.CreateElement("ID");
							idElement.AppendChild(xmlDoc.CreateTextNode(configuration.scaleOrganizationNo));

							XmlElement userNameElement = xmlDoc.CreateElement("USER_NAME");
							userNameElement.AppendChild(xmlDoc.CreateTextNode(userName));

							XmlElement passwordElement = xmlDoc.CreateElement("PASSWORD");
							passwordElement.AppendChild(xmlDoc.CreateTextNode(password));

							agentElement.AppendChild(idElement);
							agentElement.AppendChild(userNameElement);
							agentElement.AppendChild(passwordElement);

							XmlElement serviceRequestElement = xmlDoc.CreateElement("SERVICE_REQUEST");

							XmlElement serviceNameElement = xmlDoc.CreateElement("SERVICE_NAME");
							serviceNameElement.AppendChild(xmlDoc.CreateTextNode("createFactoryOrder"));

							serviceRequestElement.AppendChild(serviceNameElement);
						
							XmlElement serviceArgumentElement = xmlDoc.CreateElement("SERVICE_ARGUMENT");

							XmlElement factoryOrderElement = factoryOrder.toDOM(xmlDoc);
							serviceArgumentElement.AppendChild(factoryOrderElement);
						
							XmlElement guidElement = xmlDoc.CreateElement("GUID");
							guidElement.AppendChild(xmlDoc.CreateTextNode(System.Guid.NewGuid().ToString()));

							serviceRequestElement.AppendChild(serviceArgumentElement);
							requestDocElement.AppendChild(agentElement);
							requestDocElement.AppendChild(serviceRequestElement);
							requestDocElement.AppendChild(guidElement);


							msmqTransporter.transport(xmlDoc);

							quiksilver.setFactoryOrderEntryStatus(factoryOrder.entryNo, 2);
						
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
			logger.write("[FactoryOrderHandler] "+message, type);
		}
	}
}
