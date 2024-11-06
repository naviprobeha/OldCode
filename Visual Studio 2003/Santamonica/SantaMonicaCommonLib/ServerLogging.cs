using System;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ServerLogging.
	/// </summary>
	public class ServerLogging
	{
		private Database database;

		public ServerLogging(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public void log(string agentCode, string message)
		{
			if (message.Length > 249) message = message.Substring(1, 249);
			database.nonQuery("INSERT INTO Logging ([Agent Code], [Message], [Event Date], [Event Timestamp]) VALUES ('"+agentCode+"','"+message+"','"+DateTime.Now.ToString("yyyy-MM-dd")+"','"+System.DateTime.Now.ToString("1754-01-01 HH:mm:ss")+"')");

		}
	}
}
