using System;
using System.Xml;
using System.Data;
using System.Threading;
using System.Collections;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.NavisionConnector
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class NavisionConnector : MSMQRegisteredListener
	{
		private Logger logger;

		private Configuration configuration;
		private ArrayList msmqListeners;
		private ArrayList synchHandlers;
		private ShipmentHandler shipmentHandler;
		private CustomerHandler customerHandler;
		private ScaleHandler scaleHandler;
		private FactoryOrderHandler factoryOrderHandler;

		public NavisionConnector(Logger logger)
		{
			//
			// TODO: Add constructor logic here
			//
			this.logger = logger;

			log("Starting NavisionConnector...", 0);

			this.msmqListeners = new ArrayList();
			this.synchHandlers = new ArrayList();
		}


		public bool start()
		{
			log("Starting handlers...", 0);

			configuration = new Configuration();
			if (!configuration.init())
			{
				logger.write("Configuration failed to load:"+configuration.errorMessage, 2);
				return false;
			}



			int i = 0;
			while (i < configuration.connectionList.Count)
			{
				MSMQListener msmqListener = new MSMQListener((Connection)configuration.connectionList[i], logger);
				msmqListener.registerListener(this);
				msmqListener.start();
				this.msmqListeners.Add(msmqListener);

				SynchHandler synchHandler = new SynchHandler((Connection)configuration.connectionList[i], configuration, logger);
				synchHandler.start();
				this.synchHandlers.Add(synchHandler);

				i++;
			}

			shipmentHandler = new ShipmentHandler(configuration, logger);
			shipmentHandler.start();

			customerHandler = new CustomerHandler(configuration, logger);
			customerHandler.start();

			scaleHandler = new ScaleHandler(configuration, logger);
			scaleHandler.start();

			factoryOrderHandler = new FactoryOrderHandler(configuration, logger);
			factoryOrderHandler.start();

			return true;
			
		}



		public void stop()
		{
			log("Stopping...", 0);

			int i = 0;
			while (i < configuration.connectionList.Count)
			{
				((MSMQListener)this.msmqListeners[i]).stop();
				((SynchHandler)this.synchHandlers[i]).stop();

				i++;
			}

			shipmentHandler.stop();
			customerHandler.stop();

		}

		#region MSMQRegisteredListener Members

		public void msmqDocumentReceived(XmlDocument document)
		{
			// TODO:  Add NavisionConnector.msmqDocumentReceived implementation

			//log("Document received.", 0);

			if (document.DocumentElement != null)
			{
				XmlElement publication = document.DocumentElement;
				if (publication != null)
				{


					try
					{
						Publication publicationObject = new Publication(publication, logger, configuration);
					}
					catch(Exception e)
					{
						log(e.ToString(), 2);
						System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("C:\\"+System.DateTime.Now.ToString("yyyyMMddhhmmss.log"));
						streamWriter.WriteLine(publication.OuterXml);
						streamWriter.WriteLine(e.ToString());
						streamWriter.Flush();
						streamWriter.Close();						
						log("Logged to file.", 2);
					}
				}

			}
			//log("Document processed.", 0);


		}

		#endregion


		public void log(string message, int type)
		{
			logger.write("[NavisionConnector] "+message, type);
		}
	}
}
