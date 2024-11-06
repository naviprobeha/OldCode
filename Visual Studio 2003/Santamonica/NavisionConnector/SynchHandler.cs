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
	public class SynchHandler
	{
		private Logger logger;
		private Connection connection;
		private Configuration configuration;
		private Database database;
		private Thread thread;
		private bool running;

		public SynchHandler(Connection connection, Configuration configuration, Logger logger)
		{
			this.logger = logger;
			this.connection = connection;
			this.configuration = configuration;
			this.database = database;
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
			MSMQTransporter msmqTransporter = new MSMQTransporter(connection, logger);

			log("Started...", 0);

			Organizations organizations = new Organizations();
			
			int j = 10;

			while(running)
			{
				j++;
				if (j >= 10)
				{
					j = 0;

					try
					{

						Quiksilver quikSilver = new Quiksilver();
						quikSilver.Url = configuration.webServiceUrl;

						DataSet organizationDataSet = quikSilver.getOrganizations(connection.code);

						int i = 0;
						while (i < organizationDataSet.Tables[0].Rows.Count)
						{
							XmlDocument xmlDoc = new XmlDocument();
							xmlDoc.LoadXml("<PERFORM_SERVICE><AGENT><ID>"+organizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"</ID><USER_NAME>"+organizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString()+"</USER_NAME><PASSWORD>"+organizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(12).ToString()+"</PASSWORD></AGENT><SERVICE_REQUEST><SERVICE_NAME>synchronization</SERVICE_NAME><SERVICE_ARGUMENT/></SERVICE_REQUEST><GUID>"+System.Guid.NewGuid().ToString()+"</GUID></PERFORM_SERVICE>");

							msmqTransporter.transport(xmlDoc);

							i++;
						}
					}
					catch(Exception e)
					{
						log("Transporter exception: "+e.Message, 2);
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
			logger.write("[SynchHandler "+connection.code+"] "+message, type);
		}
	}
}
