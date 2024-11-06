using System;
using System.Data;
using Navipro.BroadWorks.Lib;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.JobEngine
{
	/// <summary>
	/// Summary description for CallOperator.
	/// </summary>
	public class OperatorCallClient : BroadWorks.Lib.CallClientListener, BroadWorks.Lib.Logger
	{
		private Navipro.SantaMonica.Common.Logger logger;
		private Configuration configuration;

		private UserOperator userOperator;

		private bool offHook;

		private BroadWorks.Lib.CallClient bwCallClient;


		public OperatorCallClient(Navipro.SantaMonica.Common.Logger logger, Configuration configuration, UserOperator userOperator, string serverName, int port)
		{
			//
			// TODO: Add constructor logic here
			//
			this.logger = logger;
			this.configuration = configuration;
			
			this.userOperator = userOperator;

			log("Starting call client.", 1);

			bwCallClient = new Navipro.BroadWorks.Lib.CallClient(serverName, port, userOperator.phoneUserId, userOperator.phonePassword, this, "CC-"+userOperator.phoneUserId);
			bwCallClient.registerListener(this);
			bwCallClient.login();

		}

		public void stop()
		{
			log("Logging off.", 1);
			bwCallClient.logoff();

		}

		private void log(string message, int level)
		{
			logger.write("[CallClient "+userOperator.userId+"] "+message, level);
		}

		#region CallClientListener Members

		public void callClient_callUpdate(CallUpdate callUpdate)
		{
			// TODO:  Add CallOperator.callClient_callUpdate implementation

			try
			{

				Database database = new Database(logger, configuration);

				PhoneLogEntries phoneLogEntries = new PhoneLogEntries();
				PhoneLogEntry phoneLogEntry = phoneLogEntries.getEntry(database, userOperator.userId, callUpdate.callId);
				if (phoneLogEntry == null)
				{
					phoneLogEntry = new PhoneLogEntry();
					phoneLogEntry.userId = userOperator.userId;
					phoneLogEntry.callId = callUpdate.callId;
					phoneLogEntry.remoteNumber = callUpdate.remoteNumber;
				}

				if (callUpdate.state == 1)
				{
					if (this.offHook) phoneLogEntry.direction = 1;
					phoneLogEntry.receivedDateTime = DateTime.Now;
					phoneLogEntry.status = callUpdate.state;
				
				}
				if (callUpdate.state == 2)
				{
					phoneLogEntry.pickedUpDateTime = DateTime.Now;
					phoneLogEntry.status = callUpdate.state;
				}
				if (callUpdate.state == 3)
				{
					phoneLogEntry.transferedToNumber = callUpdate.redirectToNum;
					phoneLogEntry.finishedDateTime = DateTime.Now;
					phoneLogEntry.status = callUpdate.state;
				}
				if (callUpdate.state == 5)
				{
					phoneLogEntry.finishedDateTime = DateTime.Now;
					phoneLogEntry.status = 5;
					if (phoneLogEntry.pickedUpDateTime.Year == 1753) phoneLogEntry.status = 4;
				}
				if (callUpdate.state == 6)
				{
					if (phoneLogEntry.status != 3) phoneLogEntry.status = 5;
				}
				phoneLogEntry.save(database);			

				database.close();
			}
			catch (Exception e)
			{
				log("Exception: "+e.Message, 2);
			}
		}

		public void callClient_connect(int status)
		{
			// TODO:  Add CallOperator.callClient_connect implementation
		}

		public void callClient_profileUpdate(ProfileUpdate profileUpdate)
		{
			// TODO:  Add CallOperator.callClient_profileUpdate implementation

			log("Profile update: "+profileUpdate.firstName+" "+profileUpdate.lastName, 1);
		}

		public void callClient_sessionUpdate(SessionUpdate sessionUpdate)
		{
			// TODO:  Add CallOperator.callClient_sessionUpdate implementation
			this.offHook = sessionUpdate.offHook;
		}

		#endregion

		#region Logger Members

		public void write(string message)
		{
			// TODO:  Add OperatorCallClient.write implementation
			log(message, 2);
		}

		#endregion
	}
}
