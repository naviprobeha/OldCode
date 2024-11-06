using System;
using System.Xml;
using System.Threading;
using System.Messaging;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.NavisionConnector
{
	/// <summary>
	/// Summary description for MSMQListener.
	/// </summary>
	public class MSMQListener
	{
		private Logger logger;
		private Connection connection;

		private bool running;

		private System.Collections.ArrayList listeners;

		public MSMQListener(Connection connection, Logger logger)
		{
			//
			// TODO: Add constructor logic here
			//

			this.logger = logger;
			this.connection = connection;

			listeners = new System.Collections.ArrayList();
		}

		public void start()
		{
			Thread thread = new Thread(new ThreadStart(run));

			running = true;
			thread.Start();
		}

		public void run()
		{
			log("MSMQ Listener started. Listening to queue: "+connection.msmqInQueue, 0);
		
			MSMQ.MSMQQueueInfo msmqInfo = new MSMQ.MSMQQueueInfoClass();
			object msmqTransaction = new MSMQ.MSMQTransactionClass();

			try
			{

				while(running)
				{
					msmqInfo.FormatName = "DIRECT=OS:"+connection.msmqInQueue;
					MSMQ.MSMQQueue msmqQueue = msmqInfo.Open(1,0);

					object transaction = false;
					object wantDestinationQueue = false;
					object wantBody = true;
					object wantConnectionType = false;
					object timeOut = 10000;

				
					MSMQ.MSMQMessage outMessage = msmqQueue.Receive(ref transaction, ref wantDestinationQueue, ref wantBody, ref timeOut, ref wantConnectionType);
					if (outMessage != null)
					{

		
						XmlDocument xmlDoc = new XmlDocument();
						xmlDoc.LoadXml((string)outMessage.Body);
						
						try
						{
							notifyListeners(xmlDoc);
						}
						catch(Exception e)
						{
							log("Data error: "+e.Message, 2);
						}
					
					}
				}
			}
			catch(Exception e)
			{
				log("Msmq Error: "+e.Message, 1);
			}

			log("MSMQ Listener stopped.", 0);


		}

		public void runOld()
		{
			log("MSMQ Listener started. Listening to queue: "+connection.msmqInQueue, 0);
		
			//try
			//{
				MessageQueue msmq = new MessageQueue("FORMATNAME:DIRECT=OS:"+connection.msmqInQueue);

				while(running)
				{

					try
					{
						System.Messaging.Message message = msmq.Receive(System.TimeSpan.FromSeconds(30));
						if (message != null)
						{

							System.IO.StreamReader streamReader = new System.IO.StreamReader(message.BodyStream);
							string document = streamReader.ReadToEnd();
			
							XmlDocument xmlDoc = new XmlDocument();
							xmlDoc.LoadXml(document);
							try
							{
								notifyListeners(xmlDoc);
							}
							catch(Exception e)
							{
								log("Data error: "+e.Message, 2);
							}

						}
					}
					catch(MessageQueueException msmqE)
					{
						if (!msmqE.Message.Substring(0, 7).Equals("Timeout"))
						{
							log("MSMQ Error: "+msmqE.Message, 1);
						}
						else
						{
							log("MSMQ Error: "+msmqE.Message, 1);
						}
					}
					catch(Exception e)
					{
						log("Error: "+e.Message, 1);
					}

					Thread.Sleep(2000);	
				}
			//}
			//catch(Exception e)
			//{
			//	log("Msmq Error: "+e.Message, 1);
			//}

			log("MSMQ Listener stopped.", 0);
		}


		public void log(string message, int type)
		{
			logger.write("[MSMQListener "+connection.code+"] "+message, type);
		}

		public void registerListener(MSMQRegisteredListener listener)
		{
			listeners.Add(listener);
		}

		private void notifyListeners(XmlDocument document)
		{
			int i = 0;
			while(i < listeners.Count)
			{
				((MSMQRegisteredListener)listeners[i]).msmqDocumentReceived(document);
				i++;
			}
		}

		public void stop()
		{
			log("Stopping...", 0);
			running = false;
		}
		
	}
}
