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
	public class ShipmentHandler
	{
		private Logger logger;
		private Configuration configuration;
		private Thread thread;
		private bool running;

		public ShipmentHandler(Configuration configuration, Logger logger)
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
			connection.code = "SHIPMENT";
			connection.msmqOutQueue = configuration.msmqShipmentQueue;

			MSMQTransporter msmqTransporter = new MSMQTransporter(connection, logger);

			log("Started...", 0);

			Organizations organizations = new Organizations();
			
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

						DataSet shipmentDataSet = quiksilver.getAvailableShipments();

						int i = 0;
						while (i < shipmentDataSet.Tables[0].Rows.Count)
						{
							DataRow shipmentDataRow = shipmentDataSet.Tables[0].Rows[i];
							ShipmentHeader shipmentHeader = new ShipmentHeader(shipmentDataRow);
                      
							DataSet shipmentLineDataSet = quiksilver.getShipmentLines(shipmentHeader.no);

						

							string userName = "";
							string password = "";

							if (shipmentHeader.organizationNo != "")
							{
						

								quiksilver.getOrganizationInfo(shipmentHeader.organizationNo, ref userName, ref password);



								XmlDocument xmlDoc = new XmlDocument();
								xmlDoc.LoadXml("<PERFORM_SERVICE/>");
								XmlElement requestDocElement = xmlDoc.DocumentElement;
						
								XmlElement agentElement = xmlDoc.CreateElement("AGENT");
								XmlElement idElement = xmlDoc.CreateElement("ID");
								idElement.AppendChild(xmlDoc.CreateTextNode(shipmentHeader.organizationNo));

								XmlElement userNameElement = xmlDoc.CreateElement("USER_NAME");
								userNameElement.AppendChild(xmlDoc.CreateTextNode(userName));

								XmlElement passwordElement = xmlDoc.CreateElement("PASSWORD");
								passwordElement.AppendChild(xmlDoc.CreateTextNode(password));

								agentElement.AppendChild(idElement);
								agentElement.AppendChild(userNameElement);
								agentElement.AppendChild(passwordElement);

								XmlElement serviceRequestElement = xmlDoc.CreateElement("SERVICE_REQUEST");

								XmlElement serviceNameElement = xmlDoc.CreateElement("SERVICE_NAME");
								serviceNameElement.AppendChild(xmlDoc.CreateTextNode("createShipment"));

								serviceRequestElement.AppendChild(serviceNameElement);
						
								XmlElement serviceArgumentElement = xmlDoc.CreateElement("SERVICE_ARGUMENT");

								XmlElement shipmentElement = shipmentHeader.toDOM(xmlDoc);
								XmlElement shipmentLinesElement = xmlDoc.CreateElement("SHIPMENT_LINES");
                        
								int x = 0;
								while(x < shipmentLineDataSet.Tables[0].Rows.Count)
								{
									DataRow shipmentLineDataRow = shipmentLineDataSet.Tables[0].Rows[x];
									ShipmentLine shipmentLine = new ShipmentLine(shipmentLineDataRow);
									XmlElement shipmentLineElement = shipmentLine.toDOM(xmlDoc);
	
									DataSet shipmentLineIdDataSet = quiksilver.getShipmentLineIds(shipmentHeader.no, shipmentLine.originalEntryNo);
							
									XmlElement shipmentLineIdsElement = xmlDoc.CreateElement("SHIPMENT_LINE_IDS");

									int y = 0;
									while(y < shipmentLineIdDataSet.Tables[0].Rows.Count)
									{
										DataRow shipmentLineIdDataRow = shipmentLineIdDataSet.Tables[0].Rows[y];
										ShipmentLineId shipmentLineId = new ShipmentLineId(shipmentLineIdDataRow);
										shipmentLineIdsElement.AppendChild(shipmentLineId.toDOM(xmlDoc));

										y++;
									}

									shipmentLineElement.AppendChild(shipmentLineIdsElement);
									shipmentLinesElement.AppendChild(shipmentLineElement);

									x++;
								}
					

								shipmentElement.AppendChild(shipmentLinesElement);
								serviceArgumentElement.AppendChild(shipmentElement);
						
								XmlElement guidElement = xmlDoc.CreateElement("GUID");
								guidElement.AppendChild(xmlDoc.CreateTextNode(System.Guid.NewGuid().ToString()));

								serviceRequestElement.AppendChild(serviceArgumentElement);
								requestDocElement.AppendChild(agentElement);
								requestDocElement.AppendChild(serviceRequestElement);
								requestDocElement.AppendChild(guidElement);


								msmqTransporter.transport(xmlDoc);
						
							}

							quiksilver.setShipmentStatus(shipmentHeader.no, 2);

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
			logger.write("[ShipmentHandler] "+message, type);
		}
	}
}
