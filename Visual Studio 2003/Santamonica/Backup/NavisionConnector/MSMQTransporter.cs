using System;
using System.Xml;
using System.Messaging;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.NavisionConnector
{
	/// <summary>
	/// Summary description for MSMQHandler.
	/// </summary>
	public class MSMQTransporter
	{
		private Logger logger;
		private Connection connection;

		public MSMQTransporter(Connection connection, Logger logger)
		{
			//
			// TODO: Add constructor logic here
			//

			this.logger = logger;
			this.connection = connection;

		}

		public void transport(XmlDocument xmlDoc)
		{
			//log("Transporting message to queue: "+connection.msmqOutQueue, 0);
		
			try
			{

				MSMQ.MSMQQueueInfo msmqInfo = new MSMQ.MSMQQueueInfoClass();
				object msmqTransaction = new MSMQ.MSMQTransactionClass();

				msmqInfo.FormatName = "DIRECT=OS:"+connection.msmqOutQueue;
				MSMQ.MSMQQueue msmqQueue = msmqInfo.Open(2,0);

				MSMQ.MSMQMessage message = new MSMQ.MSMQMessageClass();
				message.Label = "NCPXMLREQUEST";
				message.Body = xmlDoc.OuterXml;
				message.Priority = 1;
	     
				message.Send(msmqQueue, ref msmqTransaction);
				
				msmqQueue.Close();

			}
			catch(Exception e)
			{
				log("Msmq Error: "+e.Message, 1);

			}

		}



		public void transportNET(XmlDocument xmlDoc)
		{
			//log("Transporting message to queue: "+connection.msmqOutQueue, 0);
		
			try
			{
				MessageQueue msmq = new MessageQueue("FORMATNAME:DIRECT=OS:"+connection.msmqOutQueue);

				try
				{
					msmq.Send(xmlDoc, "Navision MSMQ-BA");
					//log("Message transported...", 0);

				}
				catch(MessageQueueException msmqE)
				{
					log("MSMQ Error: "+msmqE.Message, 1);
				}
				catch(Exception e)
				{
					log("Error: "+e.Message, 1);
				}

			}
			catch(Exception e)
			{
				log("Msmq Error: "+e.Message, 1);

			}

		}

		private void log(string message, int level)
		{
			logger.write("[MSMQTransporter "+connection.code+"] "+message, level);
		}

	}
}
